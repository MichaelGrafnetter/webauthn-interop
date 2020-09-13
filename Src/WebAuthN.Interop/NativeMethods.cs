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

        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetApiVersionNumber")]
        internal static extern ApiVersion GetApiVersionNumber();

        [DllImport(WebAuthn, EntryPoint = "WebAuthNIsUserVerifyingPlatformAuthenticatorAvailable")]
        internal static extern HResult IsUserVerifyingPlatformAuthenticatorAvailable(out bool isUserVerifyingPlatformAuthenticatorAvailable);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNAuthenticatorMakeCredential")]
        internal static extern HResult AuthenticatorMakeCredential(
            IntPtr hwnd,
            RelyingPartyInformation rpInformation,
            UserInformation userInformation,
            PublicKeyCredentialParameterList pubKeyCredParams,
            ClientData webAuthNClientData,
            [Optional] MakeCredentialOptions webAuthNMakeCredentialOptions,
            out CredentialAttestationSafeHandle webAuthNCredentialAttestation
        );

        [DllImport(WebAuthn, EntryPoint = "WebAuthNAuthenticatorGetAssertion")]
        internal static extern HResult AuthenticatorGetAssertion();

        [DllImport(WebAuthn, EntryPoint = "WebAuthNFreeCredentialAttestation")]
        internal static extern void FreeCredentialAttestation(IntPtr webAuthNCredentialAttestation);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNFreeAssertion")]
        internal static extern void FreeAssertion(IntPtr webAuthNAssertion);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetCancellationId")]
        internal static extern HResult GetCancellationId(out Guid cancellationId);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNCancelCurrentOperation")]
        internal static extern HResult CancelCurrentOperation(in Guid cancellationId);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetErrorName")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(PtrToConstStringMarshaler))]
        internal static extern string GetErrorName(HResult hr);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetW3CExceptionDOMError")]
        internal static extern HResult GetW3CExceptionDOMError(HResult hr);

        [DllImport(User32)]
        internal static extern IntPtr GetForegroundWindow();
    }
}
