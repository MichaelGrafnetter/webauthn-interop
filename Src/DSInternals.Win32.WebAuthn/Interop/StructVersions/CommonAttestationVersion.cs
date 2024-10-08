﻿using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// The version of the WEBAUTHN_COMMON_ATTESTATION structure, to allow for modifications in the future.
    /// </summary>
    internal enum CommonAttestationVersion : uint
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_COMMON_ATTESTATION_CURRENT_VERSION.
        /// </remarks>
        Current = PInvoke.WEBAUTHN_COMMON_ATTESTATION_CURRENT_VERSION
    }
}
