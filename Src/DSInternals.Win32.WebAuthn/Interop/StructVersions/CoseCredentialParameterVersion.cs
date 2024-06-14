using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The current version of the WEBAUTHN_COSE_CREDENTIAL_PARAMETER structure, to allow for modifications in the future.
    /// </summary>
    internal enum CoseCredentialParameterVersion : uint
    {
        /// <summary>
        /// Current version
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETER_CURRENT_VERSION.</remarks>
        Current = PInvoke.WEBAUTHN_COSE_CREDENTIAL_PARAMETER_CURRENT_VERSION
    }
}
