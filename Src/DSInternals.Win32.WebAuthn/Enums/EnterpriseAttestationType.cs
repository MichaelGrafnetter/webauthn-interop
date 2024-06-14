using System;
using Windows.Win32;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Enterprise Attestation Capabilities
    /// </summary>
    /// <see>https://fidoalliance.org/specs/fido-v2.1-rd-20201208/fido-client-to-authenticator-protocol-v2.1-rd-20201208.html#sctn-feature-descriptions-enterp-attstn</see>
    [Flags]
    public enum EnterpriseAttestationType : uint
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_NONE.
        /// </remarks>
        None = PInvoke.WEBAUTHN_ENTERPRISE_ATTESTATION_NONE,

        ///<summary>
        ///Vendor-facilitated enterprise attestation
        ///</summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_VENDOR_FACILITATED.
        /// </remarks>
        VendorFacilitated = PInvoke.WEBAUTHN_ENTERPRISE_ATTESTATION_VENDOR_FACILITATED,

        /// <summary>
        /// Platform-managed enterprise attestation
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_PLATFORM_MANAGED.
        /// </remarks>
        PlatformManaged = PInvoke.WEBAUTHN_ENTERPRISE_ATTESTATION_PLATFORM_MANAGED
    }
}
