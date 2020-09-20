using System;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.
    /// </summary>
    [Flags]
    public enum AuthenticatorTransport : int
    {
        /// <summary>
        /// No transport restrictions.
        /// </summary>
        NoRestrictions = 0,

        /// <summary>
        /// Universal Serial Bus (USB).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB.</remarks>
        USB = 0x00000001,

        /// <summary>
        /// Near Field Communication (NFC).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC.</remarks>
        NFC = 0x00000002,

        /// <summary>
        /// Bluetooth Low Energy (BLE).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE.</remarks>
        BLE = 0x00000004,

        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_TEST.</remarks>
        Test = 0x00000008,

        /// <summary>
        /// Client device-specific transport.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL.</remarks>
        Internal = 0x00000010
    }
}
