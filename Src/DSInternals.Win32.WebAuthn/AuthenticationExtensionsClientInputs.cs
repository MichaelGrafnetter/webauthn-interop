using System;
using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Contains WebAuthn extensions that are actually supported by Windows 10.
    /// These are currently defined in CTAP 2.1 Draft.
    /// </summary>
    public class AuthenticationExtensionsClientInputs
    {
        private UserVerification _credProtect;
        private bool _enforceCredProtect;
        private bool _hmacCreateSecret;
        private bool _minPinLength;
        private bool _getCredBlob;

        /// <summary>
        /// This extension is used by the platform to retrieve a symmetric secret from the authenticator when it needs to encrypt or decrypt data using that symmetric secret. This symmetric secret is scoped to a credential. The authenticator and the platform each only have the part of the complete secret to prevent offline attacks. This extension can be used to maintain different secrets on different machines.
        /// </summary>
        /// <see>https://fidoalliance.org/specs/fido2/fido-client-to-authenticator-protocol-v2.1-rd-20191217.html#sctn-hmac-secret-extension</see>
        [JsonProperty("hmacCreateSecret", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HmacCreateSecret
        {
            get
            {
                // Treat false as null, so that it is not serialized.
                return _hmacCreateSecret ? true : null;
            }
            set
            {
                _hmacCreateSecret = value == true;
            }
        }

        /// <summary>
        /// This extension indicates that the authenticator supports enhanced protection mode for the credentials created on the authenticator.
        /// If present, verify that the credentialProtectionPolicy value is one of following values: userVerificationOptional, userVerificationOptionalWithCredentialIDList, userVerificationRequired
        /// </summary>
        /// <see>https://fidoalliance.org/specs/fido2/fido-client-to-authenticator-protocol-v2.1-rd-20191217.html#sctn-credProtect-extension</see>
        [JsonProperty("credentialProtectionPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public UserVerification? CredProtect
        {
            get
            {
                // Treat Any as null, so that it is not serialized to JSON.
                return _credProtect == UserVerification.Any ? null : _credProtect;
            }
            set
            {
                _credProtect = value ?? UserVerification.Any;
            }
        }

        /// <summary>
        /// Controls whether it is better to fail to create a credential rather than ignore the protection policy. When enforceCredentialProtectionPolicy is true, and credentialProtectionPolicy is either userVerificationOptionalWithCredentialIDList or userVerificationRequired, the platform SHOULD NOT create the credential in a way that does not implement the requested protection policy.
        /// </summary>
        /// <see>https://fidoalliance.org/specs/fido2/fido-client-to-authenticator-protocol-v2.1-rd-20191217.html#sctn-credProtect-extension</see>
        [JsonProperty("enforceCredentialProtectionPolicy", NullValueHandling = NullValueHandling.Ignore)]
        public bool? EnforceCredProtect
        {
            get
            {
                // Do not serialize if CredProtect is not set.
                return _enforceCredProtect && _credProtect != UserVerification.Any ? true : null;
            }
            set
            {
                _enforceCredProtect = value == true;
            }
        }

        /// <summary>
        /// This extension allows WebAuthn Relying Parties that have previously registered a credential using the legacy FIDO JavaScript APIs to request an assertion.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn/#sctn-appid-extension</see>
        [JsonProperty("appid", NullValueHandling = NullValueHandling.Ignore)]
        public string AppID { get; set; }

        /// <summary>
        /// This extension returns the current minimum PIN length value. This value does not decrease unless the authenticator is reset, in which case, all the credentials are reset. This extension is only applicable during credential creation.
        /// </summary>
        /// <see>https://fidoalliance.org/specs/fido-v2.1-ps-20210615/fido-client-to-authenticator-protocol-v2.1-ps-20210615.html#sctn-minpinlength-extension</see>
        [JsonProperty("minPinLength", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MinPinLength
        {
            get
            {
                // Treat false as null, so that it is not serialized.
                return _minPinLength ? true : null;
            }
            set
            {
                _minPinLength = value == true;
            }
        }

        [JsonProperty("credBlob", NullValueHandling = NullValueHandling.Ignore)]
        public byte[] CredBlob { get; set; }


        [JsonProperty("getCredBlob", NullValueHandling = NullValueHandling.Ignore)]
        public bool? GetCredBlob
        {
            get
            {
                // Treat false as null, so that it is not serialized.
                return _getCredBlob ? true : null;
            }
            set
            {
                _getCredBlob = value == true;
            }
        }

        // TODO: boolean hmacCreateSecret;

        // TODO: HMACGetSecretInput hmacGetSecret;
    }
}
