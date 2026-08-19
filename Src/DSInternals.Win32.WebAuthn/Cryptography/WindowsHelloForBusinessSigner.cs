using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Produces WebAuthn assertions for Microsoft Entra ID (login.microsoft.com) using the local
/// Windows Hello for Business (NGC) key that is registered as a platform passkey.
/// </summary>
public static class WindowsHelloForBusinessSigner
{
    /// <summary>
    /// The relying party ID used by Microsoft Entra ID passkey sign-in.
    /// </summary>
    public const string RelyingPartyId = "login.microsoft.com";

    /// <summary>
    /// The name of the CNG key storage provider ("NGC KSP") that hosts Windows Hello for Business keys.
    /// </summary>
    public const string NgcKeyStorageProvider = "Microsoft Passport Key Storage Provider";

    /// <summary>
    /// Fixed substring found in the name of NGC keys created for Entra ID sign-in,
    /// e.g. S-1-5-21-.../173889d3-51f8-4156-b0e0-14c705ca6430/login.windows.net/270968e9-f6d5-43aa-940c-d330e1d50d83/john@contoso.com.
    /// </summary>
    public const string NgcKeyNameAuthorityMarker = "login.windows.net";

    /// <summary>
    /// Produces an assertion for every registered Microsoft Entra ID passkey that has a matching NGC key on this device.
    /// </summary>
    /// <param name="challenge">The challenge to sign.</param>
    /// <param name="signatureCounter">The signature counter value to embed in the authenticator data. Defaults to 0.</param>
    /// <param name="browserInPrivateMode">Indicates whether the request originates from a browser running in private/incognito mode.</param>
    /// <returns>Zero, one, or more assertions, depending on the number of registered credentials with a locally resolvable key.</returns>
    public static IReadOnlyList<AssertionPublicKeyCredential> GetAssertions(byte[] challenge, uint signatureCounter = 0, bool browserInPrivateMode = false)
    {
        ArgumentNullException.ThrowIfNull(challenge);

        // Ask the Windows WebAuthn API which passkeys are registered for Entra ID sign-in.
        IReadOnlyList<CredentialDetails>? credentials = WebAuthnApi.GetPlatformCredentialList(RelyingPartyId, browserInPrivateMode);

        if (credentials == null || credentials.Count == 0)
        {
            return [];
        }

        IReadOnlyList<NCryptKeyName> keyNames = ListNgcKeys();
        var assertions = new List<AssertionPublicKeyCredential>();

        foreach (CredentialDetails passkey in credentials)
        {
            string? upn = passkey.UserInformation?.Name;

            if (passkey.CredentialId == null || string.IsNullOrEmpty(upn))
            {
                continue;
            }

            // Multiple keys can match the UPN (e.g. leftover/rotated keys), so every candidate must be verified.
            IEnumerable<NCryptKeyName> candidates = keyNames.Where(key =>
                !string.IsNullOrEmpty(key.Name) &&
                key.Name.Contains(upn, StringComparison.OrdinalIgnoreCase) &&
                key.Name.Contains(NgcKeyNameAuthorityMarker, StringComparison.OrdinalIgnoreCase));

            foreach (NCryptKeyName candidateKey in candidates)
            {
                AssertionPublicKeyCredential? assertion = TryGetAssertion(candidateKey.Name!, passkey, challenge, signatureCounter);

                if (assertion != null)
                {
                    assertions.Add(assertion);
                    break;
                }
            }
        }

        return assertions;
    }

    /// <summary>
    /// Opens the candidate NGC key and, if its public key hash matches the credential ID, signs the assertion with it.
    /// </summary>
    private static AssertionPublicKeyCredential? TryGetAssertion(
        string keyName,
        CredentialDetails passkey,
        byte[] challenge,
        uint signatureCounter)
    {
        var provider = new CngProvider(NgcKeyStorageProvider);

        // Windows Hello is expected to already be unlocked at this point, so opening the key can stay silent.
        using CngKey cngKey = CngKey.Open(keyName, provider, CngKeyOpenOptions.Silent);

        // Windows derives the WebAuthn credential ID as the SHA-256 hash of the raw NCrypt public key blob.
        byte[] publicKeyBlob = cngKey.Export(CngKeyBlobFormat.GenericPublicBlob);
        byte[] publicKeyHash = SHA256.HashData(publicKeyBlob);

        if (!publicKeyHash.AsSpan().SequenceEqual(passkey.CredentialId))
        {
            // This key does not correspond to the credential in question; the caller should try another candidate.
            return null;
        }

        using AsymmetricAlgorithm privateKey = CreateAsymmetricAlgorithm(cngKey);
        Algorithm algorithm = SoftwareAuthenticator.DetectAlgorithm(privateKey);

        return SoftwareAuthenticator.GetAssertion(
            passkey.RelyingPartyInformation?.Id ?? RelyingPartyId,
            challenge,
            algorithm,
            signatureCounter,
            AuthenticatorFlags.UserPresent | AuthenticatorFlags.UserVerified,
            passkey.CredentialId!,
            passkey.UserInformation?.Id,
            privateKey);
    }

    private static AsymmetricAlgorithm CreateAsymmetricAlgorithm(CngKey cngKey)
    {
        if (cngKey.AlgorithmGroup == CngAlgorithmGroup.Rsa)
        {
            return new RSACng(cngKey);
        }

        if (cngKey.AlgorithmGroup == CngAlgorithmGroup.ECDsa)
        {
            return new ECDsaCng(cngKey);
        }

        throw new NotSupportedException($"Unsupported NGC key algorithm group: {cngKey.AlgorithmGroup}");
    }

    /// <summary>
    /// Enumerates the names of all keys stored in the NGC key storage provider.
    /// </summary>
    public static IReadOnlyList<NCryptKeyName> ListNgcKeys()
    {
        return ListCngKeys(NgcKeyStorageProvider);
    }

    /// <summary>
    /// Enumerates the keys stored in the given CNG key storage provider.
    /// </summary>
    /// <param name="providerName">The provider name, or <see langword="null"/> for the default provider.</param>
    public static IReadOnlyList<NCryptKeyName> ListCngKeys(string? providerName = null)
    {
        var keyNames = new List<NCryptKeyName>();

        HRESULT openResult = NativeMethods.NCryptOpenStorageProvider(
            out SafeNCryptProviderHandle providerHandle,
            providerName,
            NCryptOpenStorageProviderFlags.Silent);
        ApiHelper.Validate(openResult);

        using (providerHandle)
        {
            SafeNCryptEnumStateHandle enumState = new();

            try
            {
                while (true)
                {
                    HRESULT enumResult = NativeMethods.NCryptEnumKeys(
                        providerHandle,
                        out NCryptKeyName? keyName,
                        ref enumState,
                        CngKeyOpenOptions.Silent);

                    if (enumResult == HRESULT.NTE_NO_MORE_ITEMS)
                    {
                        break;
                    }

                    ApiHelper.Validate(enumResult);

                    if (!string.IsNullOrEmpty(keyName?.Name))
                    {
                        keyNames.Add(keyName);
                    }
                }
            }
            finally
            {
                enumState.Dispose();
            }
        }

        return keyNames;
    }
}
