using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    internal sealed class CredentialAttestationSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
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
                    sourceStructSize = Marshal.SizeOf<CredentialAttestation>();
                    break;
            }

            return VersionedStructMarshaler.PtrToStructure<CredentialAttestation>(handle, sourceStructSize);
        }
    }
}
