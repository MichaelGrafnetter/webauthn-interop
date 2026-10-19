using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Extends private credential display information with an additional data buffer.</summary>
    /// <remarks>The buffer's meaning is not established; it must not be interpreted as a CTAP2 largeBlobKey or credBlob based on its position.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ManagementCredentialV3
    {
        #region V1 and V2 members

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

        #endregion

        #region V3 members

        /// <summary>Length of the uninterpreted additional data in bytes.</summary>
        internal uint AdditionalDataLength;

        /// <summary>Borrowed additional data; semantics are not established.</summary>
        internal IntPtr AdditionalData;

        #endregion
    }
}
