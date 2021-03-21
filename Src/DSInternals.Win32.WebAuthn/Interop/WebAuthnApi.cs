using System;
using System.Collections.Generic;
using System.Linq;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Windows WebAuthn API
    /// </summary>
    /// <remarks>
    /// Requires Windows 10 1903+ to work.
    /// </remarks>
    public class WebAuthnApi
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
                if(_apiVersionCache.HasValue)
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
                    catch (EntryPointNotFoundException)
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
        public static bool IsAvailable => ApiVersion >= Interop.ApiVersion.Version1;

        /// <summary>
        /// Indicates the availability of the Credential Protection extension.
        /// </summary>
        /// <remarks>
        /// Support for the credProtect extension was added in V2 API.
        /// </remarks>
        public static bool IsCredProtectExtensionSupported => ApiVersion >= Interop.ApiVersion.Version2;

        /// <summary>
        /// Indicates whether operation cancellation is supported by the API.
        /// </summary>
        public bool IsCancellationSupported => _cancellationId.HasValue;

        /// <summary>
        /// Indicates the availability of user-verifying platform authenticator (e.g. Windows Hello).
        /// </summary>
        public static bool IsUserVerifyingPlatformAuthenticatorAvailable
        {
            get
            {
                try
                {
                    HResult result = NativeMethods.IsUserVerifyingPlatformAuthenticatorAvailable(out bool value);
                    ApiHelper.Validate(result);
                    return value;
                }
                catch(EntryPointNotFoundException)
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
            _cancellationId = GetCancellationId();
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator.
        /// </summary>
        public AuthenticatorAttestationResponse AuthenticatorMakeCredential(
            RelyingPartyInformation rpEntity,
            UserInformation userEntity,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            bool requireResidentKey = false,
            Algorithm[] pubKeyCredParams = null,
            AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IList<PublicKeyCredentialDescriptor> excludeCredentials = null,
            WindowHandle windowHandle = default
        )
        {
            if(rpEntity == null)
            {
                throw new ArgumentNullException(nameof(rpEntity));
            }

            if(rpEntity.Id == null)
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
                Scheme = Uri.UriSchemeHttps
            };

            var clientData = new CollectedClientData()
            {
                Type = ApiConstants.ClientDataCredentialCreate,
                Challenge = challenge,
                Origin = origin.Uri.ToString(),
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
                windowHandle
                );
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator.
        /// </summary>
        public AuthenticatorAttestationResponse AuthenticatorMakeCredential(
            RelyingPartyInformation rpEntity,
            UserInformation userEntity,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            bool requireResidentKey = false,
            Algorithm[] pubKeyCredParams = null,
            AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IList<PublicKeyCredentialDescriptor> excludeCredentials = null,
            WindowHandle windowHandle = default
            )
        {
            if(rpEntity == null)
            {
                throw new ArgumentNullException(nameof(rpEntity));
            }

            if(userEntity == null)
            {
                throw new ArgumentNullException(nameof(userEntity));
            }

            if (clientData == null)
            {
                throw new ArgumentNullException(nameof(clientData));
            }

            if (clientData.ClientExtensions?.CredProtect.HasValue == true && WebAuthnApi.IsCredProtectExtensionSupported == false)
            {
                // This extension is only supported in API V2.
                throw new NotSupportedException("The Credential Protection extension is not supported on this OS.");
            }

            if (pubKeyCredParams == null)
            {
                pubKeyCredParams = new[] { Algorithm.ES256 };
            }

            if (!windowHandle.IsValid)
            {
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
                using (var extensionsList = ApiHelper.TranslateAttestationExtensions(clientData.ClientExtensions))
                using (var nativeExtensions = new ExtensionsIn(extensionsList.ToArray()))
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
                    options.CancellationId = _cancellationId;

                    HResult result = NativeMethods.AuthenticatorMakeCredential(
                        windowHandle,
                        rpEntity,
                        userEntity,
                        pubKeyCredParamsNative,
                        clientDataNative,
                        options,
                        out var attestationHandle
                    );

                    ApiHelper.Validate(result);

                    try
                    {
                        var attestation = attestationHandle.ToManaged();

                        var extensions = new AuthenticationExtensionsClientOutputs()
                        {
                            HmacSecret = attestation.Extensions?.HmacSecret,
                            CredProtect = attestation.Extensions?.CredProtect
                        };

                        return new AuthenticatorAttestationResponse()
                        {
                            ClientDataJson = clientDataNative.ClientDataRaw,
                            AttestationObject = attestation.AttestationObject
                            // TODO: Return more data from the attestation.
                            // attestation.Attestation
                            // attestation.AttestationDecoded
                            // attestation.AuthenticatorData
                            // attestation.CredentialId
                            // attestation.UsedTransport
                            // attestation.FormatType
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
        /// Signs a challenge and other collected data into an assertion, which is used as a credential.
        /// </summary>
        public AuthenticatorAssertionResponse AuthenticatorGetAssertion(
            string rpId,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IList<PublicKeyCredentialDescriptor> allowCredentials = null,
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
                windowHandle
            );
        }

        /// <summary>
        /// Signs a challenge and other collected data into an assertion, which is used as a credential.
        /// </summary>
        public AuthenticatorAssertionResponse AuthenticatorGetAssertion(
            string rpId,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IList<PublicKeyCredentialDescriptor> allowCredentials = null,
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
                using (var options = new AuthenticatorGetAssertionOptions())
                {
                    // Prepare native options
                    options.TimeoutMilliseconds = timeoutMilliseconds;
                    options.AuthenticatorAttachment = authenticatorAttachment;
                    options.UserVerificationRequirement = userVerificationRequirement;
                    options.AllowCredentials = allowCredList;
                    options.AllowCredentialsEx = allowCredListEx;
                    options.CancellationId = _cancellationId;
                    options.U2fAppId = clientData.ClientExtensions?.AppID;

                    // Perform the Win32 API call
                    HResult result = NativeMethods.AuthenticatorGetAssertion(
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
            if(_cancellationId.HasValue)
            {
                HResult result = NativeMethods.CancelCurrentOperation(_cancellationId.Value);
                ApiHelper.Validate(result);
            }
        }

        private static Guid? GetCancellationId()
        {
            try
            {
                HResult result = NativeMethods.GetCancellationId(out Guid cancellationId);
                ApiHelper.Validate(result);
                return cancellationId;
            }
            catch(EntryPointNotFoundException)
            {
                // Async support is not present in earlier versions of Windows 10.
                return null;
            }
        }
    }
}
