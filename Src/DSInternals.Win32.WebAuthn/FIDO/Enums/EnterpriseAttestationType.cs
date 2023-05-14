using System;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Enterprise Attestation Capabilities
    /// </summary>
    /// <see>https://fidoalliance.org/specs/fido-v2.1-rd-20201208/fido-client-to-authenticator-protocol-v2.1-rd-20201208.html#sctn-feature-descriptions-enterp-attstn</see>
    [Flags]
    public enum EnterpriseAttestationType : int
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_NONE.
        /// </remarks>
        None = ApiConstants.EnterpriseAttestationNone,

        ///<summary>
        ///Vendor-facilitated enterprise attestation
        ///</summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_VENDOR_FACILITATED.
        /// </remarks>
        VendorFacilitated = ApiConstants.EnterpriseAttestationVendorFacilitated,

        /// <summary>
        /// Platform-managed enterprise attestation
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_ENTERPRISE_ATTESTATION_PLATFORM_MANAGED.
        /// </remarks>
        PlatformManaged = ApiConstants.EnterpriseAttestationPlatformManaged
    }
}
