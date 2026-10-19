using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Owns a selected management context released by WebAuthNCtapManageClose.</summary>
    internal class ManagementContextSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>Creates an empty owning context handle for native selection output.</summary>
        public ManagementContextSafeHandle() : base(true) { }

        /// <summary>Copies the versioned context into an independent managed capability snapshot.</summary>
        /// <remarks>The caller must serialize this read with native context refreshes.</remarks>
        /// <returns>A snapshot preserving unreported policy values as null.</returns>
        internal AuthenticatorManagementCapabilities ToManaged() => this.Read(pointer =>
        {
            uint version = unchecked((uint)Marshal.ReadInt32(pointer));
            if (version != 1 && version != 2)
            {
                throw new NotSupportedException($"Private management context version {version} is unsupported.");
            }

            var prefix = Marshal.PtrToStructure<ManagementContextV1>(pointer);
            var result = new AuthenticatorManagementCapabilities
            {
                ContextVersion = version,
                Transport = prefix.Transport,
                PinState = prefix.PinState,
                PinRetries = prefix.PinRetries,
                FingerprintState = prefix.FingerprintState,
                FingerprintKind = prefix.FingerprintKind,
                MaximumFingerprintCaptureSamples = prefix.MaximumFingerprintCaptureSamples,
                CredentialManagementState = prefix.CredentialManagementState
            };
            if (version == 2)
            {
                var context = Marshal.PtrToStructure<ManagementContextV2>(pointer);
                result.PinPolicy = new AuthenticatorPinPolicy
                {
                    AuthenticatorConfigSupported = context.AuthenticatorConfig,
                    EnterpriseAttestationEnabled = context.EnterpriseAttestation,
                    AlwaysUvEnabled = context.AlwaysUv,
                    MinimumPinLength = context.MinimumPinLength,
                    ForcePinChangeRequired = context.ForcePinChange,
                    SetMinPinLengthSupported = context.SetMinPinLength,
                    MaximumRpIds = context.MaximumRpIds,
                    MaximumLargeBlobBytes = context.MaximumLargeBlobBytes
                };
                result.MaximumMessageSize = context.MaximumMessageSize;
                result.RemainingDiscoverableCredentials = context.RemainingCredentials;
            }
            return result;
        });

        /// <summary>Closes the selected context and releases its interior allocations.</summary>
        /// <returns>True after closing the context.</returns>
        protected override bool ReleaseHandle()
        {
            NativeMethods.CtapManageClose(handle);
            return true;
        }
    }
}
