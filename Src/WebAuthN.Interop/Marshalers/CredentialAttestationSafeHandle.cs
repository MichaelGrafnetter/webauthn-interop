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

        internal CredentialAttestation Marshal()
        {
            if(this.IsInvalid)
            {
                return null;
            }

            return System.Runtime.InteropServices.Marshal.PtrToStructure<CredentialAttestation>(this.handle);
        }
    }
}
