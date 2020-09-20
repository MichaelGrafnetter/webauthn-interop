namespace WebAuthN.Interop
{
    // TODO: #define WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET                  L"hmac-secret"
    // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET
    // MakeCredential Input Type:   BOOL.
    //      - pvExtension must point to a BOOL with the value TRUE.
    //      - cbExtension must contain the sizeof(BOOL).
    // MakeCredential Output Type:  BOOL.
    //      - pvExtension will point to a BOOL with the value TRUE if credential
    //        was successfully created with HMAC_SECRET.
    //      - cbExtension will contain the sizeof(BOOL).
}
