
using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    [StructLayout(LayoutKind.Sequential)]
    internal class SafeByteArrayOut : SafeArray
    {
        public byte[] Data
        {
            get
            {
                if (_nativeArray == IntPtr.Zero || _length <= 0)
                {
                    return null;
                }

                byte[] managedArray = new byte[_length];
                Marshal.Copy(_nativeArray, managedArray, 0, _length);

                return managedArray;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class SafeByteArrayIn : SafeByteArrayOut, IDisposable
    {
        public SafeByteArrayIn(byte[] data)
        {
            if((data?.Length ?? 0) > 0)
            {
                _length = data.Length;
                _nativeArray = Marshal.AllocHGlobal(_length);
                Marshal.Copy(data, 0, _nativeArray, _length);
            }
        }

        public void Dispose()
        {
            _length = 0;

            if (_nativeArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_nativeArray);
                _nativeArray = IntPtr.Zero;
            }
        }
    }
}
