using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Defines the credential protection policy.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum UserVerification : int
    {
        /// <summary>
        /// Extension is not set.
        /// </summary>
        [EnumMember(Value = "NULL")]
        Any = ApiConstants.UserVerificationAny,

        /// <summary>
        /// This reflects "FIDO_2_0" semantics. In this configuration, user verification is optional with or without credentialID list. This is the default state of the credential if the extension is not specified and the authenticator does not report a defaultCredProtect value in the authenticatorGetInfo response.
        /// </summary>
        [EnumMember(Value = "userVerificationOptional")]
        Optional = ApiConstants.UserVerificationOptional,

        /// <summary>
        /// In this configuration, credential is discovered only when its credentialID is provided by the platform or when user verification is performed.
        /// </summary>
        [EnumMember(Value = "userVerificationOptionalWithCredentialIDList")]
        OptionalWithCredentialIDList = ApiConstants.UserVerificationOptionalWithCredentialIdList,

        /// <summary>
        /// This reflects that discovery and usage of the credential MUST be preceeded by user verification.
        /// </summary>
        [EnumMember(Value = "userVerificationRequired")]
        Required = ApiConstants.UserVerificationRequired
    }
}
