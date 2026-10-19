using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Owns a COM-task-allocated serialized large-blob store returned by Windows.</summary>
    internal sealed class ManagementLargeBlobSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>Creates an empty owning buffer handle for native large-blob output.</summary>
        public ManagementLargeBlobSafeHandle() : base(true) { }

        /// <summary>Takes ownership of a supplied COM-task-allocated large-blob buffer.</summary>
        /// <param name="pointer">Buffer pointer whose ownership transfers to this handle.</param>
        internal ManagementLargeBlobSafeHandle(IntPtr pointer) : base(true)
        {
            SetHandle(pointer);
        }

        /// <summary>Releases the COM-task-allocated large-blob buffer.</summary>
        /// <returns>True after freeing the buffer.</returns>
        protected override bool ReleaseHandle()
        {
            // Verified in webauthn.dll 10.0.26100.9278: WebAuthNCtapManageGetLargeBlobs uses
            // CoTaskMemAlloc for the returned buffer; no dedicated large-blob free function is exported.
            Marshal.FreeCoTaskMem(handle);
            return true;
        }

        /// <summary>Copies the number of serialized-store bytes reported by Windows.</summary>
        /// <param name="length">The byte count returned by Windows.</param>
        /// <returns>An independent copy of the store and its checksum.</returns>
        internal byte[] ReadBytes(uint length)
        {
            int managedLength = checked((int)length);
            return this.Read(pointer =>
            {
                var data = new byte[managedLength];
                Marshal.Copy(pointer, data, 0, data.Length);
                return data;
            });
        }
    }
}
