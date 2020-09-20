using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    internal abstract class SafeArrayHandle<T> : SafeHandleZeroOrMinusOneIsInvalid
    {
        internal SafeArrayHandle(T[] data)
        {

        }

        internal SafeArrayHandle() : base(false) { }

        internal SafeArrayHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
        {
            SetHandle(preexistingHandle);
        }

        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(this.handle);
            return true;
        }
    }
}
