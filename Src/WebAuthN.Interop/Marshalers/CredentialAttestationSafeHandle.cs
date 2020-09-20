using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
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
            if(this.IsInvalid)
            {
                return null;
            }

            return Marshal.PtrToStructure<CredentialAttestation>(this.handle);
        }
    }
}
