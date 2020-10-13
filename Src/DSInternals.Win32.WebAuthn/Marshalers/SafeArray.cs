using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
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
