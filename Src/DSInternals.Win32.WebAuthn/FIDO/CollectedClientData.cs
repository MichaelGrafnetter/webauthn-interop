﻿using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    public class CollectedClientData
    {
        /// <summary>
        /// This member contains the string "webauthn.create" when creating new credentials, and "webauthn.get" when getting an assertion from an existing credential.
        /// </summary>
        /// <remarks>
        /// The purpose of this member is to prevent certain types of signature confusion attacks (where an attacker substitutes one legitimate signature for another).
        /// </remarks>
        /// <see>https://www.w3.org/TR/webauthn-2/#dom-collectedclientdata-type</see>
        [JsonProperty("type", Required = Required.Always)]
        public string Type
        {
            get;
            set;
        }

        /// <summary>
        /// This member contains the base64url encoding of the challenge provided by the Relying Party.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn-2/#dom-collectedclientdata-challenge</see>
        [JsonProperty("challenge", Required = Required.Always)]
        [JsonConverter(typeof(Base64UrlConverter))]
        public byte[] Challenge
        {
            get;
            set;
        }

        /// <summary>
        /// This member contains the fully qualified origin of the requester, as provided to the authenticator by the client, in the syntax defined by RFC6454.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn-2/#dom-collectedclientdata-origin</see>
        [JsonProperty("origin", Required = Required.Always)]
        public string Origin
        {
            get;
            set;
        }

        [JsonProperty("clientExtensions", NullValueHandling = NullValueHandling.Ignore)]
        public AuthenticationExtensionsClientInputs ClientExtensions
        {
            get;
            set;
        }

        /// <summary>
        /// This member contains the inverse of the sameOriginWithAncestors argument value that was passed into the internal method.
        /// </summary>
        [JsonProperty("crossOrigin", NullValueHandling = NullValueHandling.Ignore)]
        public bool? CrossOrigin
        {
            get;
            set;
        }

        [JsonProperty("hashAlgorithm", NullValueHandling = NullValueHandling.Ignore)]
        public string HashAlgorithm
        {
            get;
            set;
        }

        // TODO: Add support for TokenBinding
    }
}
