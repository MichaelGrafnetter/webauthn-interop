#if NET5_0_OR_GREATER

using System;
using System.IO;
using System.Security.Cryptography;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn.Cryptography;

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
    public static AttestationPublicKeyCredential MakeCredential(
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

        if (string.IsNullOrEmpty(relyingParty.Id))
        {
            throw new ArgumentException("Relying party ID must be provided.", nameof(relyingParty));
        }

        return MakeCredential(
            relyingParty,
            relyingParty.Id,
            user,
            challenge,
            algorithm,
            aaGuid,
            signatureCounter,
            flags,
            privateKey,
            credentialId);
    }

    /// <summary>
    /// Builds a complete attestation response (packed self-attestation) signed with the given private key.
    /// </summary>
    public static AttestationPublicKeyCredential MakeCredential(
        RelyingPartyInformation relyingParty,
        string hostName,
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
        ArgumentNullException.ThrowIfNull(hostName);

        // Use provided credential ID or generate a random one
        credentialId ??= RandomNumberGenerator.GetBytes(DefaultCredentialIdLength);

        // Build COSE public key
        byte[] cosePublicKey = CredentialPublicKey.Build(privateKey, algorithm);

        // Build authenticator data with attested credential data
        AttestedCredentialData attestedData = new(aaGuid, credentialId, cosePublicKey);
        AuthenticatorData authenticatorData = new(
            relyingParty.Id ?? hostName,
            flags,
            signatureCounter,
            attestedData);
        byte[] authenticatorDataBytes = authenticatorData.ToByteArray();

        // Build clientDataJSON
        var clientData = CollectedClientData.Create(ApiConstants.ClientDataCredentialCreate, challenge, hostName, relyingParty.Id);
        byte[] clientDataJson = clientData.ToByteArray();
        byte[] clientDataHash = SHA256.HashData(clientDataJson);

        // Sign: authenticatorData || clientDataHash
        byte[] dataToSign = [.. authenticatorDataBytes, .. clientDataHash];
        byte[] signature = Sign(privateKey, dataToSign, algorithm);

        // Build attestation object (packed self-attestation: no x5c)
        byte[] attestationObject = AuthenticatorData.PackAttestation(authenticatorDataBytes, algorithm, signature);

        return new AttestationPublicKeyCredential
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
    public static AssertionPublicKeyCredential GetAssertion(
        string relyingPartyId,
        byte[] challenge,
        Algorithm algorithm,
        uint signatureCounter,
        AuthenticatorFlags flags,
        byte[] credentialId,
        byte[]? userHandle,
        AsymmetricAlgorithm privateKey,
        AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any)
    {
        return GetAssertion(
            relyingPartyId,
            relyingPartyId,
            challenge,
            algorithm,
            signatureCounter,
            flags,
            credentialId,
            userHandle,
            privateKey,
            authenticatorAttachment);
    }

    /// <summary>
    /// Builds a complete assertion response signed with the given private key.
    /// </summary>
    public static AssertionPublicKeyCredential GetAssertion(
        string hostName,
        string? relyingPartyId,
        byte[] challenge,
        Algorithm algorithm,
        uint signatureCounter,
        AuthenticatorFlags flags,
        byte[] credentialId,
        byte[]? userHandle,
        AsymmetricAlgorithm privateKey,
        AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any)
    {
        ArgumentNullException.ThrowIfNull(hostName);
        ArgumentNullException.ThrowIfNull(challenge);
        ArgumentNullException.ThrowIfNull(credentialId);
        ArgumentNullException.ThrowIfNull(privateKey);

        AuthenticatorData authenticatorData = new(relyingPartyId ?? hostName, flags, signatureCounter);
        byte[] authenticatorDataBytes = authenticatorData.ToByteArray();

        var clientData = CollectedClientData.Create(ApiConstants.ClientDataCredentialGet, challenge, hostName, relyingPartyId);
        byte[] clientDataJson = clientData.ToByteArray();
        byte[] clientDataHash = SHA256.HashData(clientDataJson);

        byte[] dataToSign = [.. authenticatorDataBytes, .. clientDataHash];
        byte[] signature = Sign(privateKey, dataToSign, algorithm);

        return new AssertionPublicKeyCredential
        {
            Id = credentialId,
            RawId = credentialId,
            Type = ApiConstants.PublicKeyCredentialType,
            AuthenticatorAttachment = authenticatorAttachment,
            Response = new AuthenticatorAssertionResponse
            {
                ClientDataJson = clientDataJson,
                AuthenticatorData = authenticatorDataBytes,
                Signature = signature,
                UserHandle = userHandle
            }
        };
    }

    public static AsymmetricAlgorithm GenerateKeyPair(Algorithm algorithm)
    {
        if (algorithm.IsRSA)
        {
            return RSA.Create(2048);
        }

        return algorithm switch
        {
            Algorithm.ES256 => ECDsa.Create(ECCurve.NamedCurves.nistP256),
            Algorithm.ES384 => ECDsa.Create(ECCurve.NamedCurves.nistP384),
            Algorithm.ES512 => ECDsa.Create(ECCurve.NamedCurves.nistP521),
            // TODO: Add Ed25519 support
            _ => throw new NotSupportedException($"Key generation is not supported for algorithm {algorithm}.")
        };
    }

    /// <summary>
    /// Generates a private key for the requested COSE algorithm and exports it as PKCS#8 PEM.
    /// </summary>
    public static string GenerateKeyPairPem(Algorithm algorithm)
    {
        using AsymmetricAlgorithm key = GenerateKeyPair(algorithm);

        return key switch
        {
            ECDsa ecdsa => ecdsa.ExportPkcs8PrivateKeyPem(),
            RSA rsa => rsa.ExportPkcs8PrivateKeyPem(),
            _ => throw new NotSupportedException($"PEM export is not supported for key type {key.GetType().Name}.")
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

        // Try EC first, then RSA, then Ed25519 (net8+ only)
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
            // Not an RSA key, try Ed25519
        }

#if NET8_0_OR_GREATER
        try
        {
            var ed25519 = Ed25519.Create();
            ed25519.ImportFromPem(pem);
            return ed25519;
        }
        catch (Exception)
        {
            // Not an Ed25519 key
        }
#endif

        throw new CryptographicException("The PEM data does not contain a supported private key format.");
    }

    /// <summary>
    /// Imports a private key from DER-encoded PKCS#8 data. The caller is responsible for disposing the returned key.
    /// </summary>
    public static AsymmetricAlgorithm ImportPrivateKeyFromPkcs8(byte[] privateKey)
    {
        ArgumentNullException.ThrowIfNull(privateKey);

        try
        {
            var ecdsa = ECDsa.Create();
            ecdsa.ImportPkcs8PrivateKey(privateKey, out _);
            return ecdsa;
        }
        catch (Exception)
        {
            // Not an EC key, try RSA.
        }

        try
        {
            var rsa = RSA.Create();
            rsa.ImportPkcs8PrivateKey(privateKey, out _);
            return rsa;
        }
        catch (Exception)
        {
            // Not an RSA key, try Ed25519.
        }

#if NET8_0_OR_GREATER
        try
        {
            var ed25519 = Ed25519.Create();
            ed25519.ImportPkcs8PrivateKey(privateKey, out _);
            return ed25519;
        }
        catch (Exception)
        {
            // Not an Ed25519 key.
        }
#endif

        throw new CryptographicException("The PKCS#8 data does not contain a supported private key format.");
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
#if NET8_0_OR_GREATER
            Ed25519 => Algorithm.EdDSA,
#endif
            _ => throw new NotSupportedException($"Unsupported key type: {key.GetType().Name}")
        };
    }

    private static byte[] Sign(AsymmetricAlgorithm key, byte[] data, Algorithm algorithm)
    {
        HashAlgorithmName hashAlg = algorithm.HashAlgorithm ?? throw new NotSupportedException($"Unsupported algorithm: {algorithm}");

#if NET8_0_OR_GREATER
        if (key is Ed25519 ed25519)
        {
            return ed25519.SignData(data, hashAlg);
        }
#endif

        if (key is ECDsa ecdsa)
        {
            return ecdsa.SignData(data, hashAlg, DSASignatureFormat.Rfc3279DerSequence);
        }
        else if (key is RSA rsa)
        {
            RSASignaturePadding padding = algorithm.RsaPadding ?? throw new NotSupportedException($"Unsupported algorithm: {algorithm}");
            return rsa.SignData(data, hashAlg, padding);
        }

        throw new NotSupportedException($"Unsupported key type for signing: {key.GetType().Name}");
    }
}

#endif
