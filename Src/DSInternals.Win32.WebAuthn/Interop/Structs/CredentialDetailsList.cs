using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// List of platform credentials.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_LIST.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialDetailsList : SafeStructArrayOut<CredentialDetails>
    {
        private CredentialDetailsList() : base() { }
    }
}
