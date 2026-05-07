using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// This enumeration's values describe the Relying Party's requirements for client-side discoverable credentials
    /// (formerly known as resident credentials or resident keys).
    /// </summary>
    /// <see href="https://www.w3.org/TR/webauthn-3/#enum-residentKeyRequirement"/>
    [JsonConverter(typeof(WebAuthnJsonEnumConverter<ResidentKeyRequirement>))]
    public enum ResidentKeyRequirement : uint
    {
        /// <summary>
        /// The Relying Party prefers creating a server-side credential, but will accept a client-side discoverable credential.
        /// The client and authenticator SHOULD create a server-side credential if possible.
        /// </summary>
        [JsonStringEnumMemberName("discouraged")]
        Discouraged = 0,

        /// <summary>
        /// The Relying Party strongly prefers creating a client-side discoverable credential, but will accept a server-side credential.
        /// The client and authenticator SHOULD create a discoverable credential if possible.
        /// </summary>
        [JsonStringEnumMemberName("preferred")]
        Preferred = 1,

        /// <summary>
        /// The Relying Party requires a client-side discoverable credential.
        /// The client MUST return an error if a client-side discoverable credential cannot be created.
        /// </summary>
        [JsonStringEnumMemberName("required")]
        Required = 2
    }
}
