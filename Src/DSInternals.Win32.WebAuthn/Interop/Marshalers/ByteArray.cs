
using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal class ByteArrayOut
    {
        protected IntPtr _nativeArray = IntPtr.Zero;

        // This class is only created by marshaling.
        protected ByteArrayOut() { }

        public byte[] Read(int length)
        {
            if (_nativeArray == IntPtr.Zero ||length <= 0)
            {
                return null;
            }

            byte[] managedArray = new byte[length];
            Marshal.Copy(_nativeArray, managedArray, 0, length);

            return managedArray;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class ByteArrayIn : ByteArrayOut, IDisposable
    {
        public ByteArrayIn(byte[] data)
        {
            if((data?.Length ?? 0) > 0)
            {
                _nativeArray = Marshal.AllocHGlobal(data.Length);
                Marshal.Copy(data, 0, _nativeArray, data.Length);
            }
        }

        public void Dispose()
        {
            if (_nativeArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_nativeArray);
                _nativeArray = IntPtr.Zero;
            }
        }
    }
}
