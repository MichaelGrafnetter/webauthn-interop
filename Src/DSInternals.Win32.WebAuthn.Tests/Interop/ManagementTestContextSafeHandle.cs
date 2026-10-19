using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    /// <summary>Owns synthetic context memory without invoking the Windows context deallocator.</summary>
    internal sealed class ManagementTestContextSafeHandle : ManagementContextSafeHandle
    {
        /// <summary>Takes ownership of a synthetic HGlobal context allocation.</summary>
        /// <param name="pointer">The allocated context address.</param>
        internal ManagementTestContextSafeHandle(IntPtr pointer)
        {
            SetHandle(pointer);
        }

        /// <summary>Gets the number of times the synthetic context was released.</summary>
        internal int ReleaseCount { get; private set; }

        /// <summary>Frees the synthetic context with its matching allocator.</summary>
        /// <returns>True after releasing the allocation.</returns>
        protected override bool ReleaseHandle()
        {
            ReleaseCount++;
            Marshal.FreeHGlobal(handle);
            return true;
        }
    }
}
