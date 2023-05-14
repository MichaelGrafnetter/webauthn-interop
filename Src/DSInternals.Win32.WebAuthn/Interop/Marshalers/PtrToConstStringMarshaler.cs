using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    internal class PtrToConstStringMarshaler : ICustomMarshaler
    {
        public object MarshalNativeToManaged(IntPtr pNativeData)
        {
            return Marshal.PtrToStringUni(pNativeData);
        }

        public void CleanUpNativeData(IntPtr pNativeData)
        {
            // Constant strings cannot be freed.
        }

        public void CleanUpManagedData(object ManagedObj)
        {
            throw new NotSupportedException();
        }

        public int GetNativeDataSize()
        {
            throw new NotSupportedException();
        }

        public IntPtr MarshalManagedToNative(object ManagedObj)
        {
            throw new NotSupportedException();
        }

#pragma warning disable CA1801 // Review unused parameters
        /// <summary>
        /// Returns an instance of the custom marshaler.
        /// </summary>
        public static ICustomMarshaler GetInstance(String cookie)
        {
            return new PtrToConstStringMarshaler();
        }
#pragma warning restore CA1801 // Review unused parameters
    }
}
