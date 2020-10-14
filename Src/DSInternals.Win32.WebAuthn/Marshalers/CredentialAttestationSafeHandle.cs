using System;
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

            // Handle possible older structure versions
            var version = (CredentialAttestationVersion) Marshal.ReadInt32(this.handle);

            int sourceStructSize;
            int targetStructSize = Marshal.SizeOf<CredentialAttestation>();
            switch(version)
            {
                case CredentialAttestationVersion.Version1:
                    sourceStructSize = targetStructSize - (Marshal.SizeOf<ExtensionsOut>() + sizeof(CtapTransport));
                    break;
                case CredentialAttestationVersion.Version2:
                    sourceStructSize = targetStructSize - sizeof(CtapTransport);
                    break;
                case CredentialAttestationVersion.Version3:
                default:
                    sourceStructSize = targetStructSize;
                    break;
            }

            if (sourceStructSize >= targetStructSize)
            {
                // Structure formats are incremental, so it does not matter if the source structure is larger.
                return Marshal.PtrToStructure<CredentialAttestation>(handle);
            }
            else
            {
                // We first need to copy the native structure to a larger zero-filled buffer
                byte[] buffer = new byte[targetStructSize];
                Marshal.Copy(handle, buffer, 0, sourceStructSize);
                var bufferHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned);

                try
                {
                    return Marshal.PtrToStructure<CredentialAttestation>(bufferHandle.AddrOfPinnedObject());
                }
                finally
                {
                    bufferHandle.Free();
                }
            }
        }
    }
}
