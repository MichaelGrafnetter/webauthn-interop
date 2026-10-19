using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Extends the version-3 private credential display record with four reserved fields.</summary>
    /// <remarks>The reserved fields have no established correspondence to CTAP2 credential-management response members.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ManagementCredentialV4
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

        #region V4 members

        /// <summary>First reserved 64-bit field following the version-3 record.</summary>
        internal ulong Unknown48;

        /// <summary>Second reserved 64-bit field following the version-3 record.</summary>
        internal ulong Unknown50;

        /// <summary>Third reserved 64-bit field following the version-3 record.</summary>
        internal ulong Unknown58;

        /// <summary>Fourth reserved 64-bit field following the version-3 record.</summary>
        internal ulong Unknown60;

        #endregion
    }
}
