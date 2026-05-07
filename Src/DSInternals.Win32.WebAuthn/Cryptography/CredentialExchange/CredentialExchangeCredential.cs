#if NET5_0_OR_GREATER

using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Base type for entries inside a CXF item's credentials array.
/// </summary>
/// <remarks>
/// CXF defines several credential types (e.g. <c>passkey</c>, <c>basic-auth</c>, <c>ssh-key</c>)
/// distinguished by a <c>type</c> discriminator. Only credential types that are explicitly
/// modeled by derived classes are deserialized into a typed instance; unknown discriminators
/// fall back to a bare <see cref="CredentialExchangeCredential"/> so the surrounding payload
/// still parses cleanly.
/// </remarks>
[JsonPolymorphic(
    TypeDiscriminatorPropertyName = "type",
    UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FallBackToBaseType,
    IgnoreUnrecognizedTypeDiscriminators = true)]
[JsonDerivedType(typeof(CredentialExchangePasskey), CredentialExchangeFile.PasskeyCredentialType)]
public class CredentialExchangeCredential
{
}

#endif
