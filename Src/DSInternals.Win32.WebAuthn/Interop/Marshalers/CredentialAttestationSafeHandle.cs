using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
#if NET5_0
    [SupportedOSPlatform("windows")]
#endif
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
                    sourceStructSize = Marshal.SizeOf<CredentialAttestationV1>();
                    break;
                case CredentialAttestationVersion.Version2:
                    sourceStructSize = Marshal.SizeOf<CredentialAttestationV2>();
                    break;
                case CredentialAttestationVersion.Version3:
                    sourceStructSize = Marshal.SizeOf<CredentialAttestationV3>();
                    break;
                case CredentialAttestationVersion.Version4:
                    sourceStructSize = Marshal.SizeOf<CredentialAttestationV4>();
                    break;
                case CredentialAttestationVersion.Version5:
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
