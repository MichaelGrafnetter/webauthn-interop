using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Information about Extensions.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class ExtensionsOut : SafeStructArrayOut<ExtensionOut>
    {
        public bool? HmacSecret
        {
            get
            {
                // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_HMAC_SECRET
                // MakeCredential Output Type:  BOOL.
                //      - pvExtension will point to a BOOL with the value TRUE if credential
                //        was successfully created with HMAC_SECRET.
                //      - cbExtension will contain the sizeof(BOOL).
                var foundExtension = Items?.FirstOrDefault(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierHmacSecret);

                if(foundExtension == null)
                {
                    // The hmac-secret extension is not present.
                    return null;
                }

                return BitConverter.ToInt32(foundExtension.Data, 0) != 0;
            }
        }

        public UserVerification? CredProtect
        {
            get
            {
                // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_PROTECT
                // MakeCredential Output Type:  DWORD.
                //      - pvExtension will point to a DWORD with one of the above WEBAUTHN_USER_VERIFICATION_* values
                //        if credential was successfully created with CRED_PROTECT.
                //      - cbExtension will contain the sizeof(DWORD).
                var foundExtension = Items?.FirstOrDefault(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierCredProtect);

                if (foundExtension == null)
                {
                    // The credProtect extension is not present.
                    return null;
                }

                return (UserVerification)BitConverter.ToInt32(foundExtension.Data, 0);
            }
        }

        //TODO: Add support for the credBlob extension.
        // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_BLOB
        // MakeCredential Input Type:   WEBAUTHN_CRED_BLOB_EXTENSION.
        //      - pvExtension must point to a WEBAUTHN_CRED_BLOB_EXTENSION struct
        //      - cbExtension must contain the sizeof(WEBAUTHN_CRED_BLOB_EXTENSION).
        // MakeCredential Output Type:  BOOL.
        //      - pvExtension will point to a BOOL with the value TRUE if credBlob was successfully created
        //      - cbExtension will contain the sizeof(BOOL).
        // GetAssertion Input Type:     BOOL.
        //      - pvExtension must point to a BOOL with the value TRUE to request the credBlob.
        //      - cbExtension must contain the sizeof(BOOL).
        // GetAssertion Output Type:    WEBAUTHN_CRED_BLOB_EXTENSION.
        //      - pvExtension will point to a WEBAUTHN_CRED_BLOB_EXTENSION struct if the authenticator
        //        returns the credBlob in the signed extensions
        //      - cbExtension will contain the sizeof(WEBAUTHN_CRED_BLOB_EXTENSION).

        //TODO: Add support for the minPinLength extension
        // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_MIN_PIN_LENGTH
        // MakeCredential Input Type:   BOOL.
        //      - pvExtension must point to a BOOL with the value TRUE to request the minPinLength.
        //      - cbExtension must contain the sizeof(BOOL).
        // MakeCredential Output Type:  DWORD.
        //      - pvExtension will point to a DWORD with the minimum pin length if returned by the authenticator
        //      - cbExtension will contain the sizeof(DWORD).
        // GetAssertion Input Type:     Not Supported
        // GetAssertion Output Type:    Not Supported

        private ExtensionsOut() : base() { }
    }

    /// <summary>
    /// Information about Extensions.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_EXTENSIONS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal class ExtensionsIn : SafeStructArrayIn<ExtensionIn>
    {
        public ExtensionsIn(ExtensionIn[] extensions) : base(extensions)
        {
        }
    }
}
