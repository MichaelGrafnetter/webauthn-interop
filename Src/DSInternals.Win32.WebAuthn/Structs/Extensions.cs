using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
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
                var foundExtension = Items.FirstOrDefault(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierHmacSecret);

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
                var foundExtension = Items.FirstOrDefault(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierCredProtect);

                if (foundExtension == null)
                {
                    // The credProtect extension is not present.
                    return null;
                }

                return (UserVerification)BitConverter.ToInt32(foundExtension.Data, 0);
            }
        }

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
