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

                if (foundExtension.Data?.Length != sizeof(int))
                {
                    // This should never happen if the Windows API is working correctly.
                    throw new ArgumentOutOfRangeException(nameof(foundExtension.Data));
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

                if(foundExtension.Data?.Length != sizeof(int))
                {
                    // This should never happen if the Windows API is working correctly.
                    throw new ArgumentOutOfRangeException(nameof(foundExtension.Data));
                }

                return (UserVerification)BitConverter.ToInt32(foundExtension.Data, 0);
            }
        }

        public bool? CredBlobCreated
        {
            get
            {
                // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_CRED_BLOB
                // MakeCredential Output Type:  BOOL.
                //      - pvExtension will point to a BOOL with the value TRUE if credBlob was successfully created
                //      - cbExtension will contain the sizeof(BOOL).
                var foundExtension = Items?.FirstOrDefault(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierCredBlob);

                if (foundExtension == null)
                {
                    // The credBlob extension is not present.
                    return null;
                }

                if (foundExtension.Data?.Length != sizeof(int))
                {
                    // This should never happen if the Windows API is working correctly.
                    throw new ArgumentOutOfRangeException(nameof(foundExtension.Data));
                }

                return BitConverter.ToInt32(foundExtension.Data, 0) != 0;
            }
        }

        public byte[] CredBlob
        {
            get
            {
                // GetAssertion Output Type:    WEBAUTHN_CRED_BLOB_EXTENSION.
                //      - pvExtension will point to a WEBAUTHN_CRED_BLOB_EXTENSION struct if the authenticator
                //        returns the credBlob in the signed extensions
                //      - cbExtension will contain the sizeof(WEBAUTHN_CRED_BLOB_EXTENSION).
                var foundExtension = Items?.FirstOrDefault(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierCredBlob);

                if (foundExtension == null)
                {
                    // The credBlob extension is not present.
                    return null;
                }

                if (foundExtension.Data?.Length < sizeof(int)+sizeof(byte))
                {
                    // This should never happen if the Windows API is working correctly.
                    throw new ArgumentOutOfRangeException(nameof(foundExtension.Data));
                }

                int blobLength = BitConverter.ToInt32(foundExtension.Data, 0);
                if (blobLength != foundExtension.Data.Length - sizeof(int))
                {
                    // This should never happen if the Windows API is working correctly.
                    throw new ArgumentOutOfRangeException(nameof(foundExtension.Data));
                }

                // Copy the credential blob from the inner WEBAUTHN_CRED_BLOB_EXTENSION structure.
                byte[] blob = new byte[blobLength];
                Array.Copy(foundExtension.Data, sizeof(int), blob, 0, blobLength);

                return blob;
            }
        }


        public int? MinPinLength
        {
            get
            {
                // Below type definitions is for WEBAUTHN_EXTENSIONS_IDENTIFIER_MIN_PIN_LENGTH
                // MakeCredential Output Type:  DWORD.
                //      - pvExtension will point to a DWORD with the minimum pin length if returned by the authenticator
                //      - cbExtension will contain the sizeof(DWORD).
                var foundExtension = Items?.FirstOrDefault(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierMinPinLength);

                if (foundExtension == null)
                {
                    // The minPinLength extension is not present.
                    return null;
                }

                if (foundExtension.Data?.Length != sizeof(int))
                {
                    // This should never happen if the Windows API is working correctly.
                    throw new ArgumentOutOfRangeException(nameof(foundExtension.Data));
                }

                return BitConverter.ToInt32(foundExtension.Data, 0);
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
