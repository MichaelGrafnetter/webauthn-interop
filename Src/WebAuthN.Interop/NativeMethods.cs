﻿using System;
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
        public static extern ApiVersion GetApiVersionNumber();

        [DllImport(WebAuthn, EntryPoint = "WebAuthNIsUserVerifyingPlatformAuthenticatorAvailable")]
        public static extern HResult IsUserVerifyingPlatformAuthenticatorAvailable(out bool isUserVerifyingPlatformAuthenticatorAvailable);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNAuthenticatorMakeCredential")]
        public static extern HResult AuthenticatorMakeCredential(
            IntPtr hwnd,
            RelyingPartyInformation rpInformation,
            UserInformation userInformation,
            PublicKeyCredentialParameters pubKeyCredParams,
            ClientData clientData,
            [Optional] MakeCredentialOptions makeCredentialOptions,
            out CredentialAttestationSafeHandle credentialAttestation
        );

        [DllImport(WebAuthn, EntryPoint = "WebAuthNAuthenticatorGetAssertion")]
        public static extern HResult AuthenticatorGetAssertion(
            IntPtr hwnd,
            string rpId,
            ClientData clientData,
            [Optional] AuthenticatorGetAssertionOptions getAssertionOptions,
            out AssertionSafeHandle assertion
        );

        [DllImport(WebAuthn, EntryPoint = "WebAuthNFreeCredentialAttestation")]
        public static extern void FreeCredentialAttestation(IntPtr webAuthNCredentialAttestation);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNFreeAssertion")]
        public static extern void FreeAssertion(IntPtr webAuthNAssertion);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetCancellationId")]
        public static extern HResult GetCancellationId(out Guid cancellationId);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNCancelCurrentOperation")]
        internal static extern HResult CancelCurrentOperation(in Guid cancellationId);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetErrorName")]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(PtrToConstStringMarshaler))]
        public static extern string GetErrorName(HResult hr);

        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetW3CExceptionDOMError")]
        public static extern HResult GetW3CExceptionDOMError(HResult hr);

        [DllImport(User32)]
        public static extern IntPtr GetForegroundWindow();
    }
}
