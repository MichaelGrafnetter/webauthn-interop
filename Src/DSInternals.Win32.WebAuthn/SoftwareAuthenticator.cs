#if NET8_0_OR_GREATER

using System;
using System.Buffers.Binary;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DSInternals.Win32.WebAuthn.COSE;
using PeterO.Cbor;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Software-based authenticator that signs WebAuthn requests using a PEM private key file,
    /// without requiring a hardware authenticator or the Windows WebAuthn API.
    /// </summary>
    public static class SoftwareAuthenticator
    {
        private const int DefaultCredentialIdLength = 32;

        /// <summary>
        /// Builds a complete attestation response (packed self-attestation) signed with the given private key.
        /// </summary>
        public static PublicKeyCredential MakeCredential(
            RelyingPartyInformation relyingParty,
            UserInformation user,
            byte[] challenge,
            Algorithm algorithm,
            Guid aaGuid,
            uint signatureCounter,
            AuthenticatorFlags flags,
            AsymmetricAlgorithm privateKey)
        {
            if (relyingParty?.Id == null) throw new ArgumentNullException(nameof(relyingParty));
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (challenge == null) throw new ArgumentNullException(nameof(challenge));
            if (privateKey == null) throw new ArgumentNullException(nameof(privateKey));

            // Generate random credential ID
            byte[] credentialId = RandomNumberGenerator.GetBytes(DefaultCredentialIdLength);

            // Build COSE public key
            CBORObject cosePublicKey = BuildCosePublicKey(privateKey, algorithm);

            // Build authenticator data with attested credential data
            byte[] authenticatorData = BuildAuthenticatorData(
                relyingParty.Id,
                flags | AuthenticatorFlags.AttestationData,
                signatureCounter,
                aaGuid,
                credentialId,
                cosePublicKey);

            // Build clientDataJSON
            byte[] clientDataJson = BuildClientDataJson("webauthn.create", challenge, relyingParty.Id);
            byte[] clientDataHash = SHA256.HashData(clientDataJson);

            // Sign: authenticatorData || clientDataHash
            byte[] dataToSign = ConcatArrays(authenticatorData, clientDataHash);
            byte[] signature = Sign(privateKey, dataToSign, algorithm);

            // Build attestation object (packed self-attestation: no x5c)
            CBORObject attestationObject = CBORObject.NewMap()
                .Add("fmt", "packed")
                .Add("authData", authenticatorData)
                .Add("attStmt", CBORObject.NewMap()
                    .Add("alg", (int)algorithm)
                    .Add("sig", signature));

            return new PublicKeyCredential
            {
                Id = credentialId,
                RawId = credentialId,
                Type = "public-key",
                Response = new AuthenticatorAttestationResponse
                {
                    ClientDataJson = clientDataJson,
                    AttestationObject = attestationObject.EncodeToBytes()
                }
            };
        }

        /// <summary>
        /// Builds a complete assertion response signed with the given private key.
        /// </summary>
        public static PublicKeyCredential GetAssertion(
            string relyingPartyId,
            byte[] challenge,
            Algorithm algorithm,
            uint signatureCounter,
            AuthenticatorFlags flags,
            byte[] credentialId,
            byte[]? userHandle,
            AsymmetricAlgorithm privateKey)
        {
            if (relyingPartyId == null) throw new ArgumentNullException(nameof(relyingPartyId));
            if (challenge == null) throw new ArgumentNullException(nameof(challenge));
            if (credentialId == null) throw new ArgumentNullException(nameof(credentialId));
            if (privateKey == null) throw new ArgumentNullException(nameof(privateKey));

            // Build authenticator data (no attested credential data for assertion)
            byte[] authenticatorData = BuildAuthenticatorData(
                relyingPartyId,
                flags,
                signatureCounter);

            // Build clientDataJSON
            byte[] clientDataJson = BuildClientDataJson("webauthn.get", challenge, relyingPartyId);
            byte[] clientDataHash = SHA256.HashData(clientDataJson);

            // Sign: authenticatorData || clientDataHash
            byte[] dataToSign = ConcatArrays(authenticatorData, clientDataHash);
            byte[] signature = Sign(privateKey, dataToSign, algorithm);

            return new PublicKeyCredential
            {
                Id = credentialId,
                RawId = credentialId,
                Type = "public-key",
                Response = new AuthenticatorAssertionResponse
                {
                    ClientDataJson = clientDataJson,
                    AuthenticatorData = authenticatorData,
                    Signature = signature,
                    UserHandle = userHandle
                }
            };
        }

        /// <summary>
        /// Generates a new key pair for the specified algorithm and returns the PEM-encoded private key.
        /// </summary>
        public static string GenerateKeyPairPem(Algorithm algorithm)
        {
            using var key = CreateKeyForAlgorithm(algorithm);
            return key switch
            {
                ECDsa ecdsa => ecdsa.ExportPkcs8PrivateKeyPem(),
                RSA rsa => rsa.ExportPkcs8PrivateKeyPem(),
                _ => throw new NotSupportedException($"Unsupported key type for algorithm {algorithm}.")
            };
        }

        /// <summary>
        /// Loads a private key from a PEM file. The caller is responsible for disposing the returned key.
        /// </summary>
        public static AsymmetricAlgorithm LoadPrivateKeyFromPem(string pemFilePath)
        {
            if (pemFilePath == null) throw new ArgumentNullException(nameof(pemFilePath));

            string pem = File.ReadAllText(pemFilePath);
            return ImportPrivateKeyFromPem(pem);
        }

        /// <summary>
        /// Imports a private key from a PEM string. The caller is responsible for disposing the returned key.
        /// </summary>
        public static AsymmetricAlgorithm ImportPrivateKeyFromPem(string pem)
        {
            if (pem == null) throw new ArgumentNullException(nameof(pem));

            // Try EC first, then RSA
            try
            {
                var ecdsa = ECDsa.Create();
                ecdsa.ImportFromPem(pem);
                return ecdsa;
            }
            catch (Exception)
            {
                // Not an EC key, try RSA
            }

            try
            {
                var rsa = RSA.Create();
                rsa.ImportFromPem(pem);
                return rsa;
            }
            catch (Exception)
            {
                // Not an RSA key either
            }

            throw new CryptographicException("The PEM file does not contain a supported private key (EC or RSA).");
        }

        /// <summary>
        /// Validates that the given private key is compatible with the specified COSE algorithm.
        /// Returns null if compatible, or an error message describing the mismatch.
        /// </summary>
        public static string? ValidateKeyForAlgorithm(AsymmetricAlgorithm key, Algorithm algorithm)
        {
            if (key is ECDsa ecdsa)
            {
                // EC key requires an EC algorithm
                if (algorithm is not (Algorithm.ES256 or Algorithm.ES384 or Algorithm.ES512))
                {
                    return $"The loaded key is an EC key, but the selected algorithm ({algorithm}) requires an RSA key.";
                }

                // Verify curve matches algorithm
                int expectedKeySize = algorithm switch
                {
                    Algorithm.ES256 => 256,
                    Algorithm.ES384 => 384,
                    Algorithm.ES512 => 521,
                    _ => 0
                };

                if (ecdsa.KeySize != expectedKeySize)
                {
                    Algorithm detected = DetectAlgorithm(key);
                    return $"The loaded EC key uses a {ecdsa.KeySize}-bit curve ({detected}), which does not match the selected algorithm ({algorithm}).";
                }

                return null;
            }

            if (key is RSA)
            {
                // RSA key requires an RSA algorithm
                if (algorithm is Algorithm.ES256 or Algorithm.ES384 or Algorithm.ES512)
                {
                    return $"The loaded key is an RSA key, but the selected algorithm ({algorithm}) requires an EC key.";
                }

                return null;
            }

            return $"Unsupported key type: {key.GetType().Name}";
        }

        /// <summary>
        /// Determines the COSE algorithm that matches the given private key.
        /// </summary>
        public static Algorithm DetectAlgorithm(AsymmetricAlgorithm key)
        {
            return key switch
            {
                ECDsa ecdsa => ecdsa.KeySize switch
                {
                    256 => Algorithm.ES256,
                    384 => Algorithm.ES384,
                    521 => Algorithm.ES512,
                    _ => throw new NotSupportedException($"Unsupported EC key size: {ecdsa.KeySize}")
                },
                RSA => Algorithm.RS256,
                _ => throw new NotSupportedException($"Unsupported key type: {key.GetType().Name}")
            };
        }

        private static AsymmetricAlgorithm CreateKeyForAlgorithm(Algorithm algorithm)
        {
            return algorithm switch
            {
                Algorithm.ES256 => ECDsa.Create(ECCurve.NamedCurves.nistP256),
                Algorithm.ES384 => ECDsa.Create(ECCurve.NamedCurves.nistP384),
                Algorithm.ES512 => ECDsa.Create(ECCurve.NamedCurves.nistP521),
                Algorithm.RS256 or Algorithm.RS384 or Algorithm.RS512
                    or Algorithm.PS256 or Algorithm.PS384 or Algorithm.PS512
                    or Algorithm.RS1 => RSA.Create(2048),
                _ => throw new NotSupportedException($"Key generation is not supported for algorithm {algorithm}.")
            };
        }

        private static CBORObject BuildCosePublicKey(AsymmetricAlgorithm key, Algorithm algorithm)
        {
            if (key is ECDsa ecdsa)
            {
                var parameters = ecdsa.ExportParameters(false);
                EllipticCurve curve = algorithm switch
                {
                    Algorithm.ES256 => EllipticCurve.P256,
                    Algorithm.ES384 => EllipticCurve.P384,
                    Algorithm.ES512 => EllipticCurve.P521,
                    _ => throw new NotSupportedException($"Unsupported EC algorithm: {algorithm}")
                };

                return CBORObject.NewMap()
                    .Add(CBORObject.FromObject((int)KeyCommonParameter.KeyType), (int)KeyType.EC2)
                    .Add(CBORObject.FromObject((int)KeyCommonParameter.Alg), (int)algorithm)
                    .Add(CBORObject.FromObject((int)KeyTypeParameter.Crv), (int)curve)
                    .Add(CBORObject.FromObject((int)KeyTypeParameter.X), parameters.Q.X)
                    .Add(CBORObject.FromObject((int)KeyTypeParameter.Y), parameters.Q.Y);
            }

            if (key is RSA rsa)
            {
                var parameters = rsa.ExportParameters(false);

                return CBORObject.NewMap()
                    .Add(CBORObject.FromObject((int)KeyCommonParameter.KeyType), (int)KeyType.RSA)
                    .Add(CBORObject.FromObject((int)KeyCommonParameter.Alg), (int)algorithm)
                    .Add(CBORObject.FromObject((int)KeyTypeParameter.N), parameters.Modulus)
                    .Add(CBORObject.FromObject((int)KeyTypeParameter.E), parameters.Exponent);
            }

            throw new NotSupportedException($"Unsupported key type: {key.GetType().Name}");
        }

        private static byte[] BuildAuthenticatorData(
            string relyingPartyId,
            AuthenticatorFlags flags,
            uint signatureCounter,
            Guid? aaGuid = null,
            byte[]? credentialId = null,
            CBORObject? cosePublicKey = null)
        {
            using var ms = new MemoryStream();

            // RP ID hash (SHA-256, 32 bytes)
            byte[] rpIdHash = SHA256.HashData(Encoding.UTF8.GetBytes(relyingPartyId));
            ms.Write(rpIdHash, 0, rpIdHash.Length);

            // Flags (1 byte)
            ms.WriteByte((byte)flags);

            // Signature counter (4 bytes, big-endian)
            byte[] signCountBytes = new byte[4];
            BinaryPrimitives.WriteUInt32BigEndian(signCountBytes, signatureCounter);
            ms.Write(signCountBytes, 0, 4);

            // Attested credential data (only for attestation)
            if (aaGuid.HasValue && credentialId != null && cosePublicKey != null)
            {
                // AAGUID (16 bytes, big-endian)
                byte[] aaguidBytes = aaGuid.Value.ToByteArray();
                // Convert from little-endian .NET Guid to big-endian wire format
                if (BitConverter.IsLittleEndian)
                {
                    SwapBytes(aaguidBytes, 0, 3);
                    SwapBytes(aaguidBytes, 1, 2);
                    SwapBytes(aaguidBytes, 4, 5);
                    SwapBytes(aaguidBytes, 6, 7);
                }
                ms.Write(aaguidBytes, 0, 16);

                // Credential ID length (2 bytes, big-endian)
                byte[] credIdLenBytes = new byte[2];
                BinaryPrimitives.WriteUInt16BigEndian(credIdLenBytes, (ushort)credentialId.Length);
                ms.Write(credIdLenBytes, 0, 2);

                // Credential ID
                ms.Write(credentialId, 0, credentialId.Length);

                // COSE public key (CBOR)
                byte[] coseKeyBytes = cosePublicKey.EncodeToBytes();
                ms.Write(coseKeyBytes, 0, coseKeyBytes.Length);
            }

            return ms.ToArray();
        }

        private static byte[] BuildClientDataJson(string type, byte[] challenge, string rpId)
        {
            string origin = $"https://{rpId}";
            var clientData = new
            {
                type,
                challenge = Base64UrlConverter.ToBase64UrlString(challenge),
                origin,
                crossOrigin = false
            };
            string json = JsonSerializer.Serialize(clientData);
            return Encoding.UTF8.GetBytes(json);
        }

        private static byte[] Sign(AsymmetricAlgorithm key, byte[] data, Algorithm algorithm)
        {
            if (key is ECDsa ecdsa)
            {
                HashAlgorithmName hashAlg = GetHashAlgorithm(algorithm);
                return ecdsa.SignData(data, hashAlg, DSASignatureFormat.Rfc3279DerSequence);
            }

            if (key is RSA rsa)
            {
                HashAlgorithmName hashAlg = GetHashAlgorithm(algorithm);
                RSASignaturePadding padding = GetRsaPadding(algorithm);
                return rsa.SignData(data, hashAlg, padding);
            }

            throw new NotSupportedException($"Unsupported key type for signing: {key.GetType().Name}");
        }

        private static HashAlgorithmName GetHashAlgorithm(Algorithm algorithm)
        {
            return algorithm switch
            {
                Algorithm.ES256 or Algorithm.RS256 or Algorithm.PS256 => HashAlgorithmName.SHA256,
                Algorithm.ES384 or Algorithm.RS384 or Algorithm.PS384 => HashAlgorithmName.SHA384,
                Algorithm.ES512 or Algorithm.RS512 or Algorithm.PS512 => HashAlgorithmName.SHA512,
                Algorithm.RS1 => HashAlgorithmName.SHA1,
                _ => throw new NotSupportedException($"Unsupported algorithm: {algorithm}")
            };
        }

        private static RSASignaturePadding GetRsaPadding(Algorithm algorithm)
        {
            return algorithm switch
            {
                Algorithm.PS256 or Algorithm.PS384 or Algorithm.PS512 => RSASignaturePadding.Pss,
                Algorithm.RS256 or Algorithm.RS384 or Algorithm.RS512 or Algorithm.RS1 => RSASignaturePadding.Pkcs1,
                _ => throw new NotSupportedException($"Algorithm {algorithm} is not an RSA algorithm.")
            };
        }

        private static byte[] ConcatArrays(byte[] a, byte[] b)
        {
            byte[] result = new byte[a.Length + b.Length];
            Buffer.BlockCopy(a, 0, result, 0, a.Length);
            Buffer.BlockCopy(b, 0, result, a.Length, b.Length);
            return result;
        }

        private static void SwapBytes(byte[] bytes, int index1, int index2)
        {
            (bytes[index2], bytes[index1]) = (bytes[index1], bytes[index2]);
        }
    }
}

#endif
