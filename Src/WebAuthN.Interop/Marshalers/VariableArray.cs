using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal abstract class VariableArrayOut<T>
    {
        protected int _length = 0;
        protected IntPtr _nativeArray = IntPtr.Zero;

        protected VariableArrayOut()
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
    internal sealed class VariableByteArrayOut : VariableArrayOut<byte>
    {
        private VariableByteArrayOut() : base() { }

        public override byte[] Data
        {
            get
            {
                if(_nativeArray == IntPtr.Zero)
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
    internal abstract class VariableArrayIn<T>
    {
        private int _length = 0;
        public T[] Data { get; private set; } = null;

        public VariableArrayIn(T[] data)
        {
            if (data != null)
            {
                _length = data.Length;
                Data = data;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class VariableByteArrayIn : VariableArrayIn<byte>
    {
        public VariableByteArrayIn(byte[] data) : base(data) { }
    }
}
