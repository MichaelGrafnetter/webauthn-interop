using System;

namespace DSInternals.Win32.WebAuthn.Interop
{
    [Flags]
    internal enum AssertionOptionsFlags : int
    {
        None = 0,
        AuthenticatorHmacSecretValues = ApiConstants.AuthenticatorHmacSecretValuesFlag
    }
}
