using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal abstract class SafeStructArrayOut<T>
    {
        protected int _length;
        protected IntPtr _nativeArray;

        protected SafeStructArrayOut()
        {
        }

        public T[]? Items
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
    internal abstract class SafeStructArrayIn<T> : IDisposable
    {
        protected int _length;
        protected IntPtr _nativeArray;

        public SafeStructArrayIn(T[]? items)
        {
            if (items == null || items.Length == 0)
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

        public bool IsEmpty => _length == 0 || _nativeArray == IntPtr.Zero;

        public int Length => _length;

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
