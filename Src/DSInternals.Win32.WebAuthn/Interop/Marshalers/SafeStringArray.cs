using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal class SafeStringArrayIn : IDisposable
    {
        protected int _length;
        protected IntPtr _nativeArray;

        public SafeStringArrayIn(string[]? items)
        {
            if (items == null || items.Length == 0)
            {
                return;
            }

            _length = items.Length;

            // Allocate memory
            int ptrSize = Marshal.SizeOf<IntPtr>();
            int arraySize = _length * ptrSize;
            _nativeArray = Marshal.AllocHGlobal(arraySize);

            // Copy items
            for (int i = 0; i < _length; i++)
            {
                IntPtr stringPtr = Marshal.StringToHGlobalUni(items[i]);
                Marshal.WriteIntPtr(_nativeArray + (ptrSize * i), stringPtr);
            }
        }

        public string[]? Items
        {
            get
            {
                if (_length == 0 || _nativeArray == IntPtr.Zero)
                {
                    return null;
                }

                string[] items = new string[_length];
                for (int i = 0; i < _length; i++)
                {
                    IntPtr stringPtr = Marshal.ReadIntPtr(_nativeArray + (Marshal.SizeOf<IntPtr>() * i));
                    items[i] = Marshal.PtrToStringUni(stringPtr) ?? string.Empty;
                }

                return items;
            }
        }

        public void Dispose()
        {
            if (_nativeArray != IntPtr.Zero)
            {
                for (int i = 0; i < _length; i++)
                {
                    IntPtr stringPtr = Marshal.ReadIntPtr(_nativeArray + (Marshal.SizeOf<IntPtr>() * i));
                    if (stringPtr != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(stringPtr);
                    }
                }

                Marshal.FreeHGlobal(_nativeArray);
                _nativeArray = IntPtr.Zero;
            }
        }
    }
}
