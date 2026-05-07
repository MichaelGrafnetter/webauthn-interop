using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;
using Windows.Win32;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Defines hints as to how clients might communicate with a particular authenticator in order to obtain an assertion for a specific credential.
    /// </summary>
    /// <see href="https://www.w3.org/TR/webauthn-3/#enum-transport"/>
    [Flags]
    [JsonConverter(typeof(WebAuthnJsonEnumConverter<AuthenticatorTransport>))]
    public enum AuthenticatorTransport : uint
    {
        /// <summary>
        /// No transport restrictions.
        /// </summary>
        NoRestrictions = 0,

        /// <summary>
        /// Universal Serial Bus (USB).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB.</remarks>
        [JsonStringEnumMemberName(ApiConstants.CtapTransportUsb)]
        USB = PInvoke.WEBAUTHN_CTAP_TRANSPORT_USB,

        /// <summary>
        /// Near Field Communication (NFC).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC.</remarks>
        [JsonStringEnumMemberName(ApiConstants.CtapTransportNfc)]
        NFC = PInvoke.WEBAUTHN_CTAP_TRANSPORT_NFC,

        /// <summary>
        /// Bluetooth Low Energy (BLE).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE.</remarks>
        [JsonStringEnumMemberName(ApiConstants.CtapTransportBle)]
        BLE = PInvoke.WEBAUTHN_CTAP_TRANSPORT_BLE,

        /// <summary>
        /// Reserved for testing.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_TEST.</remarks>
        [JsonStringEnumMemberName("test")]
        Test = PInvoke.WEBAUTHN_CTAP_TRANSPORT_TEST,

        /// <summary>
        /// Client device-specific transport (platform authenticator).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL.</remarks>
        [JsonStringEnumMemberName(ApiConstants.CtapTransportInternal)]
        Internal = PInvoke.WEBAUTHN_CTAP_TRANSPORT_INTERNAL,

        /// <summary>
        /// Hybrid Transport (QR Code / caBLE).
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_HYBRID.</remarks>
        [JsonStringEnumMemberName(ApiConstants.CtapTransportHybrid)]
        Hybrid = PInvoke.WEBAUTHN_CTAP_TRANSPORT_HYBRID,

        /// <summary>
        /// Smart card transport.
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CTAP_TRANSPORT_SMART_CARD.</remarks>
        [JsonStringEnumMemberName(ApiConstants.CtapTransportSmartCard)]
        SmartCard = PInvoke.WEBAUTHN_CTAP_TRANSPORT_SMART_CARD
    }
}
