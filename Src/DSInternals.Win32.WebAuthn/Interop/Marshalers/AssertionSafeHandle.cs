using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    internal sealed class AssertionSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
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
                    sourceStructSize = Marshal.SizeOf<Assertion>();
                    break;
            }

            return VersionedStructMarshaler.PtrToStructure<Assertion>(handle, sourceStructSize);
        }
    }
}
