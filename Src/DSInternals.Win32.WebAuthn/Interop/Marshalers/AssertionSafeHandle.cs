using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
#if NET5_0
    [SupportedOSPlatform("windows")]
#endif
    internal class AssertionSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private AssertionSafeHandle() : base(true) { }

        protected override bool ReleaseHandle()
        {
            NativeMethods.FreeAssertion(this.handle);
            return true;
        }

        internal Assertion ToManaged()
        {
            if (this.IsInvalid)
            {
                return null;
            }

            // Handle possible older structure versions
            var version = (AssertionVersion) Marshal.ReadInt32(this.handle);
            int sourceStructSize;
            int targetStructSize = Marshal.SizeOf<Assertion>();

            switch(version)
            {
                case AssertionVersion.Version1:
                    sourceStructSize = Marshal.SizeOf<AssertionV1>();
                    break;
                case AssertionVersion.Version2:
                    sourceStructSize = Marshal.SizeOf<AssertionV2>();
                    break;
                case AssertionVersion.Version3:
                    sourceStructSize = Marshal.SizeOf<AssertionV3>();
                    break;
                case AssertionVersion.Version4:
                default:
                    sourceStructSize = targetStructSize;
                    break;
            }

            if (sourceStructSize >= targetStructSize)
            {
                // Structure formats are incremental, so it does not matter if the source structure is larger.
                return Marshal.PtrToStructure<Assertion>(this.handle);
            }
            else
            {
                // We first need to copy the native structure to a larger zero-filled buffer
                byte[] buffer = new byte[targetStructSize];
                Marshal.Copy(handle, buffer, 0, sourceStructSize);
                var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                try
                {
                    return Marshal.PtrToStructure<Assertion>(bufferHandle.AddrOfPinnedObject());
                }
                finally
                {
                    bufferHandle.Free();
                }
            }
        }
    }
}
