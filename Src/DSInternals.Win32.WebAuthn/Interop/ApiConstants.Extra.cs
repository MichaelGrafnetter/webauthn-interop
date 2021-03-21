namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Contains WebAuthn API constants. Most of them are auto-generated from webauthn.h.
    /// </summary>
    public static partial class ApiConstants
    {
        public const string ClientDataCredentialCreate = "webauthn.create";
        public const string ClientDataCredentialCreateU2F = "navigator.id.finishEnrollment";
        public const string ClientDataCredentialGet = "webauthn.get";
        public const string ClientDataCredentialGetU2F = "navigator.id.getAssertion";
        public const int DefaultTimeoutMilliseconds = 60000;
    }
}
