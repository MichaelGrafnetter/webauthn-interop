#if NET5_0_OR_GREATER

using System;
using System.Buffers.Binary;
using System.Formats.Cbor;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Software-based authenticator that signs WebAuthn requests using a PEM private key file,
    /// without requiring a hardware authenticator or the Windows WebAuthn API.
    /// </summary>
    public static class SoftwareAuthenticator
    {
        public const int DefaultCredentialIdLength = 32;

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
            AsymmetricAlgorithm privateKey,
            byte[]? credentialId = null)
        {
            ArgumentNullException.ThrowIfNull(relyingParty);
            ArgumentNullException.ThrowIfNull(user);
            ArgumentNullException.ThrowIfNull(challenge);
            ArgumentNullException.ThrowIfNull(privateKey);

            // Use provided credential ID or generate a random one
            credentialId ??= RandomNumberGenerator.GetBytes(DefaultCredentialIdLength);

            // Build COSE public key
            byte[] cosePublicKey = BuildCosePublicKey(privateKey, algorithm);

            // Build authenticator data with attested credential data
            byte[] authenticatorData = BuildAuthenticatorData(
                relyingParty.Id,
                flags | AuthenticatorFlags.AttestationData,
                signatureCounter,
                aaGuid,
                credentialId,
                cosePublicKey);

            // Build clientDataJSON
            byte[] clientDataJson = BuildClientDataJson(ApiConstants.ClientDataCredentialCreate, challenge, relyingParty.Id);
            byte[] clientDataHash = SHA256.HashData(clientDataJson);

            // Sign: authenticatorData || clientDataHash
            byte[] dataToSign = [..authenticatorData, ..clientDataHash];
            byte[] signature = Sign(privateKey, dataToSign, algorithm);

            // Build attestation object (packed self-attestation: no x5c)
            byte[] attestationObject = BuildAttestationObject(authenticatorData, algorithm, signature);

            return new PublicKeyCredential
            {
                Id = credentialId,
                RawId = credentialId,
                Type = ApiConstants.PublicKeyCredentialType,
                Response = new AuthenticatorAttestationResponse
                {
                    ClientDataJson = clientDataJson,
                    AttestationObject = attestationObject
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
            ArgumentNullException.ThrowIfNull(relyingPartyId);
            ArgumentNullException.ThrowIfNull(challenge);
            ArgumentNullException.ThrowIfNull(credentialId);
            ArgumentNullException.ThrowIfNull(privateKey);

            // Build authenticator data (no attested credential data for assertion)
            byte[] authenticatorData = BuildAuthenticatorData(
                relyingPartyId,
                flags,
                signatureCounter);

            // Build clientDataJSON
            byte[] clientDataJson = BuildClientDataJson(ApiConstants.ClientDataCredentialGet, challenge, relyingPartyId);
            byte[] clientDataHash = SHA256.HashData(clientDataJson);

            // Sign: authenticatorData || clientDataHash
            byte[] dataToSign = [..authenticatorData, ..clientDataHash];
            byte[] signature = Sign(privateKey, dataToSign, algorithm);

            return new PublicKeyCredential
            {
                Id = credentialId,
                RawId = credentialId,
                Type = ApiConstants.PublicKeyCredentialType,
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
            ArgumentNullException.ThrowIfNull(pemFilePath);

            string pem = File.ReadAllText(pemFilePath);
            return ImportPrivateKeyFromPem(pem);
        }

        /// <summary>
        /// Imports a private key from a PEM string. The caller is responsible for disposing the returned key.
        /// </summary>
        public static AsymmetricAlgorithm ImportPrivateKeyFromPem(string pem)
        {
            ArgumentNullException.ThrowIfNull(pem);

            // Try EC first, then RSA
            try
            {
                var ecdsa = ECDsa.Create();
                ecdsa.ImportFromPem(pem);
                return ecdsa;
            }
            catch
            {
                // Not an EC key, try RSA
            }

            try
            {
                var rsa = RSA.Create();
                rsa.ImportFromPem(pem);
                return rsa;
            }
            catch
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

        private static byte[] BuildCosePublicKey(AsymmetricAlgorithm key, Algorithm algorithm)
        {
            var writer = new CborWriter(CborConformanceMode.Lax);

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

                writer.WriteStartMap(5);
                writer.WriteInt32((int)KeyCommonParameter.KeyType);
                writer.WriteInt32((int)KeyType.EC2);
                writer.WriteInt32((int)KeyCommonParameter.Alg);
                writer.WriteInt32((int)algorithm);
                writer.WriteInt32((int)KeyTypeParameter.Crv);
                writer.WriteInt32((int)curve);
                writer.WriteInt32((int)KeyTypeParameter.X);
                writer.WriteByteString(parameters.Q.X!);
                writer.WriteInt32((int)KeyTypeParameter.Y);
                writer.WriteByteString(parameters.Q.Y!);
                writer.WriteEndMap();

                return writer.Encode();
            }

            if (key is RSA rsa)
            {
                var parameters = rsa.ExportParameters(false);

                writer.WriteStartMap(4);
                writer.WriteInt32((int)KeyCommonParameter.KeyType);
                writer.WriteInt32((int)KeyType.RSA);
                writer.WriteInt32((int)KeyCommonParameter.Alg);
                writer.WriteInt32((int)algorithm);
                writer.WriteInt32((int)KeyTypeParameter.N);
                writer.WriteByteString(parameters.Modulus!);
                writer.WriteInt32((int)KeyTypeParameter.E);
                writer.WriteByteString(parameters.Exponent!);
                writer.WriteEndMap();

                return writer.Encode();
            }

            throw new NotSupportedException($"Unsupported key type: {key.GetType().Name}");
        }

        private static byte[] BuildAuthenticatorData(
            string relyingPartyId,
            AuthenticatorFlags flags,
            uint signatureCounter,
            Guid? aaGuid = null,
            byte[]? credentialId = null,
            byte[]? cosePublicKey = null)
        {
            using var ms = new MemoryStream();

            // RP ID hash (SHA-256, 32 bytes)
            byte[] rpIdHash = SHA256.HashData(Encoding.UTF8.GetBytes(relyingPartyId));
            ms.Write(rpIdHash, 0, rpIdHash.Length);

            // Flags (1 byte)
            ms.WriteByte((byte)flags);

            // Signature counter (4 bytes, big-endian)
            byte[] signCountBytes = new byte[sizeof(uint)];
            BinaryPrimitives.WriteUInt32BigEndian(signCountBytes, signatureCounter);
            ms.Write(signCountBytes, 0, sizeof(uint));

            // Attested credential data (only for attestation)
            if (aaGuid.HasValue && credentialId != null && cosePublicKey != null)
            {
                // AAGUID (16 bytes, big-endian)
                byte[] aaguidBytes = aaGuid.Value.ToByteArray(bigEndian: true);
                ms.Write(aaguidBytes, 0, aaguidBytes.Length);

                // Credential ID length (2 bytes, big-endian)
                byte[] credIdLenBytes = new byte[sizeof(ushort)];
                BinaryPrimitives.WriteUInt16BigEndian(credIdLenBytes, (ushort)credentialId.Length);
                ms.Write(credIdLenBytes, 0, sizeof(ushort));

                // Credential ID
                ms.Write(credentialId, 0, credentialId.Length);

                // COSE public key (already CBOR-encoded)
                ms.Write(cosePublicKey, 0, cosePublicKey.Length);
            }

            return ms.ToArray();
        }

        private static byte[] BuildAttestationObject(byte[] authenticatorData, Algorithm algorithm, byte[] signature)
        {
            var writer = new CborWriter(CborConformanceMode.Lax);
            writer.WriteStartMap(3);

            writer.WriteTextString("fmt");
            writer.WriteTextString("packed");

            writer.WriteTextString("authData");
            writer.WriteByteString(authenticatorData);

            writer.WriteTextString("attStmt");
            writer.WriteStartMap(2);
            writer.WriteTextString("alg");
            writer.WriteInt32((int)algorithm);
            writer.WriteTextString("sig");
            writer.WriteByteString(signature);
            writer.WriteEndMap();

            writer.WriteEndMap();
            return writer.Encode();
        }

        private static byte[] BuildClientDataJson(string type, byte[] challenge, string rpId)
        {
            var clientData = new CollectedClientData
            {
                Type = type,
                Challenge = challenge,
                Origin = $"https://{rpId}",
                CrossOrigin = false
            };

            return JsonSerializer.SerializeToUtf8Bytes(clientData, WebAuthnJsonContext.Default.CollectedClientData);
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
    }
}

#endif
