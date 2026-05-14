using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
    internal sealed class PlatformCredentialListSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private PlatformCredentialListSafeHandle() : base(true) { }

        protected override bool ReleaseHandle()
        {
            NativeMethods.FreePlatformCredentialList(this.handle);
            return true;
        }

        internal CredentialDetailsOut[]? ToManaged()
        {
            return Marshal.PtrToStructure<CredentialDetailsList>(handle)?.Items;
        }
    }
}
