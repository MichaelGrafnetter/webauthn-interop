using System;
using System.Diagnostics.Contracts;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{

    internal class GenericBuffer<T> : SafeBuffer
    {
        public GenericBuffer(T[] array) : base(true)
        {
            if(array == null)
            {
                // TODO: Empty vs. null
                return;
            }

            Initialize(array);
            SetHandle(Marshal.AllocHGlobal(checked((int)ByteLength)));
            WriteArray(array);
        }

        internal GenericBuffer() : base(false) { }

        internal GenericBuffer(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
        {
            SetHandle(preexistingHandle);
        }

        public void Initialize(T[] array)
        {
            if (array != null)
            {
                int itemSize = Marshal.SizeOf<T>();
                int numItems = array.Length;
                Initialize(checked((ulong)(itemSize * numItems)));
            }
            else
            {
                Initialize(0);
            }
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public void WriteArray(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            int itemSize = Marshal.SizeOf<T>();
            if (checked((ulong)(array.Length * itemSize) > ByteLength))
                throw new ArgumentOutOfRangeException(nameof(array));
            Contract.EndContractBlock();

            if (typeof(T) == typeof(byte))
            {
                // Perform fast copy
                Marshal.Copy(array as byte[], 0, handle, array.Length);
            }
            else
            {
                // Copy structs one-by-one
                for (int i = 0; i < array.Length; i++)
                {
                    Marshal.StructureToPtr<T>(array[i], handle + (itemSize * i), false);
                }
            }
        }

        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public T[] ReadArray()
        {
            int itemSize = Marshal.SizeOf<T>();
            int numItems = checked((int)ByteLength / itemSize);
            Contract.EndContractBlock();

            // Allocate the managed array
            T[] array = new T[numItems];

            if (typeof(T) == typeof(byte))
            {
                // Perform fast copy
                Marshal.Copy(handle, array as byte[], 0, array.Length);
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = Marshal.PtrToStructure<T>(handle + (itemSize * i));
                }
            }

            return array;
        }

        protected override bool ReleaseHandle()
        {
            Marshal.FreeHGlobal(this.handle);
            return true;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public abstract class VariableArray<T> : IDisposable
    {
        protected int _length = 0;
        // TODO: Safe Handle
        protected IntPtr _nativeArray = IntPtr.Zero;

        public VariableArray(T[] data)
        {
            if(data != null)
            {
                this._length = data.Length;
                this.InitializePointer(data);
            }
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

        protected virtual void Dispose(bool disposing)
        {
            if (_nativeArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_nativeArray);
                _nativeArray = IntPtr.Zero;
            }
        }

        ~VariableArray()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class VariableByteArray : VariableArray<byte>
    {
        public VariableByteArray(byte[] data) : base(data)
        {
        }

        protected override void InitializePointer(byte[] data)
        {
            _nativeArray = Marshal.AllocHGlobal(data.Length);
            Marshal.Copy(data, 0, _nativeArray, data.Length);
        }

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
}
