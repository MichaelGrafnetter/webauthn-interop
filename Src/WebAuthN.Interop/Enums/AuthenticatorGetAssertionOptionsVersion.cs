using System;
using System.Collections.Generic;
using System.Text;

namespace WebAuthN.Interop
{
    internal enum AuthenticatorGetAssertionOptionsVersion : int
    {
        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_1.</remarks>
        Version1 = 1,

        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_2.</remarks>
        Version2 = 2,

        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_3.</remarks>
        Version3 = 3,

        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_VERSION_4.</remarks>
        Version4 = 4,

        /// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_GET_ASSERTION_OPTIONS_CURRENT_VERSION.</remarks>
        Current = Version4
    }
}
