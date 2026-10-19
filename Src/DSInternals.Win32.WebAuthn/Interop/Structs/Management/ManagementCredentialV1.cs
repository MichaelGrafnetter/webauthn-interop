using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Contains credential and account display information shared by private record versions 1 and 2.</summary>
    /// <remarks>This is neither the documented WEBAUTHN_CREDENTIAL_DETAILS structure nor a CTAP2 credential-management CBOR response.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ManagementCredentialV1
    {
        /// <summary>Private record layout version.</summary>
        internal uint Version;

        /// <summary>Borrowed NUL-terminated UTF-16 relying party identifier.</summary>
        internal IntPtr RelyingPartyId;

        /// <summary>Borrowed NUL-terminated UTF-16 account name.</summary>
        internal IntPtr UserName;

        /// <summary>Borrowed NUL-terminated UTF-16 account display name.</summary>
        internal IntPtr DisplayName;

        /// <summary>Opaque credential identifier length in bytes.</summary>
        internal uint CredentialIdLength;

        /// <summary>Borrowed opaque credential identifier bytes.</summary>
        internal IntPtr CredentialId;

        /// <summary>First reserved DWORD following the credential identifier; semantics are not established.</summary>
        internal uint Unknown30;

        /// <summary>Second reserved DWORD following the credential identifier; semantics are not established.</summary>
        internal uint Unknown34;
    }
}
