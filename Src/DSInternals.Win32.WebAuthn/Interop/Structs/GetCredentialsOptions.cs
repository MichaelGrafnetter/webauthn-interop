using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Platform Credential Retrieval Options.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_GET_CREDENTIALS_OPTIONS.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class GetCredentialsOptions
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private GetCredentialOptionsVersion _version = GetCredentialOptionsVersion.Version1;

        /// <summary>
        /// Optional relying party ID.
        /// </summary>
        public string RpId;

        /// <summary>
        /// Indicates whether the browser is in Private Mode. Defaulting to FALSE.
        /// </summary>
        public bool BrowserInPrivateMode;
    }
}
