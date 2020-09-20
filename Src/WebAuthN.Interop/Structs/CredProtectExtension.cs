namespace WebAuthN.Interop
{
    // TODO: #define WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT                 L"credProtect"
    /// <summary>
    /// CredProtect  extension.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CRED_PROTECT_EXTENSION_IN.</remarks>
    internal class CredProtectExtension
    {
        public UserVerification CredProtect;

        /// <summary>
        /// Indicates whether authenticator support for the credProtect extension is required.
        /// </summary>
        public bool RequireCredProtect;
    }

    // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT
    // MakeCredential Input Type:   WEBAUTHN_CRED_PROTECT_EXTENSION_IN.
    //      - pvExtension must point to a WEBAUTHN_CRED_PROTECT_EXTENSION_IN struct
    //      - cbExtension will contain the sizeof(WEBAUTHN_CRED_PROTECT_EXTENSION_IN).
    // MakeCredential Output Type:  DWORD.
    //      - pvExtension will point to a DWORD with one of the above WEBAUTHN_USER_VERIFICATION_* values
    //        if credential was successfully created with CRED_PROTECT.
    //      - cbExtension will contain the sizeof(DWORD).
    // GetAssertion Input Type:     Not Supported
    // GetAssertion Output Type:    Not Supported
}
