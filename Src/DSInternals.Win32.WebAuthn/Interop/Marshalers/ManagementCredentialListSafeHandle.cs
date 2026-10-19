using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Owns a private credential list released by WebAuthNCtapManageFreeDisplayCredentials.</summary>
    internal class ManagementCredentialListSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>Creates an empty owning list handle for native enumeration output.</summary>
        public ManagementCredentialListSafeHandle() : base(true) { }

        /// <summary>Copies the versioned display records into independent managed credentials.</summary>
        /// <returns>Managed credential records whose identifiers and strings remain valid after disposal.</returns>
        internal IReadOnlyList<DiscoverableCredentialDetails> ToManaged() => this.Read(pointer =>
        {
            var list = Marshal.PtrToStructure<ManagementCredentialList>(pointer);
            int count = checked((int)list.Count);
            if (count != 0 && list.Records == IntPtr.Zero)
            {
                throw new InvalidOperationException("The management API returned an invalid credential array.");
            }
            var results = new List<DiscoverableCredentialDetails>(count);
            for (int i = 0; i < count; i++)
            {
                IntPtr recordPointer = Marshal.ReadIntPtr(list.Records, checked(i * IntPtr.Size));
                if (recordPointer == IntPtr.Zero)
                {
                    throw new InvalidOperationException("The management API returned a null credential record.");
                }
                uint version = unchecked((uint)Marshal.ReadInt32(recordPointer));
                if (version < 1 || version > 4)
                {
                    throw new NotSupportedException($"Private display credential version {version} is unsupported.");
                }
                // All reviewed versions share this complete prefix. Unknown suffixes stay private.
                var record = Marshal.PtrToStructure<ManagementCredentialV1>(recordPointer);
                if (record.CredentialIdLength != 0 && record.CredentialId == IntPtr.Zero)
                {
                    throw new InvalidOperationException("The management API returned an invalid credential ID.");
                }
                var id = new byte[checked((int)record.CredentialIdLength)];
                if (id.Length != 0)
                {
                    Marshal.Copy(record.CredentialId, id, 0, id.Length);
                }
                results.Add(new DiscoverableCredentialDetails
                {
                    CredentialId = id,
                    RelyingPartyId = Marshal.PtrToStringUni(record.RelyingPartyId),
                    UserName = Marshal.PtrToStringUni(record.UserName),
                    UserDisplayName = Marshal.PtrToStringUni(record.DisplayName)
                });
            }
            return results;
        });

        /// <summary>Releases the credential list and its interior records.</summary>
        /// <returns>True after freeing the credential list.</returns>
        protected override bool ReleaseHandle()
        {
            NativeMethods.CtapManageFreeDisplayCredentials(handle);
            return true;
        }
    }
}
