using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal abstract class SafeArray
    {
        protected int _length = 0;
        protected IntPtr _nativeArray = IntPtr.Zero;

        protected SafeArray()
        {
        }
    }
}
