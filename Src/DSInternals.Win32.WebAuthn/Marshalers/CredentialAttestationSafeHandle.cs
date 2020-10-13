using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn
{
    internal class CredentialAttestationSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        private CredentialAttestationSafeHandle() : base(true) { }

        protected override bool ReleaseHandle()
        {
            NativeMethods.FreeCredentialAttestation(this.handle);
            return true;
        }

        internal CredentialAttestation ToManaged()
        {
            if (this.IsInvalid)
            {
                return null;
            }

            return Marshal.PtrToStructure<CredentialAttestation>(this.handle);
        }
    }
}
