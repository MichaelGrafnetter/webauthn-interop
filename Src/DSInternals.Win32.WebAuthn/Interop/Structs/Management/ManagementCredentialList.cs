using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Contains the count and pointer array of a private display-credential list.</summary>
    /// <remarks>A Windows allocation layout, not the CTAP2 credential enumeration response map.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ManagementCredentialList
    {
        /// <summary>Number of record pointers, not inline records.</summary>
        internal uint Count;

        /// <summary>Borrowed pointer array; every record belongs to the outer list allocation.</summary>
        internal IntPtr Records;
    }
}
