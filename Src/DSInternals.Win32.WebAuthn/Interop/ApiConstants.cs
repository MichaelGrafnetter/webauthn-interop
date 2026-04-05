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
        /// WebAuthn operation type used when creating new FIDO U2F credentials.
        /// </summary>
        public const string ClientDataCredentialCreateU2F = "navigator.id.finishEnrollment";

        /// <summary>
        /// WebAuthn operation type used for authentication with FIDO2 credentials.
        /// </summary>
        public const string ClientDataCredentialGet = "webauthn.get";

        /// <summary>
        /// WebAuthn operation type used for authentication with FIDO U2F credentials.
        /// </summary>
        public const string ClientDataCredentialGetU2F = "navigator.id.getAssertion";

        /// <summary>
        /// Default timeut for WebAuthn operations.
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

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_HINT_SECURITY_KEY.
        /// </remarks>
        public const string CredentialHintSecurityKey = PInvoke.WEBAUTHN_CREDENTIAL_HINT_SECURITY_KEY;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_HINT_CLIENT_DEVICE.
        /// </remarks>
        public const string CredentialHintClientDevice = PInvoke.WEBAUTHN_CREDENTIAL_HINT_CLIENT_DEVICE;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_HINT_HYBRID.
        /// </remarks>
        public const string CredentialHintHybrid = PInvoke.WEBAUTHN_CREDENTIAL_HINT_HYBRID;
    }
}
