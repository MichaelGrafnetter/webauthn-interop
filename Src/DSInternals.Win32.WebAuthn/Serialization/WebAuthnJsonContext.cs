using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Cryptography;
using DSInternals.Win32.WebAuthn.EntraID;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Okta;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Source-generated JSON serialization metadata for WebAuthn models.
    /// </summary>
    [JsonSourceGenerationOptions(
        PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonSerializable(typeof(CollectedClientData))]
    [JsonSerializable(typeof(AttestationPublicKeyCredential))]
    [JsonSerializable(typeof(AssertionPublicKeyCredential))]
    [JsonSerializable(typeof(AuthenticatorAttestationResponse))]
    [JsonSerializable(typeof(AuthenticatorAssertionResponse))]
    [JsonSerializable(typeof(AuthenticationExtensionsClientAttestationInputs))]
    [JsonSerializable(typeof(AuthenticationExtensionsClientAttestationOutputs))]
    [JsonSerializable(typeof(AuthenticationExtensionsClientAssertionInputs))]
    [JsonSerializable(typeof(AuthenticationExtensionsClientAssertionOutputs))]
    [JsonSerializable(typeof(CredentialPropertiesOutputs))]
    [JsonSerializable(typeof(LargeBlobAttestationInputs))]
    [JsonSerializable(typeof(LargeBlobAttestationOutputs))]
    [JsonSerializable(typeof(LargeBlobAssertionInputs))]
    [JsonSerializable(typeof(LargeBlobAssertionOutputs))]
    [JsonSerializable(typeof(PRFAttestationInputs))]
    [JsonSerializable(typeof(PRFAttestationOutputs))]
    [JsonSerializable(typeof(PRFAssertionInputs))]
    [JsonSerializable(typeof(PRFAssertionOutputs))]
    [JsonSerializable(typeof(PRFValues))]
    [JsonSerializable(typeof(RemoteDesktopClientOverride))]
    [JsonSerializable(typeof(PublicKeyCredentialCreationOptions))]
    [JsonSerializable(typeof(MicrosoftGraphWebauthnCredentialCreationOptions))]
    [JsonSerializable(typeof(MicrosoftGraphWebauthnAttestationResponse))]
    [JsonSerializable(typeof(OktaWebauthnCredentialCreationOptions))]
    [JsonSerializable(typeof(OktaFido2AuthenticationMethod))]
    [JsonSerializable(typeof(OktaWebauthnAttestationResponse))]
#if NET5_0_OR_GREATER
    [JsonSerializable(typeof(KeePassXCPasskey))]
    [JsonSerializable(typeof(BitwardenVaultExportHeader))]
    [JsonSerializable(typeof(BitwardenCleartextVaultExport))]
    [JsonSerializable(typeof(BitwardenEncryptedVaultExport))]
#endif
    [JsonSerializable(typeof(PublicKeyCredentialRequestOptions))]
    public partial class WebAuthnJsonContext : JsonSerializerContext
    {
    }
}
