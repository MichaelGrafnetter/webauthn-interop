using System;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.EntraID;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Okta;

namespace DSInternals.Win32.WebAuthn.Serialization;

#if NET7_0_OR_GREATER
[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(CollectedClientData))]
[JsonSerializable(typeof(MicrosoftGraphWebauthnAttestationResponse))]
[JsonSerializable(typeof(MicrosoftGraphWebauthnCredentialCreationOptions))]
[JsonSerializable(typeof(OktaFido2AuthenticationMethod))]
[JsonSerializable(typeof(OktaProfile))]
[JsonSerializable(typeof(OktaWebauthnAttestationResponse))]
[JsonSerializable(typeof(OktaWebauthnCredentialCreationOptions))]
internal partial class WebAuthnJsonSerializerContext : JsonSerializerContext
{
}
#endif