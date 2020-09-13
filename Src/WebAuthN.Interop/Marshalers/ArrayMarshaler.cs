/*
 * Original author: CBHacking
 * Source: https://stackoverflow.com/questions/5902103/how-do-i-marshal-a-struct-that-contains-a-variable-sized-array-to-c
 */
using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Marshaler for native array pointers prefixed by array length.
    /// </summary>
    internal class ArrayMarshaler<T> : ICustomMarshaler
    {
        /// <summary>
        /// Returns an instance of the custom marshaler.
        /// </summary>
        public static ICustomMarshaler GetInstance(String cookie)
        {
            return new ArrayMarshaler<T>();
        }

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

            T[] array = (T[]) ManagedObj;

            int itemSize = Marshal.SizeOf<T>();



            // Get the total size of unmanaged memory that is needed (length + elements)
            int size = sizeof(int) + (itemSize * array.Length);

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

            // Allocate a managed array
            T[] array = new T[length];

            // Populate the array
            if(typeof(T) == typeof(byte))
            {
                // Fast copy for binary arrays
                byte[] binaryArray = array as byte[];
                Marshal.Copy(arrayAddress, binaryArray, 0, length);
            }
            else
            {
                // Marshal items one-by-one
                int itemSize = Marshal.SizeOf<T>();

                for (int i = 0; i < length; i++)
                {
                    array[i] = Marshal.PtrToStructure<T>(arrayAddress + (itemSize * i));
                }
            }

            return array;
        }
    }
}
