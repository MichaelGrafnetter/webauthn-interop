using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// This class exposes native WebAuthn API implemented in webauthn.dll in Windows 10.
    /// </summary>
    /// <see>https://github.com/microsoft/webauthn/blob/master/webauthn.h</see>
    internal static class NativeMethods
    {
        private const string WebAuthn = "webauthn.dll";
        private const string User32 = "user32.dll";

        [DllImport(WebAuthn)]
        internal static extern ApiVersion WebAuthNGetApiVersionNumber();

        [DllImport(WebAuthn)]
        internal static extern HResult WebAuthNIsUserVerifyingPlatformAuthenticatorAvailable(out bool isUserVerifyingPlatformAuthenticatorAvailable);

        [DllImport(WebAuthn)]
        internal static extern HResult WebAuthNAuthenticatorMakeCredential();

        [DllImport(WebAuthn)]
        internal static extern HResult WebAuthNAuthenticatorGetAssertion();

        [DllImport(WebAuthn)]
        internal static extern void WebAuthNFreeCredentialAttestation(IntPtr webAuthNCredentialAttestation);

        [DllImport(WebAuthn)]
        internal static extern void WebAuthNFreeAssertion(IntPtr webAuthNAssertion);

        [DllImport(WebAuthn)]
        internal static extern HResult WebAuthNGetCancellationId(out Guid cancellationId);

        [DllImport(WebAuthn)]
        internal static extern HResult WebAuthNCancelCurrentOperation(in Guid cancellationId);

        [DllImport(WebAuthn, CharSet = CharSet.Unicode)]
        internal static extern string WebAuthNGetErrorName(HResult hr);

        [DllImport(WebAuthn)]
        internal static extern HResult WebAuthNGetW3CExceptionDOMError(HResult hr);

        [DllImport(User32)]
        internal static extern IntPtr GetForegroundWindow();
    }
}
