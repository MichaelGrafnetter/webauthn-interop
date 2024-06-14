using System;
using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    [Flags]
    internal enum AssertionOptionsFlags : uint
    {
        None = 0,
        AuthenticatorHmacSecretValues = PInvoke.WEBAUTHN_AUTHENTICATOR_HMAC_SECRET_VALUES_FLAG
    }
}
