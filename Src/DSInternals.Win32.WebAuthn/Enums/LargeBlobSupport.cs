using System.Text.Json.Serialization;
using Windows.Win32;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Large blob support options.
    /// </summary>
    [JsonConverter(typeof(WebAuthnJsonEnumConverter<LargeBlobSupport>))]
    public enum LargeBlobSupport : uint
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_NONE.
        /// </remarks>
        None = PInvoke.WEBAUTHN_LARGE_BLOB_SUPPORT_NONE,

        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_REQUIRED.
        /// </remarks>
        [JsonStringEnumMemberName("required")]
        Required = PInvoke.WEBAUTHN_LARGE_BLOB_SUPPORT_REQUIRED,

        /// <remarks>
        /// Corresponds to WEBAUTHN_LARGE_BLOB_SUPPORT_PREFERRED.
        /// </remarks>
        [JsonStringEnumMemberName("preferred")]
        Preferred = PInvoke.WEBAUTHN_LARGE_BLOB_SUPPORT_PREFERRED
    }
}
