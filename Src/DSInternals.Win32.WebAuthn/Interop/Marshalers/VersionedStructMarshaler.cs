using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    internal static class VersionedStructMarshaler
    {
        public static T PtrToStructure<T>(IntPtr ptr, int sourceStructSize) where T : class
        {
            if (ptr == IntPtr.Zero || sourceStructSize == 0)
            {
                return null;
            }

            if(sourceStructSize < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(sourceStructSize));
            }

            int targetStructSize = Marshal.SizeOf<T>();

            if (sourceStructSize >= targetStructSize)
            {
                // Structure formats are incremental, so it does not matter if the source structure is larger.
                return Marshal.PtrToStructure<T>(ptr);
            }
            else
            {
                // We first need to copy the native structure to a larger zero-filled buffer
                byte[] buffer = new byte[targetStructSize];
                Marshal.Copy(ptr, buffer, 0, sourceStructSize);
                var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                try
                {
                    return Marshal.PtrToStructure<T>(bufferHandle.AddrOfPinnedObject());
                }
                finally
                {
                    bufferHandle.Free();
                }
            }
        }
    }
}
