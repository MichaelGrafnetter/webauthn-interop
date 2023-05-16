namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Contains WebAuthn API constants. Most of them are auto-generated from webauthn.h.
    /// </summary>
    public static partial class ApiConstants
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
        public const int DefaultTimeoutMilliseconds = 60000;
    }
}
