namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Contains WebAuthn API constants. Most of them are auto-generated from webauthn.h.
    /// </summary>
    partial class ApiConstants
    {
        // TODO: Add support for U2F 'navigator.id.getAssertion' for authentication, and 'navigator.id.finishEnrollment' for registration 
        public const string ClientDataCredentialCreate = "webauthn.create";
        public const string ClientDataCredentialGet = "webauthn.get";
        public const int DefaultTimeoutMilliseconds = 60000;
    }
}
