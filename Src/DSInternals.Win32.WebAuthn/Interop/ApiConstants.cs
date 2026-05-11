using System;
using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Contains WebAuthn API constants.
    /// </summary>
    public static class ApiConstants
    {
        /// <summary>
        /// WebAuthn operation type used when creating new FIDO2 credentials.
        /// </summary>
        public const string ClientDataCredentialCreate = "webauthn.create";

        /// <summary>
        /// The client data type for WebAuthn assertion (authentication) operations.
        /// </summary>
        public const string ClientDataCredentialGet = "webauthn.get";

        /// <summary>
        /// WebAuthn operation type used when creating new FIDO U2F credentials.
        /// </summary>
        public const string ClientDataCredentialCreateU2F = "navigator.id.finishEnrollment";

        /// <summary>
        /// WebAuthn operation type used for authentication with FIDO U2F credentials.
        /// </summary>
        public const string ClientDataCredentialGetU2F = "navigator.id.getAssertion";

        /// <summary>
        /// The authenticator attachment value for platform authenticators.
        /// </summary>
        public const string AuthenticatorAttachmentPlatform = "platform";

        /// <summary>
        /// The authenticator attachment value for cross-platform (roaming) authenticators.
        /// </summary>
        public const string AuthenticatorAttachmentCrossPlatform = "cross-platform";

        /// <summary>
        /// Default timeout for WebAuthn operations.
        /// </summary>
        public const uint DefaultTimeoutMilliseconds = 60000;

        /// <summary>
        /// Default length of the random challenges in bytes.
        /// </summary>
        public const int DefaultChallengeLength = 128;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_ONE_HMAC_SECRET_LENGTH.
        /// </remarks>
        public const uint CtapOneHmacSecretLength = PInvoke.WEBAUTHN_CTAP_ONE_HMAC_SECRET_LENGTH;

        /// <summary>
        /// The credential type for public-key credentials.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_TYPE_PUBLIC_KEY.
        /// </remarks>
        public const string PublicKeyCredentialType = PInvoke.WEBAUTHN_CREDENTIAL_TYPE_PUBLIC_KEY;

        /// <remarks>
        /// Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_256.
        /// </remarks>
        public const string HashAlgorithmSha256 = PInvoke.WEBAUTHN_HASH_ALGORITHM_SHA_256;

        /// <remarks>
        /// Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_384.
        /// </remarks>
        public const string HashAlgorithmSha384 = PInvoke.WEBAUTHN_HASH_ALGORITHM_SHA_384;

        /// <remarks>
        /// Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_512.
        /// </remarks>
        public const string HashAlgorithmSha512 = PInvoke.WEBAUTHN_HASH_ALGORITHM_SHA_512;

        /// <remarks>
        /// Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT.
        /// </remarks>
        public const string ExtensionIdentifierCredProtect = PInvoke.WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT;

        /// <remarks>
        /// Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_BLOB.
        /// </remarks>
        public const string ExtensionIdentifierCredBlob = PInvoke.WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_BLOB;

        /// <remarks>
        /// Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_MIN_PIN_LENGTH.
        /// </remarks>
        public const string ExtensionIdentifierMinPinLength = PInvoke.WEBAUTHN_EXTENSIONS_IDENTIFIER_MIN_PIN_LENGTH;

        /// <remarks>
        /// Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET.
        /// </remarks>
        public const string ExtensionIdentifierHmacSecret = PInvoke.WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_TYPE_PACKED.
        /// </remarks>
        public const string AttestationTypePacked = PInvoke.WEBAUTHN_ATTESTATION_TYPE_PACKED;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_TYPE_U2F.
        /// </remarks>
        public const string AttestationTypeU2F = PInvoke.WEBAUTHN_ATTESTATION_TYPE_U2F;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_TYPE_TPM.
        /// </remarks>
        public const string AttestationTypeTpm = PInvoke.WEBAUTHN_ATTESTATION_TYPE_TPM;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_TYPE_NONE.
        /// </remarks>
        public const string AttestationTypeNone = PInvoke.WEBAUTHN_ATTESTATION_TYPE_NONE;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_VER_TPM_2_0.
        /// </remarks>
        public const string AttestationVersionTpm20 = PInvoke.WEBAUTHN_ATTESTATION_VER_TPM_2_0;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB_STRING.
        /// </remarks>
        public const string CtapTransportUsb = PInvoke.WEBAUTHN_CTAP_TRANSPORT_USB_STRING;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC_STRING.
        /// </remarks>
        public const string CtapTransportNfc = PInvoke.WEBAUTHN_CTAP_TRANSPORT_NFC_STRING;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE_STRING.
        /// </remarks>
        public const string CtapTransportBle = PInvoke.WEBAUTHN_CTAP_TRANSPORT_BLE_STRING;

        /// <summary>
        /// The transport value indicating a platform (internal) authenticator.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL_STRING.
        /// </remarks>
        public const string CtapTransportInternal = PInvoke.WEBAUTHN_CTAP_TRANSPORT_INTERNAL_STRING;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_HYBRID_STRING.
        /// </remarks>
        public const string CtapTransportHybrid = PInvoke.WEBAUTHN_CTAP_TRANSPORT_HYBRID_STRING;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_SMART_CARD_STRING.
        /// </remarks>
        public const string CtapTransportSmartCard = PInvoke.WEBAUTHN_CTAP_TRANSPORT_SMART_CARD_STRING;

        /// <summary>
        /// The credential hint value for security keys (roaming authenticators).
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_HINT_SECURITY_KEY.
        /// </remarks>
        public const string CredentialHintSecurityKey = PInvoke.WEBAUTHN_CREDENTIAL_HINT_SECURITY_KEY;

        /// <summary>
        /// The credential hint value for client devices (platform authenticators).
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_HINT_CLIENT_DEVICE.
        /// </remarks>
        public const string CredentialHintClientDevice = PInvoke.WEBAUTHN_CREDENTIAL_HINT_CLIENT_DEVICE;

        /// <summary>
        /// The credential hint value for hybrid (QR code) authenticators.
        /// </summary>
        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_HINT_HYBRID.
        /// </remarks>
        public const string CredentialHintHybrid = PInvoke.WEBAUTHN_CREDENTIAL_HINT_HYBRID;

        /// <summary>
        /// The mediation requirement value indicating that the user agent must not show any UI to the user.
        /// </summary>
        public const string MediationSilent = "silent";

        /// <summary>
        /// The mediation requirement value indicating that the user agent may show UI to the user (default behavior).
        /// </summary>
        public const string MediationOptional = "optional";

        /// <summary>
        /// The mediation requirement value indicating conditional mediation (passkey autofill).
        /// </summary>
        public const string MediationConditional = "conditional";

        /// <summary>
        /// The mediation requirement value indicating that the user agent must always show UI to the user.
        /// </summary>
        public const string MediationRequired = "required";

        /// <summary>
        /// The AAGUID of the KeePassXC authenticator.
        /// </summary>
        public static readonly Guid KeePassXCAaGuid = new("fdb141b2-5d84-443e-8a35-4698c205a502");

        /// <summary>
        /// The AAGUID of the Bitwarden authenticator.
        /// </summary>
        public static readonly Guid BitwardenAaGuid = new("d548826e-79b4-db40-a3d8-11116f7e8349");
    }
}
