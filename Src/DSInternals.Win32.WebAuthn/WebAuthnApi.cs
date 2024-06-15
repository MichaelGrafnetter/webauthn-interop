using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Windows WebAuthn API
    /// </summary>
    /// <remarks>
    /// Requires Windows 10 1903+ to work.
    /// </remarks>
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    public partial class WebAuthnApi
    {
        private static ApiVersion? _apiVersionCache;
        private Guid? _cancellationId;

        /// <summary>
        /// Gets the API version information.
        /// </summary>
        /// <remarks>
        /// Indicates the presence of APIs and features.
        /// </remarks>
        public static ApiVersion? ApiVersion
        {
            get
            {
                if (_apiVersionCache.HasValue)
                {
                    return _apiVersionCache;
                }
                else
                {
                    try
                    {
                        _apiVersionCache = NativeMethods.GetApiVersionNumber();
                        return _apiVersionCache.Value;
                    }
                    catch (TypeLoadException)
                    {
                        // The WebAuthNGetApiVersionNumber() function was added in Windows 10 1903.
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// Indicates the availability of the WebAuthn API.
        /// </summary>
        public static bool IsAvailable => ApiVersion >= WebAuthn.ApiVersion.Version1;

        /// <summary>
        /// Indicates the availability of the Credential Protection extension.
        /// </summary>
        /// <remarks>
        /// Support for the credProtect extension was added in V2 API.
        /// </remarks>
        public static bool IsCredProtectExtensionSupported => ApiVersion >= WebAuthn.ApiVersion.Version2;

        /// <summary>
        /// Indicates the availability of enterprise attestation.
        /// </summary>
        /// <remarks>
        /// Support for the enterprise attestation was added in V3 API.
        /// </remarks>
        public static bool IsEnterpriseAttestationSupported => ApiVersion >= WebAuthn.ApiVersion.Version3;

        /// <summary>
        /// Indicates the availability of the Credential Blob extension.
        /// </summary>
        /// <remarks>
        /// Support for the credBlob extension was added in V3 API.
        /// </remarks>
        public static bool IsCredBlobSupported => ApiVersion >= WebAuthn.ApiVersion.Version3;

        /// <summary>
        /// Indicates the availability of the large blobs.
        /// </summary>
        /// <remarks>
        /// Support for the large blobs was added in V5 API.
        /// </remarks>
        public static bool IsLargeBlobSupported => ApiVersion >= WebAuthn.ApiVersion.Version5;

        /// <summary>
        /// Indicates the API can differentiate between browser modes.
        /// </summary>
        /// <remarks>
        /// Support for the browser mode indicator was added in V5 API.
        /// </remarks>
        public static bool IsPrivateBrowserModeIndicatorSupported => ApiVersion >= WebAuthn.ApiVersion.Version5;

        /// <summary>
        /// Indicates the availability of the API for platform credential management.
        /// </summary>
        /// <remarks>
        /// Support for platform credential management was added in V4 API.
        /// </remarks>
        public static bool IsPlatformCredentialManagementSupported => ApiVersion >= WebAuthn.ApiVersion.Version4;

        /// <summary>
        /// Indicates the availability of the minimum PIN length extension.
        /// </summary>
        /// <remarks>
        /// Support for the minPinLength extension was added in V3 API.
        /// </remarks>
        public static bool IsMinPinLengthSupported => ApiVersion >= WebAuthn.ApiVersion.Version3;

        /// <summary>
        /// Indicates the availability of the psuedo-random function (PRF) extension.
        /// </summary>
        /// <remarks>
        /// Support for the prf extension was added in V6 API.
        /// </remarks>
        public static bool IsPsuedoRandomFunctionSupported => ApiVersion >= WebAuthn.ApiVersion.Version6;

        /// <summary>
        /// Indicates whether operation cancellation is supported by the API.
        /// </summary>
        public bool IsCancellationSupported => _cancellationId.HasValue;

        /// <summary>
        /// Indicates the support for unsigned extension outputs.
        /// </summary>
        /// <remarks>
        /// Support for the unsigned extension outputs was added in V7 API.
        /// </remarks>
        public static bool IsUnsignedExtensionOutputSupported => ApiVersion >= WebAuthn.ApiVersion.Version7;

        /// <summary>
        /// Indicates the support for linked device data.
        /// </summary>
        /// <remarks>
        /// Support for linked device data was added in V7 API.
        /// </remarks>
        public static bool IsHybridStorageLinkedDataSupported => ApiVersion >= WebAuthn.ApiVersion.Version7;

        /// <summary>
        /// Indicates the availability of user-verifying platform authenticator (e.g. Windows Hello).
        /// </summary>
        public static bool IsUserVerifyingPlatformAuthenticatorAvailable
        {
            get
            {
                try
                {
                    var result = NativeMethods.IsUserVerifyingPlatformAuthenticatorAvailable(out var value);
                    ApiHelper.Validate(result);
                    return value;
                }
                catch (TypeLoadException)
                {
                    // If the IsUserVerifyingPlatformAuthenticatorAvailable function cannot be found, the feature is definitely not supported.
                    return false;
                }
            }
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="WebAuthnApi"/> class.
        /// </summary>
        public WebAuthnApi()
        {
            if (!IsAvailable)
            {
                throw new NotSupportedException("The WebAuthN API is not supported on this OS.");
            }

            _cancellationId = GetCancellationId();
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator and returns the credential public key
        /// associated with its credential private key.
        /// </summary>
        public PublicKeyCredential AuthenticatorMakeCredential(PublicKeyCredentialCreationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return AuthenticatorMakeCredential(
                rpEntity: options.RelyingParty,
                userEntity: options.User,
                challenge: options.Challenge,
                userVerificationRequirement: options.AuthenticatorSelection.UserVerificationRequirement,
                authenticatorAttachment: options.AuthenticatorSelection.AuthenticatorAttachment,
                requireResidentKey: options.AuthenticatorSelection.RequireResidentKey,
                pubKeyCredParams: options.PublicKeyCredentialParameters.Select(p => p.Algorithm).ToArray(),
                attestationConveyancePreference: options.Attestation,
                timeoutMilliseconds: options.TimeoutMilliseconds,
                extensions: options.Extensions,
                excludeCredentials: options.ExcludeCredentials
                );
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator and returns the credential public key
        /// associated with its credential private key.
        /// </summary>
        public PublicKeyCredential AuthenticatorMakeCredential(
            RelyingPartyInformation rpEntity,
            UserInformation userEntity,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            bool requireResidentKey = false,
            COSE.Algorithm[] pubKeyCredParams = null,
            AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor> excludeCredentials = null,
            EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None,
            AuthenticationExtensionsClientInputs extensions = null,
            LargeBlobSupport largeBlobSupport = LargeBlobSupport.None,
            bool preferResidentKey = false,
            bool browserInPrivateMode = false,
            bool enablePseudoRandomFunction = false,
            HybridStorageLinkedData linkedDevice = null,
            WindowHandle windowHandle = default
        )
        {
            if (rpEntity == null)
            {
                throw new ArgumentNullException(nameof(rpEntity));
            }

            if (rpEntity.Id == null)
            {
                throw new ArgumentException("Relying party ID must be provided.", nameof(rpEntity));
            }

            if (challenge == null)
            {
                throw new ArgumentNullException(nameof(challenge));
            }

            // TODO: Handle U2F attachment

            // Add "https://" to RpId if missing
            var origin = new UriBuilder(rpEntity.Id)
            {
                Scheme = Uri.UriSchemeHttps,
                Port = -1 // Omit port number in the URI
            };

            var clientData = new CollectedClientData()
            {
                Type = ApiConstants.ClientDataCredentialCreate,
                Challenge = challenge,
                Origin = origin.Uri.ToString().TrimEnd('/'),
                CrossOrigin = false
            };

            return AuthenticatorMakeCredential(
                rpEntity,
                userEntity,
                clientData,
                userVerificationRequirement,
                authenticatorAttachment,
                requireResidentKey,
                pubKeyCredParams,
                attestationConveyancePreference,
                timeoutMilliseconds,
                excludeCredentials,
                enterpriseAttestation,
                extensions,
                largeBlobSupport,
                preferResidentKey,
                browserInPrivateMode,
                enablePseudoRandomFunction,
                linkedDevice,
                windowHandle
                );
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator and returns the credential public key
        /// associated with its credential private key.
        /// </summary>
        public PublicKeyCredential AuthenticatorMakeCredential(
            RelyingPartyInformation rpEntity,
            UserInformation userEntity,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            bool requireResidentKey = false,
            COSE.Algorithm[] pubKeyCredParams = null,
            AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor> excludeCredentials = null,
            EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None,
            AuthenticationExtensionsClientInputs extensions = null,
            LargeBlobSupport largeBlobSupport = LargeBlobSupport.None,
            bool preferResidentKey = false,
            bool browserInPrivateMode = false,
            bool enablePseudoRandomFunction = false,
            HybridStorageLinkedData linkedDevice = null,
            WindowHandle windowHandle = default
            )
        {
            if (rpEntity == null)
            {
                throw new ArgumentNullException(nameof(rpEntity));
            }

            if (userEntity == null)
            {
                throw new ArgumentNullException(nameof(userEntity));
            }

            if (clientData == null)
            {
                throw new ArgumentNullException(nameof(clientData));
            }

            if (extensions?.CredProtect!= UserVerification.Any && IsCredProtectExtensionSupported == false)
            {
                // This extension is only supported in API V2.
                throw new NotSupportedException("The Credential Protection extension is not supported on this OS.");
            }

            if (extensions?.CredentialBlob != null && IsCredBlobSupported == false)
            {
                // This extension is only supported in API V3.
                throw new NotSupportedException("The credential blob extension is not supported on this OS.");
            }

            if (enterpriseAttestation != EnterpriseAttestationType.None && IsEnterpriseAttestationSupported == false)
            {
                // This feature is only supported in API V4.
                throw new NotSupportedException("The enterprise attestation requirement is not supported on this OS.");
            }

            if (browserInPrivateMode == true && IsPrivateBrowserModeIndicatorSupported == false)
            {
                // This feature is only supported in API V5.
                throw new NotSupportedException("The browser private mode indicator is not supported on this OS.");
            }

            if (largeBlobSupport == LargeBlobSupport.Required && IsLargeBlobSupported == false)
            {
                // This feature is only supported in API V5.
                throw new NotSupportedException("Large blobs are not supported on this OS.");
            }

            if (enablePseudoRandomFunction == true && IsPsuedoRandomFunctionSupported == false)
            {
                // This feature is only supported in API V6.
                throw new NotSupportedException("The PRF extension is not supported on this OS.");
            }

            if (linkedDevice != null && IsHybridStorageLinkedDataSupported == false)
            {
                // This feature is only supported in API V7.
                throw new NotSupportedException("Hybrid storage linked data is not supported on this OS.");
            }

            if (pubKeyCredParams == null || pubKeyCredParams.Length == 0)
            {
                // Provide a default algorithm
                pubKeyCredParams = new[] { COSE.Algorithm.ES256 };
            }

            if (!windowHandle.IsValid)
            {
                // Provide a default window handle
                windowHandle = WindowHandle.ForegroundWindow;
            }

            using (var excludeCreds = new DisposableList<CredentialIn>())
            using (var excludeCredsEx = new DisposableList<CredentialEx>())
            {
                if (excludeCredentials != null)
                {
                    excludeCreds.AddRange(excludeCredentials.Select(credential => new CredentialIn(credential.Id, credential.Type)));
                    excludeCredsEx.AddRange(excludeCredentials.Select(credential => new CredentialEx(credential.Id, credential.Type, credential.Transports)));
                }

                using (var excludeCredList = new Credentials(excludeCreds.ToArray()))
                using (var excludeCredListEx = new CredentialList(excludeCredsEx.ToArray()))
                using (var pubKeyCredParamsNative = new CoseCredentialParameters(pubKeyCredParams))
                using (var clientDataNative = new ClientData(clientData))
                using (var extensionsList = ApiHelper.Translate(extensions))
                using (var nativeExtensions = new ExtensionsIn(extensionsList.ToArray()))
                using (var userEntityNative = ApiHelper.Translate(userEntity))
                using (var options = new AuthenticatorMakeCredentialOptions())
                {
                    options.AttestationConveyancePreference = attestationConveyancePreference;
                    options.UserVerificationRequirement = userVerificationRequirement;
                    options.AuthenticatorAttachment = authenticatorAttachment;
                    options.TimeoutMilliseconds = timeoutMilliseconds;
                    options.Extensions = nativeExtensions;
                    options.ExcludeCredentials = excludeCredList;
                    options.ExcludeCredentialsEx = excludeCredListEx;
                    options.RequireResidentKey = requireResidentKey;
                    options.PreferResidentKey = preferResidentKey;
                    options.EnterpriseAttestation = enterpriseAttestation;
                    options.LargeBlobSupport = largeBlobSupport;
                    options.BrowserInPrivateMode = browserInPrivateMode;
                    options.EnablePseudoRandomFunction = enablePseudoRandomFunction;
                    options.CancellationId = _cancellationId;
                    options.LinkedDevice = linkedDevice;

                    var result = NativeMethods.AuthenticatorMakeCredential(
                        windowHandle,
                        rpEntity,
                        userEntityNative,
                        pubKeyCredParamsNative,
                        clientDataNative,
                        options,
                        out var attestationHandle
                    );

                    ApiHelper.Validate(result);

                    try
                    {
                        var attestation = attestationHandle.ToManaged();

                        return new PublicKeyCredential()
                        {
                            Id = attestation.CredentialId,
                            AuthenticatorResponse = new AuthenticatorAttestationResponse()
                            {
                                ClientDataJson = clientDataNative.ClientDataRaw,
                                AttestationObject = attestation.AttestationObject,
                            },
                            ClientExtensionResults = new AuthenticationExtensionsClientOutputs()
                            {
                                HmacSecret = attestation.Extensions?.HmacSecret ?? false,
                                CredProtect = attestation.Extensions?.CredProtect ?? UserVerification.Any,
                                MinimumPinLength = attestation.Extensions?.MinPinLength,
                                CredentialBlobCreated = attestation.Extensions?.CredBlobCreated ?? false
                            }
                        };
                    }
                    finally
                    {
                        attestationHandle.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.
        /// </summary>
        public AuthenticatorAssertionResponse AuthenticatorGetAssertion(
            string rpId,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor> allowCredentials = null,
            AuthenticationExtensionsClientInputs extensions = null,
            CredentialLargeBlobOperation largeBlobOperation = CredentialLargeBlobOperation.None,
            byte[] largeBlob = null,
            bool browserInPrivateMode = false,
            HybridStorageLinkedData linkedDevice = null,
            WindowHandle windowHandle = default
        )
        {
            if (rpId == null)
            {
                throw new ArgumentNullException(nameof(rpId));
            }

            if (challenge == null)
            {
                throw new ArgumentNullException(nameof(challenge));
            }

            // TODO: Handle U2F attachment

            // Add "https://" to RpId if missing
            var origin = new UriBuilder(rpId)
            {
                Scheme = Uri.UriSchemeHttps
            };

            var clientData = new CollectedClientData()
            {
                Type = ApiConstants.ClientDataCredentialGet,
                Challenge = challenge,
                Origin = origin.Uri.ToString(),
                CrossOrigin = false
            };

            return AuthenticatorGetAssertion(
                rpId,
                clientData,
                userVerificationRequirement,
                authenticatorAttachment,
                timeoutMilliseconds,
                allowCredentials,
                extensions,
                largeBlobOperation,
                largeBlob,
                browserInPrivateMode,
                linkedDevice,
                windowHandle
            );
        }

        /// <summary>
        /// Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.
        /// </summary>
        public AuthenticatorAssertionResponse AuthenticatorGetAssertion(
            string rpId,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor> allowCredentials = null,
            AuthenticationExtensionsClientInputs extensions = null,
            CredentialLargeBlobOperation largeBlobOperation = CredentialLargeBlobOperation.None,
            byte[] largeBlob = null,
            bool browserInPrivateMode = false,
            HybridStorageLinkedData linkedDevice = null,
            WindowHandle windowHandle = default
            )
        {
            if (rpId == null)
            {
                throw new ArgumentNullException(nameof(rpId));
            }

            if (clientData == null)
            {
                throw new ArgumentNullException(nameof(clientData));
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

            if (linkedDevice != null && IsHybridStorageLinkedDataSupported == false)
            {
                // This feature is only supported in API V7.
                throw new NotSupportedException("Hybrid storage linked data is not supported on this OS.");
            }

            if (!windowHandle.IsValid)
            {
                windowHandle = WindowHandle.ForegroundWindow;
            }

            using (var allowCreds = new DisposableList<CredentialIn>())
            using (var allowCredsEx = new DisposableList<CredentialEx>())
            {
                if (allowCredentials != null)
                {
                    allowCreds.AddRange(allowCredentials.Select(credential =>
                        new CredentialIn(credential.Id, credential.Type)));
                    allowCredsEx.AddRange(allowCredentials.Select(credential =>
                        new CredentialEx(credential.Id, credential.Type, credential.Transports)));
                }

                using (var allowCredList = new Credentials(allowCreds.ToArray()))
                using (var allowCredListEx = new CredentialList(allowCredsEx.ToArray()))
                using (var clientDataNative = new ClientData(clientData))
                using (var globalHmacSalt = ApiHelper.Translate(extensions?.HmacGetSecret))
                using (var hmacSecretSaltValues = new HmacSecretSaltValuesIn(globalHmacSalt, null))
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
                    options.LinkedDevice = linkedDevice;

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

                        var extensionsOut = new AuthenticationExtensionsClientOutputs()
                        {
                            HmacGetSecret = new HMACGetSecretOutput
                            {
                                Output1 = assertion.HmacSecret?.First,
                                Output2 = assertion.HmacSecret?.Second,
                            }
                        };

                        byte[] credBlob = assertion.Extensions?.CredBlob;

                        // Wrap the raw results
                        return new AuthenticatorAssertionResponse()
                        {
                            ClientDataJson = clientDataNative.ClientDataRaw,
                            AuthenticatorData = assertion.AuthenticatorData,
                            Signature = assertion.Signature,
                            UserHandle = assertion.UserId
                        };
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
        /// Gets the list of stored credentials.
        /// </summary>
        /// <param name="rpId">Optional Id of the relying party that is making the request.</param>
        /// <param name="browserInPrivateMode">Indicates whether the browser is in private mode.</param>
        /// <exception cref="NotSupportedException"></exception>
        public static IList<CredentialDetails> GetPlatformCredentialList(string rpId = null, bool browserInPrivateMode = false)
        {
            if (IsPlatformCredentialManagementSupported == false)
            {
                // This feature is only supported in API V4.
                throw new NotSupportedException("Credential API is not supported on this OS.");
            }

            var options = new GetCredentialsOptions()
            {
                RpId = string.IsNullOrEmpty(rpId) ? null : rpId,
                BrowserInPrivateMode = browserInPrivateMode
            };

            // Perform the Win32 API call
            var result = NativeMethods.GetPlatformCredentialList(options, out var credentialListHandle);
            ApiHelper.Validate(result);

            try
            {
                // Wrap the raw results
                var credentialList = credentialListHandle.ToManaged();
                return ApiHelper.Translate(credentialList);
            }
            finally
            {
                // Release native buffers.
                credentialListHandle.Dispose();
            }
        }

        /// <summary>
        /// Removes a Public Key Credential Source stored on a Virtual Authenticator.
        /// </summary>
        /// <param name="credentialId">The ID of the credential to be removed.</param>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static void DeletePlatformCredential(byte[] credentialId)
        {
            if (IsPlatformCredentialManagementSupported == false)
            {
                // This feature is only supported in API V4.
                throw new NotSupportedException("Credential API is not supported on this OS.");
            }

            if (credentialId == null)
            {
                throw new ArgumentNullException(nameof(credentialId));
            }

            // Perform the Win32 API call
            var result = NativeMethods.DeletePlatformCredential(credentialId.Length, credentialId);

            ApiHelper.Validate(result);
        }

        /// <summary>
        /// Cancels the WebAuthn operation currently in progress.
        /// </summary>
        /// <remarks>
        /// When this operation is invoked by the client in an authenticator session,
        /// it has the effect of terminating any AuthenticatorMakeCredential or AuthenticatorGetAssertion operation
        /// currently in progress in that authenticator session.
        /// The authenticator stops prompting for, or accepting, any user input related to authorizing the canceled operation. The client ignores any further responses from the authenticator for the canceled operation.
        /// </remarks>
        public void CancelCurrentOperation()
        {
            if (_cancellationId.HasValue)
            {
                var result = NativeMethods.CancelCurrentOperation(_cancellationId.Value);
                ApiHelper.Validate(result);
            }
        }

        /// <summary>
        /// Gets the cancellation ID for a canceled operation.
        /// </summary>
        /// <returns>ID of the cancelled operation.</returns>
        private static Guid? GetCancellationId()
        {
            try
            {
                var result = NativeMethods.GetCancellationId(out var cancellationId);
                ApiHelper.Validate(result);
                return cancellationId;
            }
            catch (TypeLoadException)
            {
                // Async support is not present in earlier versions of Windows 10.
                return null;
            }
        }
    }
}
