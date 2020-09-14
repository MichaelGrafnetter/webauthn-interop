using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Marshaler for native array pointers prefixed by array length.
    /// </summary>
    internal class BufferMarshaler : ICustomMarshaler
    {
        /// <summary>
        /// Performs necessary cleanup of the managed data when it is no longer needed.
        /// </summary>
        public void CleanUpManagedData(object ManagedObj)
        {
            // Nothing to release.
        }

        /// <summary>
        /// Performs necessary cleanup of the unmanaged data when it is no longer needed.
        /// </summary>
        public void CleanUpNativeData(IntPtr pNativeData)
        {
            // TODO: Memory leak
            Marshal.FreeHGlobal(pNativeData);
        }

        /// <summary>
        /// Returns the size of the native data to be marshaled.
        /// </summary>
        public int GetNativeDataSize()
        {
            // Length + pointer to array
            return sizeof(int) + Marshal.SizeOf<IntPtr>();
        }

        /// <summary>
        /// Converts the managed data to unmanaged data.
        /// </summary>
        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            if (ManagedObj == null)
            {
                // Nothing to marshal.
                return IntPtr.Zero;
            }

            int structSize = sizeof(int) + Marshal.SizeOf<IntPtr>();
            IntPtr nativeBuffer = Marshal.AllocHGlobal(structSize);

            Marshal.StructureToPtr<>()

            var buffer = (Buffer) ManagedObj;
            // TODO: Check that data is not null

            Marshal.WriteInt32(nativeBuffer, buffer.Data.Length);

            IntPtr arrayAddress = nativeBuffer + sizeof(int);

            Marshal.






            Marshal.Copy(buffer.Data, 0, destination, buffer.Data.Length);

            // Allocate unmanaged space. For COM, use Marshal.AllocCoTaskMem instead.
            IntPtr ptr = Marshal.AllocHGlobal(size);
            // Write the "Length" field first
            Marshal.WriteInt32(ptr, array.Length);
            // Write the array data
            for (int i = 0; i < array.Length; i++)
            {   // Newly-allocated space has no existing object, so the last param is false
                Marshal.StructureToPtr<T>(array[i], ptr + sizeof(int) + (itemSize * i), false);
            }
            // If you're only using arrays of primitive types, you could use this instead:
            //Marshal.Copy(array, 0, ptr + sizeof(int), array.Length);
            return ptr;
        }

        /// <summary>
        /// Converts the unmanaged data to managed data.
        /// </summary>
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            if (pNativeData == IntPtr.Zero)
            {
                // Nothing to marshal
                return null;
            }

            // The first item is the length of the array
            int length = Marshal.ReadInt32(pNativeData);
            
            if(length <= 0)
            {
                // The array is empty.
                return null;
            }

            // Move to the first item in the array
            IntPtr arrayAddress = Marshal.ReadIntPtr(pNativeData + sizeof(int));

            if(arrayAddress == IntPtr.Zero)
            {
                // The array is empty, even though the length field says otherwise.
                return null;
            }

            // Allocate and populate the managed array
            var array = new byte[length];
            Marshal.Copy(arrayAddress, array, 0, length);

            return new Buffer(array);
        }

        /// <summary>
        /// Returns an instance of the custom marshaler.
        /// </summary>
        public static ICustomMarshaler GetInstance(String cookie)
        {
            return new BufferMarshaler();
        }
    }
}
