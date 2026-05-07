using System.Text.Json;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// A single user verification method factor reported by the authenticator in the WebAuthn uvm extension output.
/// </summary>
/// <see href="https://www.w3.org/TR/webauthn-2/#sctn-uvm-extension"/>
[JsonConverter(typeof(UvmEntryConverter))]
public sealed class UvmEntry
{
    /// <summary>
    /// The authentication method/factor used by the authenticator to verify the user.
    /// </summary>
    public UserVerificationMethod UserVerificationMethod { get; set; }

    /// <summary>
    /// The method used by the authenticator to protect the FIDO registration private key material.
    /// </summary>
    public KeyProtectionType KeyProtectionType { get; set; }

    /// <summary>
    /// The method used by the authenticator to protect the matcher that performs user verification.
    /// </summary>
    public MatcherProtectionType MatcherProtectionType { get; set; }

    /// <summary>
    /// Initializes a new empty <see cref="UvmEntry"/>.
    /// </summary>
    public UvmEntry()
    {
    }

    /// <summary>
    /// Initializes a new <see cref="UvmEntry"/> with the specified factor values.
    /// </summary>
    /// <param name="userVerificationMethod">User verification method/factor.</param>
    /// <param name="keyProtectionType">Key protection type.</param>
    /// <param name="matcherProtectionType">Matcher protection type.</param>
    public UvmEntry(UserVerificationMethod userVerificationMethod, KeyProtectionType keyProtectionType, MatcherProtectionType matcherProtectionType)
    {
        UserVerificationMethod = userVerificationMethod;
        KeyProtectionType = keyProtectionType;
        MatcherProtectionType = matcherProtectionType;
    }
}
