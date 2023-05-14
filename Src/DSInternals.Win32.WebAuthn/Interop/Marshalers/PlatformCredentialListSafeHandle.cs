using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
#if NET5_0
    [SupportedOSPlatform("windows")]
#endif
    internal class PlatformCredentialListSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private PlatformCredentialListSafeHandle() : base(true) { }

        protected override bool ReleaseHandle()
        {
            NativeMethods.FreePlatformCredentialList(this.handle);
            return true;
        }
        /*
        internal Assertion ToManaged()
        {
            if (this.IsInvalid)
            {
                return null;
            }

            return Marshal.PtrToStructure<Assertion>(this.handle);
        }
        */
    }
}
