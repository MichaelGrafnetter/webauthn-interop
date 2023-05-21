using System;
using System.Runtime.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.
    /// </summary>
    /// <see>https://www.w3.org/TR/webauthn/#enum-transport</see>
    [Flags]
    public enum AuthenticatorTransport : int
    {
        /// <summary>
        /// No transport restrictions.
        /// </summary>
        [EnumMember(Value = "NULL")]
#pragma warning disable CA1008 // Enums should have zero value
        NoRestrictions = 0,
#pragma warning restore CA1008 // Enums should have zero value

        /// <summary>
        /// Universal Serial Bus (USB).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB.</remarks>
        [EnumMember(Value = "usb")]
        USB = ApiConstants.CtapTransportUsb,

        /// <summary>
        /// Near Field Communication (NFC).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC.</remarks>
        [EnumMember(Value = "nfc")]
        NFC = ApiConstants.CtapTransportNfc,

        /// <summary>
        /// Bluetooth Low Energy (BLE).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE.</remarks>
        [EnumMember(Value = "ble")]
        BLE = ApiConstants.CtapTransportBle,

        /// <summary>
        /// Hybrid Transport (QR Code).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_HYBRID.</remarks>
        [EnumMember(Value = "hybrid")]
        Hybrid = ApiConstants.CtapTransportHybrid,

        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_TEST.</remarks>
        Test = ApiConstants.CtapTransportTest,

        /// <summary>
        /// Client device-specific transport.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL.</remarks>
        [EnumMember(Value = "internal")]
        Internal = ApiConstants.CtapTransportInternal
    }
}
