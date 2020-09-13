using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// This class exposes native WebAuthn API implemented in webauthn.dll in Windows 10.
    /// </summary>
    /// <remarks>This code was heavily inspired by the work of Alex Seigler.</remarks>
    /// <see>https://github.com/microsoft/webauthn/blob/master/webauthn.h</see>
    /// <seealso>https://github.com/aseigler/HelloSample/blob/master/hellointerop/HelloInterop.cs</seealso>
    internal static class NativeMethods
    {
        private const string WebAuthn = "webauthn.dll";
        private const string User32 = "user32.dll";

        [DllImport(WebAuthn)]
        public static extern ApiVersion WebAuthNGetApiVersionNumber();

        [DllImport(WebAuthn)]
        public static extern HResult WebAuthNIsUserVerifyingPlatformAuthenticatorAvailable(out bool isUserVerifyingPlatformAuthenticatorAvailable);

        [DllImport(WebAuthn)]
        public static extern HResult WebAuthNAuthenticatorMakeCredential();

        [DllImport(WebAuthn)]
        public static extern HResult WebAuthNAuthenticatorGetAssertion();

        [DllImport(WebAuthn)]
        public static extern void WebAuthNFreeCredentialAttestation(IntPtr webAuthNCredentialAttestation);

        [DllImport(WebAuthn)]
        public static extern void WebAuthNFreeAssertion(IntPtr webAuthNAssertion);

        [DllImport(WebAuthn)]
        public static extern HResult WebAuthNGetCancellationId(out Guid cancellationId);

        [DllImport(WebAuthn)]
        public static extern HResult WebAuthNCancelCurrentOperation(in Guid cancellationId);

        [DllImport(WebAuthn, CharSet = CharSet.Unicode)]
        public static extern string WebAuthNGetErrorName(HResult hr);

        [DllImport(WebAuthn)]
        public static extern HResult WebAuthNGetW3CExceptionDOMError(HResult hr);

        [DllImport(User32)]
        private static extern IntPtr GetForegroundWindow();
    }
}
