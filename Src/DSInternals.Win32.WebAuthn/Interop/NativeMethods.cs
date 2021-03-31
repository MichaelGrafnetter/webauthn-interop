using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// This class exposes native WebAuthn API implemented in webauthn.dll in Windows 10.
    /// </summary>
    /// <see>https://github.com/microsoft/webauthn/blob/master/webauthn.h</see>
#if NET5_0
    [SupportedOSPlatform("windows")]
#endif
    internal static class NativeMethods
    {
        private const string WebAuthn = "webauthn.dll";
        private const string User32 = "user32.dll";

        /// <summary>
        /// Indicates the presence of relevant APIs.
        /// </summary>
        /// <returns>Available API version.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetApiVersionNumber")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern ApiVersion GetApiVersionNumber();

        /// <summary>
        /// Indicates whether a user-verifying platform authenticator is available.
        /// </summary>
        /// <param name="isUserVerifyingPlatformAuthenticatorAvailable">True if and only if a user-verifying platform authenticator is available.</param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNIsUserVerifyingPlatformAuthenticatorAvailable")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult IsUserVerifyingPlatformAuthenticatorAvailable(out bool isUserVerifyingPlatformAuthenticatorAvailable);

        /// <summary>
        /// Creates a new credential instance based on the provided options.
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="rpInformation"></param>
        /// <param name="userInformation"></param>
        /// <param name="pubKeyCredParams"></param>
        /// <param name="clientData"></param>
        /// <param name="makeCredentialOptions"></param>
        /// <param name="credentialAttestation"></param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNAuthenticatorMakeCredential", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult AuthenticatorMakeCredential(
            WindowHandle windowHandle,
            RelyingPartyInformation rpInformation,
            UserInformation userInformation,
            CoseCredentialParameters pubKeyCredParams,
            ClientData clientData,
            AuthenticatorMakeCredentialOptions makeCredentialOptions,
            out CredentialAttestationSafeHandle credentialAttestation
        );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="windowHandle"></param>
        /// <param name="rpId"></param>
        /// <param name="clientData"></param>
        /// <param name="getAssertionOptions"></param>
        /// <param name="assertion"></param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNAuthenticatorGetAssertion", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult AuthenticatorGetAssertion(
            WindowHandle windowHandle,
            string rpId,
            ClientData clientData,
            AuthenticatorGetAssertionOptions getAssertionOptions,
            out AssertionSafeHandle assertion
        );

        /// <summary>
        /// Frees memory that the AuthenticatorMakeCredential function has allocated for a WEBAUTHN_CREDENTIAL_ATTESTATION structure.
        /// </summary>
        /// <param name="webAuthNCredentialAttestation">Pointer to a WEBAUTHN_ATTESTATION structure to deallocate.</param>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNFreeCredentialAttestation")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern void FreeCredentialAttestation(IntPtr webAuthNCredentialAttestation);

        /// <summary>
        /// Frees memory that the AuthenticatorGetAssertion function has allocated for a WEBAUTHN_ASSERTION structure.
        /// </summary>
        /// <param name="webAuthNAssertion">Pointer to a WEBAUTHN_ASSERTION structure to deallocate.</param>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNFreeAssertion")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern void FreeAssertion(IntPtr webAuthNAssertion);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationId"></param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetCancellationId")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult GetCancellationId(out Guid cancellationId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationId"></param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCancelCurrentOperation")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult CancelCurrentOperation(in Guid cancellationId);

        /// <summary>
        /// Translates a HRESULT code to error name.
        /// </summary>
        /// <param name="hr">Coded numerical value that is assigned to a specific exception.</param>
        /// <returns>
        /// Returns the following Error Names:
        ///  L"Success"              - S_OK
        ///  L"InvalidStateError"    - NTE_EXISTS
        ///  L"ConstraintError"      - HRESULT_FROM_WIN32(ERROR_NOT_SUPPORTED),
        ///                            NTE_NOT_SUPPORTED,
        ///                            NTE_TOKEN_KEYSET_STORAGE_FULL
        ///  L"NotSupportedError"    - NTE_INVALID_PARAMETER
        ///  L"NotAllowedError"      - NTE_DEVICE_NOT_FOUND,
        ///                            NTE_NOT_FOUND,
        ///                            HRESULT_FROM_WIN32(ERROR_CANCELLED),
        ///                            NTE_USER_CANCELLED,
        ///                            HRESULT_FROM_WIN32(ERROR_TIMEOUT)
        ///  L"UnknownError"         - All other hr values
        /// </returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetErrorName", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(PtrToConstStringMarshaler))]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern string GetErrorName(HResult hr);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hr"></param>
        /// <returns></returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetW3CExceptionDOMError")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult GetW3CExceptionDOMError(HResult hr);

        /// <summary>
        /// Retrieves a handle to the foreground window (the window with which the user is currently working).
        /// </summary>
        /// <returns>The return value is a handle to the foreground window. The foreground window can be NULL in certain circumstances, such as when a window is losing activation.</returns>
        [DllImport(User32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern WindowHandle GetForegroundWindow();
    }
}
