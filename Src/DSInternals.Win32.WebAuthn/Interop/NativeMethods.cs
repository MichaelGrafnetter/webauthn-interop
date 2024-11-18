using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// This class exposes native WebAuthn API implemented in webauthn.dll in Windows 10.
    /// </summary>
    /// <see>https://github.com/microsoft/webauthn/blob/master/webauthn.h</see>
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    internal static class NativeMethods
    {
        private const string WebAuthn = "webauthn.dll";
        private const string User32 = "user32.dll";
        private const string Kernel32 = "kernel32.dll";

        /// <summary>
        /// Gets the version number of the WebAuthN API.
        /// </summary>
        /// <returns>The WebAuthN API version number.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetApiVersionNumber")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern ApiVersion GetApiVersionNumber();

        /// <summary>
        /// Determines whether the platform authenticator service is available.
        /// </summary>
        /// <param name="isUserVerifyingPlatformAuthenticatorAvailable">True if and only if a user-verifying platform authenticator is available.</param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNIsUserVerifyingPlatformAuthenticatorAvailable")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult IsUserVerifyingPlatformAuthenticatorAvailable(out bool isUserVerifyingPlatformAuthenticatorAvailable);

        /// <summary>
        /// The WebAuthNAuthenticatorMakeCredential operation creates a public key credential source
        /// bound to a managing authenticator and returns the credential public key associated
        /// with its credential private key. The Relying Party can use this credential public key
        /// to verify the authentication assertions created by this public key credential source.
        /// </summary>
        /// <param name="windowHandle">The handle for the window that will be used to display the UI.</param>
        /// <param name="rpInformation">The Relying Party's WEBAUTHN_RP_ENTITY_INFORMATION.</param>
        /// <param name="userInformation">The user account’s WEBAUTHN_USER_ENTITY_INFORMATION, containing the user handle given by the Relying Party.</param>
        /// <param name="pubKeyCredParams">A sequence of pairs of public key credential type and public key algorithms requested by the Relying Party. This sequence is ordered from most preferred to least preferred. The authenticator makes a best-effort to create the most preferred credential that it can.</param>
        /// <param name="clientData">The client data to be sent to the authenticator for the Relying Party.</param>
        /// <param name="makeCredentialOptions">Provides the options to use when creating the public key credential source.</param>
        /// <param name="credentialAttestation">On successful completion of this operation, the authenticator returns the attestation object to the client.</param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNAuthenticatorMakeCredential", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult AuthenticatorMakeCredential(
            WindowHandle windowHandle,
            RelyingPartyInformation rpInformation,
            UserInformationIn userInformation,
            CoseCredentialParameters pubKeyCredParams,
            ClientData clientData,
            AuthenticatorMakeCredentialOptions makeCredentialOptions,
            out CredentialAttestationSafeHandle credentialAttestation
        );

        /// <summary>
        /// Produces an assertion signature representing an assertion by the authenticator
        /// that the user has consented to a specific transaction, such as logging in or completing a purchase.
        /// </summary>
        /// <param name="windowHandle">The handle for the window that will be used to display the UI.</param>
        /// <param name="rpId">The ID of the Relying Party.</param>
        /// <param name="clientData">The client data to be sent to the authenticator for the Relying Party.</param>
        /// <param name="getAssertionOptions">The options for the WebAuthNAuthenticatorGetAssertion operation.</param>
        /// <param name="assertion">A pointer to a WEBAUTHN_ASSERTION that receives the assertion.</param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        /// <remarks>
        /// If the authenticator cannot find any credential corresponding to the specified Relying Party that matches the specified criteria, it terminates the operation and returns an error.
        /// Before performing this operation, all other operations in progress in the authenticator session MUST be aborted by running the WebAuthNCancelCurrentOperation operation.
        /// </remarks>
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
        /// Gets the cancellation ID for the canceled operation.
        /// </summary>
        /// <param name="cancellationId">The GUID returned, representing the ID of the cancelled operation.</param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetCancellationId")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult GetCancellationId(out Guid cancellationId);

        /// <summary>
        /// When this operation is invoked by the client in an authenticator session, it has the effect
        /// of terminating any WebAuthNAuthenticatorMakeCredential or WebAuthNAuthenticatorGetAssertion
        /// operation currently in progress in that authenticator session. The authenticator stops prompting for,
        /// or accepting, any user input related to authorizing the canceled operation.
        /// The client ignores any further responses from the authenticator for the canceled operation.
        /// </summary>
        /// <param name="cancellationId">The GUID returned, representing the ID of the cancelled operation.</param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        /// <remarks>
        /// This operation is ignored if it is invoked in an authenticator session which does not have
        /// an WebAuthNAuthenticatorMakeCredential or WebAuthNAuthenticatorGetAssertion operation currently in progress.
        /// </remarks>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCancelCurrentOperation")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult CancelCurrentOperation(in Guid cancellationId);

        /// <summary>
        /// Gets the list of WEBAUTHN_CREDENTIAL_DETAILS_LIST currently stored for the user.
        /// </summary>
        /// <param name="getCredentialsOptions">The options for the operation.</param>
        /// <param name="credentialDetailsList">The credentials list returned by the operation.</param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetPlatformCredentialList", CharSet = CharSet.Unicode)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult GetPlatformCredentialList(GetCredentialsOptions getCredentialsOptions, [Out] out PlatformCredentialListSafeHandle credentialDetailsList);

        /// <summary>
        /// Frees the allocation for the WEBAUTHN_CREDENTIAL_DETAILS_LIST.
        /// </summary>
        /// <param name="credentialDetailsList">The platform credential list to be freed.</param>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNFreePlatformCredentialList")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern void FreePlatformCredentialList(IntPtr credentialDetailsList);

        /// <summary>
        /// Removes a Public Key Credential Source stored on a Virtual Authenticator.
        /// </summary>
        /// <param name="credentialIdLength">Length of the credential ID to be removed.</param>
        /// <param name="credentialId">Credential ID to be removed.</param>
        /// <returns>If the function succeeds, it returns S_OK. If the function fails, it returns an HRESULT value that indicates the error.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNDeletePlatformCredential")]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern HResult DeletePlatformCredential(int credentialIdLength, byte[] credentialId);


        /// <summary>
        /// Gets the error name for the specified error code.
        /// </summary>
        /// <param name="hr">Coded numerical value that is assigned to a specific exception.</param>
        /// <returns>
        /// Returns the following error codes:
        ///  Success              - S_OK
        ///  InvalidStateError    - NTE_EXISTS
        ///  ConstraintError      - HRESULT_FROM_WIN32(ERROR_NOT_SUPPORTED),
        ///                         NTE_NOT_SUPPORTED,
        ///                         NTE_TOKEN_KEYSET_STORAGE_FULL
        ///  NotSupportedError    - NTE_INVALID_PARAMETER
        ///  NotAllowedError      - NTE_DEVICE_NOT_FOUND,
        ///                         NTE_NOT_FOUND,
        ///                         HRESULT_FROM_WIN32(ERROR_CANCELLED),
        ///                         NTE_USER_CANCELLED,
        ///                         HRESULT_FROM_WIN32(ERROR_TIMEOUT)
        ///  UnknownError         - All other HRESULT values
        /// </returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNGetErrorName", CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.CustomMarshaler, MarshalTypeRef = typeof(PtrToConstStringMarshaler))]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern string GetErrorName(HResult hr);

        /// <summary>
        /// Gets the W3C DOM error code for the last failed operation in the authenticator session.
        /// </summary>
        /// <param name="hr">The HRESULT returned by the last failed operation in the session.</param>
        /// <returns>An HRESULT with the failure status.</returns>
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

        /// <summary>
        /// Retrieves the window handle used by the console associated with the calling process.
        /// </summary>
        /// <returns>The return value is a handle to the window used by the console associated with the calling process or NULL if there is no such associated console.</returns>
        [DllImport(Kernel32)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        public static extern WindowHandle GetConsoleWindow();
    }
}
