using System;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.
    /// </summary>
    [Flags]
    internal enum CtapTransport : int
    {
        /// <summary>
        /// No transport restrictions.
        /// </summary>
        NoRestrictions = 0,

        /// <summary>
        /// Universal Serial Bus (USB).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB.</remarks>
        USB = ApiConstants.CtapTransportUsb,

        /// <summary>
        /// Near Field Communication (NFC).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC.</remarks>
        NFC = ApiConstants.CtapTransportNfc,

        /// <summary>
        /// Bluetooth Low Energy (BLE).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE.</remarks>
        BLE = ApiConstants.CtapTransportBle,

        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_TEST.</remarks>
        Test = ApiConstants.CtapTransportTest,

        /// <summary>
        /// Client device-specific transport.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL.</remarks>
        Internal = ApiConstants.CtapTransportInternal
    }
}
