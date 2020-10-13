using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    [StructLayout(LayoutKind.Sequential)]
    internal abstract class SafeStructArrayOut<T> : SafeArray
    {
        protected SafeStructArrayOut()
        {
        }

        public T[] Data
        {
            get
            {
                if (_nativeArray == IntPtr.Zero || _length <= 0)
                {
                    return null;
                }

                // Allocate a managed array
                T[] managedArray = new T[_length];

                // Marshal items one-by-one
                int itemSize = Marshal.SizeOf<T>();
                for (int i = 0; i < _length; i++)
                {
                    managedArray[i] = Marshal.PtrToStructure<T>(_nativeArray + (itemSize * i));
                }

                return managedArray;
            }
        }
    }


    [StructLayout(LayoutKind.Sequential)]
    internal class SafeStructArrayIn<T> : SafeArray, IDisposable
    {
        public SafeStructArrayIn(T[] items)
        {
            if ((items?.Length ?? 0) <= 0)
            {
                return;
            }

            _length = items.Length;

            // Allocate memory
            int itemSize = Marshal.SizeOf<T>();
            int arraySize = _length * itemSize;
            _nativeArray = Marshal.AllocHGlobal(arraySize);

            // Copy items
            for (int i = 0; i < _length; i++)
            {
                Marshal.StructureToPtr<T>(items[i], _nativeArray + (itemSize * i), false);
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
