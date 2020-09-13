using System;

namespace WebAuthN.Interop
{
    public class WebAuthN
    {
        // TODO: Async operations

        public static ApiVersion ApiVersion
        {
            get
            {
                try
                {
                    return NativeMethods.WebAuthNGetApiVersionNumber();
                }
                catch(EntryPointNotFoundException)
                {
                    // The WebAuthNGetApiVersionNumber() function was added in Windows 10 1903.
                    return ApiVersion.Version1;
                }
            }
        }

        public static bool IsPlatformAuthenticatorAvailable
        {
            get
            {
                var hResult = NativeMethods.WebAuthNIsUserVerifyingPlatformAuthenticatorAvailable(out bool value);

                // TODO: Process hResult

                return value;
            }
        }

        public void AuthenticatorMakeCredential()
        {
        }

        public void AuthenticatorGetAssertion()
        {
        }

        public void CancelCurrentOperation()
        {

        }
    }
}
