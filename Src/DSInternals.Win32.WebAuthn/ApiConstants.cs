/* This file has been automatically generated. Do not modify it! */

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Contains constants from "webauthn.h".
    /// </summary>
    internal static partial class ApiConstants
    {
        /// <remarks>
        /// Corresponds to WEBAUTHN_API_VERSION_1.
        /// </remarks>
        public const int ApiVersion1 = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_API_VERSION_2.
        /// </remarks>
        public const int ApiVersion2 = 2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_API_CURRENT_VERSION.
        /// </remarks>
        public const int ApiCurrentVersion = ApiVersion2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_RP_ENTITY_INFORMATION_CURRENT_VERSION.
        /// </remarks>
        public const int RpEntityInformationCurrentVersion = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_MAX_USER_ID_LENGTH.
        /// </remarks>
        public const int MaxUserIdLength = 64;

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_ENTITY_INFORMATION_CURRENT_VERSION.
        /// </remarks>
        public const int UserEntityInformationCurrentVersion = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_256.
        /// </remarks>
        public const string HashAlgorithmSha256 = "SHA-256";

        /// <remarks>
        /// Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_384.
        /// </remarks>
        public const string HashAlgorithmSha384 = "SHA-384";

        /// <remarks>
        /// Corresponds to WEBAUTHN_HASH_ALGORITHM_SHA_512.
        /// </remarks>
        public const string HashAlgorithmSha512 = "SHA-512";

        /// <remarks>
        /// Corresponds to WEBAUTHN_CLIENT_DATA_CURRENT_VERSION.
        /// </remarks>
        public const int ClientDataCurrentVersion = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_TYPE_PUBLIC_KEY.
        /// </remarks>
        public const string CredentialTypePublicKey = "public-key";

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P256_WITH_SHA256.
        /// </remarks>
        public const int CoseAlgorithmEcdsaP256WithSha256 = -7;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P384_WITH_SHA384.
        /// </remarks>
        public const int CoseAlgorithmEcdsaP384WithSha384 = -35;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_ECDSA_P521_WITH_SHA512.
        /// </remarks>
        public const int CoseAlgorithmEcdsaP521WithSha512 = -36;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA256.
        /// </remarks>
        public const int CoseAlgorithmRsassaPkcs1V15WithSha256 = -257;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA384.
        /// </remarks>
        public const int CoseAlgorithmRsassaPkcs1V15WithSha384 = -258;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_RSASSA_PKCS1_V1_5_WITH_SHA512.
        /// </remarks>
        public const int CoseAlgorithmRsassaPkcs1V15WithSha512 = -259;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA256.
        /// </remarks>
        public const int CoseAlgorithmRsaPssWithSha256 = -37;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA384.
        /// </remarks>
        public const int CoseAlgorithmRsaPssWithSha384 = -38;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_ALGORITHM_RSA_PSS_WITH_SHA512.
        /// </remarks>
        public const int CoseAlgorithmRsaPssWithSha512 = -39;

        /// <remarks>
        /// Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETER_CURRENT_VERSION.
        /// </remarks>
        public const int CoseCredentialParameterCurrentVersion = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_CURRENT_VERSION.
        /// </remarks>
        public const int CredentialCurrentVersion = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_USB.
        /// </remarks>
        public const int CtapTransportUsb = 0x00000001;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_NFC.
        /// </remarks>
        public const int CtapTransportNfc = 0x00000002;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_BLE.
        /// </remarks>
        public const int CtapTransportBle = 0x00000004;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_TEST.
        /// </remarks>
        public const int CtapTransportTest = 0x00000008;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_INTERNAL.
        /// </remarks>
        public const int CtapTransportInternal = 0x00000010;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CTAP_TRANSPORT_FLAGS_MASK.
        /// </remarks>
        public const int CtapTransportFlagsMask = 0x0000001F;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_EX_CURRENT_VERSION.
        /// </remarks>
        public const int CredentialExCurrentVersion = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET.
        /// </remarks>
        public const string ExtensionsIdentifierHmacSecret = "hmac-secret";

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_ANY.
        /// </remarks>
        public const int UserVerificationAny = 0;

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_OPTIONAL.
        /// </remarks>
        public const int UserVerificationOptional = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_OPTIONAL_WITH_CREDENTIAL_ID_LIST.
        /// </remarks>
        public const int UserVerificationOptionalWithCredentialIdList = 2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIRED.
        /// </remarks>
        public const int UserVerificationRequired = 3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT.
        /// </remarks>
        public const string ExtensionsIdentifierCredProtect = "credProtect";

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_ANY.
        /// </remarks>
        public const int AuthenticatorAttachmentAny = 0;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_PLATFORM.
        /// </remarks>
        public const int AuthenticatorAttachmentPlatform = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM.
        /// </remarks>
        public const int AuthenticatorAttachmentCrossPlatform = 2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_ATTACHMENT_CROSS_PLATFORM_U2F_V2.
        /// </remarks>
        public const int AuthenticatorAttachmentCrossPlatformU2fV2 = 3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_ANY.
        /// </remarks>
        public const int UserVerificationRequirementAny = 0;

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_REQUIRED.
        /// </remarks>
        public const int UserVerificationRequirementRequired = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_PREFERRED.
        /// </remarks>
        public const int UserVerificationRequirementPreferred = 2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_USER_VERIFICATION_REQUIREMENT_DISCOURAGED.
        /// </remarks>
        public const int UserVerificationRequirementDiscouraged = 3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_ANY.
        /// </remarks>
        public const int AttestationConveyancePreferenceAny = 0;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_NONE.
        /// </remarks>
        public const int AttestationConveyancePreferenceNone = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_INDIRECT.
        /// </remarks>
        public const int AttestationConveyancePreferenceIndirect = 2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_CONVEYANCE_PREFERENCE_DIRECT.
        /// </remarks>
        public const int AttestationConveyancePreferenceDirect = 3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_1.
        /// </remarks>
        public const int AuthenticatorMakeCredentialOptionsVersion1 = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_2.
        /// </remarks>
        public const int AuthenticatorMakeCredentialOptionsVersion2 = 2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_VERSION_3.
        /// </remarks>
        public const int AuthenticatorMakeCredentialOptionsVersion3 = 3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_MAKE_CREDENTIAL_OPTIONS_CURRENT_VERSION.
        /// </remarks>
        public const int AuthenticatorMakeCredentialOptionsCurrentVersion = AuthenticatorMakeCredentialOptionsVersion3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_1.
        /// </remarks>
        public const int AuthenticatorGetAssertionOptionsVersion1 = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_2.
        /// </remarks>
        public const int AuthenticatorGetAssertionOptionsVersion2 = 2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3.
        /// </remarks>
        public const int AuthenticatorGetAssertionOptionsVersion3 = 3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4.
        /// </remarks>
        public const int AuthenticatorGetAssertionOptionsVersion4 = 4;

        /// <remarks>
        /// Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_CURRENT_VERSION.
        /// </remarks>
        public const int AuthenticatorGetAssertionOptionsCurrentVersion = AuthenticatorGetAssertionOptionsVersion4;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_DECODE_NONE.
        /// </remarks>
        public const int AttestationDecodeNone = 0;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_DECODE_COMMON.
        /// </remarks>
        public const int AttestationDecodeCommon = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_VER_TPM_2_0.
        /// </remarks>
        public const string AttestationVerTpm20 = "2.0";

        /// <remarks>
        /// Corresponds to WEBAUTHN_COMMON_ATTESTATION_CURRENT_VERSION.
        /// </remarks>
        public const int CommonAttestationCurrentVersion = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_TYPE_PACKED.
        /// </remarks>
        public const string AttestationTypePacked = "packed";

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_TYPE_U2F.
        /// </remarks>
        public const string AttestationTypeU2f = "fido-u2f";

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_TYPE_TPM.
        /// </remarks>
        public const string AttestationTypeTpm = "tpm";

        /// <remarks>
        /// Corresponds to WEBAUTHN_ATTESTATION_TYPE_NONE.
        /// </remarks>
        public const string AttestationTypeNone = "none";

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1.
        /// </remarks>
        public const int CredentialAttestationVersion1 = 1;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.
        /// </remarks>
        public const int CredentialAttestationVersion2 = 2;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.
        /// </remarks>
        public const int CredentialAttestationVersion3 = 3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_CURRENT_VERSION.
        /// </remarks>
        public const int CredentialAttestationCurrentVersion = CredentialAttestationVersion3;

        /// <remarks>
        /// Corresponds to WEBAUTHN_ASSERTION_CURRENT_VERSION.
        /// </remarks>
        public const int AssertionCurrentVersion = 1;


    }
}

