using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    public partial class WebAuthnApi
    {
        /// <summary>
        /// Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="options">The credential request options that describe the relying party, allowed credentials, and the desired authenticator behavior.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <returns>The signed assertion public key credential produced by the authenticator.</returns>
        public AssertionPublicKeyCredential AuthenticatorGetAssertion(PublicKeyCredentialRequestOptions options, WindowHandle windowHandle = default)
        {
            ArgumentNullException.ThrowIfNull(options);

            if (string.IsNullOrWhiteSpace(options.RpId))
            {
                throw new ArgumentException("Relying party ID must be provided.", nameof(options));
            }

            return AuthenticatorGetAssertion(
                rpId: options.RpId,
                challenge: options.Challenge,
                userVerificationRequirement: options.UserVerification,
                timeoutMilliseconds: options.Timeout ?? ApiConstants.DefaultTimeoutMilliseconds,
                allowCredentials: options.AllowCredentials,
                extensions: options.Extensions,
                credentialHints: options.Hints?.ToArray(),
                publicKeyCredentialRequestOptionsJson: Encoding.UTF8.GetBytes(options.ToString()),
                windowHandle: windowHandle
            );
        }

        /// <summary>
        /// Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="rpId">Identifier of the relying party requesting the assertion.</param>
        /// <param name="challenge">Cryptographic challenge produced by the relying party to be signed by the authenticator.</param>
        /// <param name="userVerificationRequirement">Indicates whether user verification is required, preferred, or discouraged.</param>
        /// <param name="authenticatorAttachment">Constrains the type of authenticator that may be used (platform, cross-platform, or any).</param>
        /// <param name="timeoutMilliseconds">Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.</param>
        /// <param name="allowCredentials">Optional list of credentials acceptable to the relying party for the assertion.</param>
        /// <param name="extensions">Client extension inputs for the assertion operation.</param>
        /// <param name="browserInPrivateMode">Indicates whether the request originates from a browser running in private/incognito mode.</param>
        /// <param name="linkedDevice">Optional hybrid (cross-device) storage linked data for state-assisted transactions.</param>
        /// <param name="autoFill">Indicates whether the request is a conditional UI (autofill) request.</param>
        /// <param name="credentialHints">Optional ordered list of public key credential hints describing the modality the relying party prefers.</param>
        /// <param name="authenticatorId">Optional identifier of a specific authenticator to target.</param>
        /// <param name="publicKeyCredentialRequestOptionsJson">Optional UTF-8 encoded JSON representation of the original request options, forwarded to the authenticator.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <returns>The signed assertion public key credential produced by the authenticator.</returns>
        public AssertionPublicKeyCredential AuthenticatorGetAssertion(
            string rpId,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            uint timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor>? allowCredentials = null,
            AuthenticationExtensionsClientAssertionInputs? extensions = null,
            bool browserInPrivateMode = false,
            HybridStorageLinkedData? linkedDevice = null,
            bool autoFill = false,
            string[]? credentialHints = null,
            byte[]? authenticatorId = null,
            byte[]? publicKeyCredentialRequestOptionsJson = null,
            WindowHandle windowHandle = default
        )
        {
            ArgumentNullException.ThrowIfNull(rpId);
            ArgumentNullException.ThrowIfNull(challenge);

            // TODO: Handle U2F attachment

            var clientData = CollectedClientData.Create(
                type: ApiConstants.ClientDataCredentialGet,
                challenge: challenge,
                hostName: rpId,
                relyingPartyId: rpId,
                remoteClientDataJson: extensions?.RemoteClientDataJson,
                remoteDesktopClientOverride: extensions?.RemoteDesktopClientOverride);

            return AuthenticatorGetAssertion(
                rpId,
                clientData,
                userVerificationRequirement,
                authenticatorAttachment,
                timeoutMilliseconds,
                allowCredentials,
                extensions,
                browserInPrivateMode,
                linkedDevice,
                autoFill,
                credentialHints,
                authenticatorId,
                publicKeyCredentialRequestOptionsJson,
                windowHandle
            );
        }

        /// <summary>
        /// Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="rpId">Identifier of the relying party requesting the assertion.</param>
        /// <param name="clientData">The client data that contains the challenge, type, origin, and related context to be signed by the authenticator.</param>
        /// <param name="userVerificationRequirement">Indicates whether user verification is required, preferred, or discouraged.</param>
        /// <param name="authenticatorAttachment">Constrains the type of authenticator that may be used (platform, cross-platform, or any).</param>
        /// <param name="timeoutMilliseconds">Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.</param>
        /// <param name="allowCredentials">Optional list of credentials acceptable to the relying party for the assertion.</param>
        /// <param name="extensions">Client extension inputs for the assertion operation.</param>
        /// <param name="browserInPrivateMode">Indicates whether the request originates from a browser running in private/incognito mode.</param>
        /// <param name="linkedDevice">Optional hybrid (cross-device) storage linked data for state-assisted transactions.</param>
        /// <param name="autoFill">Indicates whether the request is a conditional UI (autofill) request.</param>
        /// <param name="credentialHints">Optional ordered list of public key credential hints describing the modality the relying party prefers.</param>
        /// <param name="authenticatorId">Optional identifier of a specific authenticator to target.</param>
        /// <param name="publicKeyCredentialRequestOptionsJson">Optional UTF-8 encoded JSON representation of the original request options, forwarded to the authenticator.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <returns>The signed assertion public key credential produced by the authenticator.</returns>
        public AssertionPublicKeyCredential AuthenticatorGetAssertion(
            string rpId,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            uint timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor>? allowCredentials = null,
            AuthenticationExtensionsClientAssertionInputs? extensions = null,
            bool browserInPrivateMode = false,
            HybridStorageLinkedData? linkedDevice = null,
            bool autoFill = false,
            string[]? credentialHints = null,
            byte[]? authenticatorId = null,
            byte[]? publicKeyCredentialRequestOptionsJson = null,
            WindowHandle windowHandle = default
            )
        {
            ArgumentNullException.ThrowIfNull(rpId);
            ArgumentNullException.ThrowIfNull(clientData);

            var largeBlobOperation = extensions?.LargeBlob?.Operation ?? CredentialLargeBlobOperation.None;
            var largeBlob = extensions?.LargeBlob?.Write;

            if (extensions?.HmacGetSecret != null && extensions.Prf != null)
            {
                throw new ArgumentException("The hmacGetSecret and prf extensions cannot be requested together.", nameof(extensions));
            }

            if (extensions?.GetCredentialBlob == true && IsCredBlobSupported == false)
            {
                // This feature is only supported in API V3.
                throw new NotSupportedException("Credential blobs are not supported on this OS.");
            }

            if ((largeBlobOperation != CredentialLargeBlobOperation.None || largeBlob != null) && IsLargeBlobSupported == false)
            {
                // This feature is only supported in API V5.
                throw new NotSupportedException("Large blobs are not supported on this OS.");
            }

            if (browserInPrivateMode == true && IsPrivateBrowserModeIndicatorSupported == false)
            {
                // This feature is only supported in API V5.
                throw new NotSupportedException("The browser private mode indicator is not supported on this OS.");
            }

            if (extensions?.HmacGetSecret != null && IsPsuedoRandomFunctionSupported == false)
            {
                // This feature is only supported in API V6.
                throw new NotSupportedException("The PRF extension is not supported on this OS.");
            }

            if (extensions?.Prf != null && IsPsuedoRandomFunctionSupported == false)
            {
                // This feature is only supported in API V6.
                throw new NotSupportedException("The PRF extension is not supported on this OS.");
            }

            if (linkedDevice != null && IsHybridStorageLinkedDataSupported == false)
            {
                // This feature is only supported in API V7.
                throw new NotSupportedException("Hybrid storage linked data is not supported on this OS.");
            }

            if (credentialHints != null && credentialHints.Length > 0 && IsPublicKeyCredentialHintSupported == false)
            {
                // This feature is only supported in API V8.
                throw new NotSupportedException("Credential hints are not supported on this OS.");
            }

            if ((extensions?.RemoteClientDataJson is not null || extensions?.RemoteDesktopClientOverride is not null) && IsRemoteWebOriginSupported == false)
            {
                // This feature is only supported in API V9.
                throw new NotSupportedException("Remote web origin is not supported on this OS.");
            }

            if (extensions?.Uvm == true)
            {
                // The uvm extension is not exposed by the Win32 WebAuthn API (webauthn.h V9).
                throw new NotSupportedException("The uvm extension is not supported on this OS.");
            }

            if (extensions?.Payment is not null)
            {
                // The SPC payment extension is browser-only during assertion;
                // webauthn.h V9 only exposes the registration-time bThirdPartyPayment flag.
                throw new NotSupportedException("The Secure Payment Confirmation (payment) extension is not supported on this OS during assertion.");
            }

            if (!windowHandle.IsValid)
            {
                windowHandle = WindowHandle.ForegroundWindow;
            }

            // Windows 11 24H2+ system passkey providers (e.g. 1Password) receive the original
            // PublicKeyCredentialRequestOptions JSON from webauthn.dll. When the caller hasn't
            // supplied one, synthesize it from the individual parameters so plug-in authenticators
            // are still routed and appear in the OS picker.
            if (publicKeyCredentialRequestOptionsJson is null or { Length: 0 })
            {
                publicKeyCredentialRequestOptionsJson = Encoding.UTF8.GetBytes(
                    new PublicKeyCredentialRequestOptions
                    {
                        Challenge = clientData.Challenge,
                        Timeout = timeoutMilliseconds,
                        RpId = rpId,
                        AllowCredentials = allowCredentials,
                        UserVerification = userVerificationRequirement,
                        Hints = credentialHints,
                        Extensions = extensions,
                    }.ToString());
            }

            using (var allowCreds = new DisposableList<CredentialIn>())
            using (var allowCredsEx = new DisposableList<CredentialEx>())
            using (var prfCredentialEvals = new DisposableList<CredentialWithHmacSecretSaltIn>())
            {
                if (allowCredentials != null)
                {
                    allowCreds.AddRange(allowCredentials.Select(credential =>
                        new CredentialIn(credential.Id, credential.Type)));
                    allowCredsEx.AddRange(allowCredentials.Select(credential =>
                        new CredentialEx(credential.Id, credential.Type, credential.Transports)));
                }

                var nativePrfEvalByCredential = ApiHelper.Translate(extensions?.Prf?.EvalByCredential);
                if (nativePrfEvalByCredential != null)
                {
                    prfCredentialEvals.AddRange(nativePrfEvalByCredential);
                }

                using (var allowCredList = new Credentials(allowCreds.ToArray()))
                using (var allowCredListEx = new CredentialList(allowCredsEx.ToArray()))
                using (var clientDataNative = new ClientData(clientData))
                using (var legacyHmacSalt = ApiHelper.Translate(extensions?.HmacGetSecret))
                using (var prfGlobalEval = ApiHelper.TranslatePrf(extensions?.Prf?.Eval))
                using (var hmacSecretSaltValues = new HmacSecretSaltValuesIn(
                    legacyHmacSalt ?? prfGlobalEval,
                    extensions?.Prf != null ? [.. prfCredentialEvals] : null))
                using (var extensionsList = ApiHelper.Translate(extensions))
                using (var nativeExtensions = new ExtensionsIn(extensionsList.ToArray()))
                using (var options = new AuthenticatorGetAssertionOptions())
                {
                    // Prepare native options
                    options.TimeoutMilliseconds = timeoutMilliseconds;
                    options.AuthenticatorAttachment = authenticatorAttachment;
                    options.UserVerificationRequirement = userVerificationRequirement;
                    options.AllowCredentials = allowCredList;
                    options.AllowCredentialsEx = allowCredListEx;
                    options.U2fAppId = extensions?.AppID;
                    options.LargeBlobOperation = largeBlobOperation;
                    options.Extensions = nativeExtensions;
                    options.LargeBlob = largeBlob;
                    options.BrowserInPrivateMode = browserInPrivateMode;
                    options.HmacSecretSaltValues = hmacSecretSaltValues;
                    options.UseRawHmacSecretValues = extensions?.HmacGetSecret != null;
                    options.LinkedDevice = linkedDevice;
                    options.AutoFill = autoFill;
                    options.CredentialHints = credentialHints;
                    options.RemoteWebOrigin = clientData.TopOrigin;
                    options.AuthenticatorId = authenticatorId;
                    options.PublicKeyCredentialRequestOptionsJson = publicKeyCredentialRequestOptionsJson;

                    options.CancellationId = _cancellationId;

                    // Perform the Win32 API call
                    var result = NativeMethods.AuthenticatorGetAssertion(
                        windowHandle,
                        rpId,
                        clientDataNative,
                        options,
                        out var assertionHandle
                    );

                    ApiHelper.Validate(result);

                    try
                    {
                        var assertion = assertionHandle.ToManaged();

                        // Prefer native AuthenticationResponseJSON (WEBAUTHN_ASSERTION_VERSION_6+),
                        // then backfill fields that are only available through the native response structure.
                        byte[]? responseJsonBytes = assertion.AuthenticationResponseJson;
                        AssertionPublicKeyCredential? parsedJsonCredential = null;
                        if (responseJsonBytes is { Length: > 0 })
                        {
                            parsedJsonCredential = AssertionPublicKeyCredential.FromJson(Encoding.UTF8.GetString(responseJsonBytes));
                        }

                        // Assemble the native fields and extension outputs. This is the full response for older
                        // Windows versions, and supplements native JSON on newer versions.
                        var hmacSecret = assertion.HmacSecret;
                        var extensionsOut = new AuthenticationExtensionsClientAssertionOutputs();

                        if (extensions?.AppID != null)
                        {
                            extensionsOut.AppID = options.IsU2fAppIdUsed;
                        }

                        if (hmacSecret != null && extensions?.HmacGetSecret != null)
                        {
                            extensionsOut.HmacGetSecret = new HMACGetSecretOutput
                            {
                                Output1 = hmacSecret.First,
                                Output2 = hmacSecret.Second,
                            };
                        }

                        if (extensions?.Prf != null)
                        {
                            extensionsOut.Prf = new PRFAssertionOutputs
                            {
                                Results = hmacSecret != null
                                    ? new PRFValues
                                    {
                                        First = hmacSecret.First,
                                        Second = hmacSecret.Second
                                    }
                                    : null
                            };
                        }

                        if (extensions?.RemoteClientDataJson != null)
                        {
                            extensionsOut.RemoteClientDataJson = true;
                        }

                        byte[]? credBlob = assertion.Extensions?.CredBlob;
                        if (credBlob != null)
                        {
                            extensionsOut.CredentialBlob = credBlob;
                        }

                        if (largeBlobOperation == CredentialLargeBlobOperation.Get)
                        {
                            extensionsOut.LargeBlob = new LargeBlobAssertionOutputs
                            {
                                Blob = assertion.LargeBlob
                            };
                        }
                        else if (largeBlobOperation == CredentialLargeBlobOperation.Set ||
                                 largeBlobOperation == CredentialLargeBlobOperation.Delete)
                        {
                            extensionsOut.LargeBlob = new LargeBlobAssertionOutputs
                            {
                                Written = assertion.LargeBlobStatus == CredentialLargeBlobStatus.Success
                            };
                        }

                        var nativeCredential = new AssertionPublicKeyCredential()
                        {
                            Id = assertion.Credential?.Id,
                            UnsignedExtensionOutputs = assertion.UnsignedExtensionOutputs,
                            RawId = assertion.Credential?.Id,
                            Type = ApiConstants.PublicKeyCredentialType,
                            ClientExtensionResults = extensionsOut.IsEmpty ? null : extensionsOut,
                            AuthenticatorAttachment = authenticatorAttachment,
                            Response = new AuthenticatorAssertionResponse()
                            {
                                ClientData = clientDataNative.ClientDataRaw,
                                AuthenticatorData = assertion.AuthenticatorData,
                                Signature = assertion.Signature,
                                UserHandle = assertion.UserId
                            },
                        };

                        return parsedJsonCredential == null
                            ? nativeCredential
                            : parsedJsonCredential.Merge(nativeCredential);
                    }
                    finally
                    {
                        // Release native buffers.
                        assertionHandle.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="options">The credential request options that describe the relying party, allowed credentials, and the desired authenticator behavior.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the signed assertion public key credential produced by the authenticator.</returns>
        public async Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(
            PublicKeyCredentialRequestOptions options,
            WindowHandle windowHandle = default,
            CancellationToken cancellationToken = default)
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorGetAssertion(options, windowHandle), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="rpId">Identifier of the relying party requesting the assertion.</param>
        /// <param name="challenge">Cryptographic challenge produced by the relying party to be signed by the authenticator.</param>
        /// <param name="userVerificationRequirement">Indicates whether user verification is required, preferred, or discouraged.</param>
        /// <param name="authenticatorAttachment">Constrains the type of authenticator that may be used (platform, cross-platform, or any).</param>
        /// <param name="timeoutMilliseconds">Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.</param>
        /// <param name="allowCredentials">Optional list of credentials acceptable to the relying party for the assertion.</param>
        /// <param name="extensions">Client extension inputs for the assertion operation.</param>
        /// <param name="browserInPrivateMode">Indicates whether the request originates from a browser running in private/incognito mode.</param>
        /// <param name="linkedDevice">Optional hybrid (cross-device) storage linked data for state-assisted transactions.</param>
        /// <param name="autoFill">Indicates whether the request is a conditional UI (autofill) request.</param>
        /// <param name="credentialHints">Optional ordered list of public key credential hints describing the modality the relying party prefers.</param>
        /// <param name="authenticatorId">Optional identifier of a specific authenticator to target.</param>
        /// <param name="publicKeyCredentialRequestOptionsJson">Optional UTF-8 encoded JSON representation of the original request options, forwarded to the authenticator.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the signed assertion public key credential produced by the authenticator.</returns>
        public async Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(
            string rpId,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            uint timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor>? allowCredentials = null,
            AuthenticationExtensionsClientAssertionInputs? extensions = null,
            bool browserInPrivateMode = false,
            HybridStorageLinkedData? linkedDevice = null,
            bool autoFill = false,
            string[]? credentialHints = null,
            byte[]? authenticatorId = null,
            byte[]? publicKeyCredentialRequestOptionsJson = null,
            WindowHandle windowHandle = default,
            CancellationToken cancellationToken = default
        )
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorGetAssertion(
                rpId,
                challenge,
                userVerificationRequirement,
                authenticatorAttachment,
                timeoutMilliseconds,
                allowCredentials,
                extensions,
                browserInPrivateMode,
                linkedDevice,
                autoFill,
                credentialHints,
                authenticatorId,
                publicKeyCredentialRequestOptionsJson,
                windowHandle
            ), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="rpId">Identifier of the relying party requesting the assertion.</param>
        /// <param name="clientData">The client data that contains the challenge, type, origin, and related context to be signed by the authenticator.</param>
        /// <param name="userVerificationRequirement">Indicates whether user verification is required, preferred, or discouraged.</param>
        /// <param name="authenticatorAttachment">Constrains the type of authenticator that may be used (platform, cross-platform, or any).</param>
        /// <param name="timeoutMilliseconds">Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.</param>
        /// <param name="allowCredentials">Optional list of credentials acceptable to the relying party for the assertion.</param>
        /// <param name="extensions">Client extension inputs for the assertion operation.</param>
        /// <param name="browserInPrivateMode">Indicates whether the request originates from a browser running in private/incognito mode.</param>
        /// <param name="linkedDevice">Optional hybrid (cross-device) storage linked data for state-assisted transactions.</param>
        /// <param name="autoFill">Indicates whether the request is a conditional UI (autofill) request.</param>
        /// <param name="credentialHints">Optional ordered list of public key credential hints describing the modality the relying party prefers.</param>
        /// <param name="authenticatorId">Optional identifier of a specific authenticator to target.</param>
        /// <param name="publicKeyCredentialRequestOptionsJson">Optional UTF-8 encoded JSON representation of the original request options, forwarded to the authenticator.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the signed assertion public key credential produced by the authenticator.</returns>
        public async Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(
            string rpId,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            uint timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor>? allowCredentials = null,
            AuthenticationExtensionsClientAssertionInputs? extensions = null,
            bool browserInPrivateMode = false,
            HybridStorageLinkedData? linkedDevice = null,
            bool autoFill = false,
            string[]? credentialHints = null,
            byte[]? authenticatorId = null,
            byte[]? publicKeyCredentialRequestOptionsJson = null,
            WindowHandle windowHandle = default,
            CancellationToken cancellationToken = default
            )
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorGetAssertion(
                rpId,
                clientData,
                userVerificationRequirement,
                authenticatorAttachment,
                timeoutMilliseconds,
                allowCredentials,
                extensions,
                browserInPrivateMode,
                linkedDevice,
                autoFill,
                credentialHints,
                authenticatorId,
                publicKeyCredentialRequestOptionsJson,
                windowHandle
            ), cancellationToken).ConfigureAwait(false);
        }
    }
}
