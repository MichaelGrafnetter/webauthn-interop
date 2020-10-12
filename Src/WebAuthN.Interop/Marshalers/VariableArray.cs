using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal abstract class VariableArray<T>
    {
        protected int _length = 0;
        protected IntPtr _nativeArray = IntPtr.Zero;

        protected VariableArray()
        {
        }

        public virtual T[] Data
        {
            get
            {
                if (_nativeArray == IntPtr.Zero)
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
    internal sealed class VariableByteArrayOut : VariableArray<byte>
    {
        private VariableByteArrayOut() : base() { }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class VariableArrayIn<T> : VariableArray<T>, IDisposable
    {
        public VariableArrayIn(T[] data)
        {
            if (data != null)
            {
                _length = data.Length;
                InitializePointer(data);
            }
        }

        protected virtual void InitializePointer(T[] data)
        {
            // Allocate memory
            int itemSize = Marshal.SizeOf<T>();
            int arraySize = data.Length * itemSize;
            _nativeArray = Marshal.AllocHGlobal(arraySize);

            // Copy items
            for (int i = 0; i < data.Length; i++)
            {
                Marshal.StructureToPtr<T>(data[i], _nativeArray + (itemSize * i), false);
            }
        }

        public virtual void Dispose()
        {
            if (_nativeArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_nativeArray);
                _nativeArray = IntPtr.Zero;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class VariableByteArrayIn : VariableArrayIn<byte>
    {
        public VariableByteArrayIn(byte[] data) : base(data) { }

        public override byte[] Data
        {
            get
            {
                if (_nativeArray == IntPtr.Zero)
                {
                    return null;
                }

                byte[] managedArray = new byte[_length];
                Marshal.Copy(_nativeArray, managedArray, 0, _length);

                return managedArray;
            }
        }

        protected override void InitializePointer(byte[] data)
        {
            _nativeArray = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, _nativeArray, data.Length);
        }
    }
}
