using System;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    internal abstract class VariableArray<T> : IDisposable
    {
        protected int length = 0;
        protected IntPtr nativeArray = IntPtr.Zero;

        public VariableArray(T[] data)
        {
            if(data != null)
            {
                this.length = data.Length;
                this.InitializePointer(data);
            }
        }

        protected VariableArray()
        {
        }

        public virtual T[] Data
        {
            get
            {
                if (nativeArray == IntPtr.Zero)
                {
                    return null;
                }

                // Allocate a managed array
                T[] managedArray = new T[length];

                // Marshal items one-by-one
                int itemSize = Marshal.SizeOf<T>();
                for (int i = 0; i < length; i++)
                {
                    managedArray[i] = Marshal.PtrToStructure<T>(nativeArray + (itemSize * i));
                }

                return managedArray;
            }
        }

        protected virtual void InitializePointer(T[] data)
        {
            // Allocate memory
            int itemSize = Marshal.SizeOf<T>();
            int arraySize = data.Length * itemSize;
            nativeArray = Marshal.AllocHGlobal(arraySize);

            // Copy items
            for (int i = 0; i < data.Length; i++)
            {
                Marshal.StructureToPtr<T>(data[i], nativeArray + (itemSize * i), false);
            }
        }

        public virtual void Dispose()
        {
            if (nativeArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeArray);
                nativeArray = IntPtr.Zero;
            }

            GC.SuppressFinalize(this);
        }

        ~VariableArray()
        {
            Dispose();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal class VariableByteArray : VariableArray<byte>
    {
        public VariableByteArray(byte[] data) : base(data)
        {
        }

        protected VariableByteArray() : base()
        {
        }

        protected override void InitializePointer(byte[] data)
        {
            nativeArray = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, nativeArray, data.Length);
        }

        public override byte[] Data
        {
            get
            {
                if(nativeArray == IntPtr.Zero)
                {
                    return null;
                }

                byte[] managedArray = new byte[length];
                Marshal.Copy(nativeArray, managedArray, 0, length);

                return managedArray;
            }
        }

        internal sealed class VariableByteArrayOut : VariableByteArray
        {
            private VariableByteArrayOut() : base()
            {
            }

            public override void Dispose()
            {
                // Do nothing, as the memory is managed by someone else.
            }
        }
    }
}
