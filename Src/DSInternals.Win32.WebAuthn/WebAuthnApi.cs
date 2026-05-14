using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSInternals.Win32.WebAuthn.FIDO;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.Win32;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Windows WebAuthn API
    /// </summary>
    /// <remarks>
    /// Requires Windows 10 1903+ to work.
    /// </remarks>
    public partial class WebAuthnApi
    {
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
                if (field.HasValue)
                {
                    // Cached value
                    return field;
                }
                else
                {
                    try
                    {
                        return field = NativeMethods.GetApiVersionNumber();
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
        /// Indicates the availability of the public key credential hints extension.
        /// </summary>
        /// <remarks>
        /// Support for credential hints was added in V8 API.
        /// </remarks>
        public static bool IsPublicKeyCredentialHintSupported => ApiVersion >= WebAuthn.ApiVersion.Version8;

        /// <summary>
        /// Indicates the availability of the authenticator list API.
        /// </summary>
        /// <remarks>
        /// Support for the authenticator list API was added in V9 API.
        /// </remarks>
        public static bool IsAuthenticatorListSupported => ApiVersion >= WebAuthn.ApiVersion.Version9;

        /// <summary>
        /// Indicates the availability of remote web origin support for proxied WebAuthn requests.
        /// </summary>
        /// <remarks>
        /// Support for remote web origin was added in V9 API.
        /// </remarks>
        public static bool IsRemoteWebOriginSupported => ApiVersion >= WebAuthn.ApiVersion.Version9;

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
        /// Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.
        /// </summary>
        /// <param name="options">The credential creation options that describe the relying party, the user, and the desired authenticator behavior.</param>
        /// <param name="hostName">
        /// Optional host name used to derive the WebAuthn origin and to fill in a missing relying party identifier.
        /// Useful for relying parties (such as Okta) that omit <c>rp.id</c> from server-issued creation options.
        /// When <see cref="RelyingPartyInformation.Id"/> is null or empty, the host name is used as the relying party identifier sent to the authenticator.
        /// </param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <returns>The attestation public key credential produced by the authenticator.</returns>
        public AttestationPublicKeyCredential AuthenticatorMakeCredential(
            PublicKeyCredentialCreationOptions options,
            string? hostName = null,
            WindowHandle windowHandle = default)
        {
            ArgumentNullException.ThrowIfNull(options);

            var authenticatorSelection = options.AuthenticatorSelection;

            return AuthenticatorMakeCredential(
                rpEntity: options.RelyingParty,
                userEntity: options.User,
                challenge: options.Challenge,
                userVerificationRequirement: authenticatorSelection?.UserVerificationRequirement ?? UserVerificationRequirement.Any,
                authenticatorAttachment: authenticatorSelection?.AuthenticatorAttachment ?? AuthenticatorAttachment.Any,
                residentKey: authenticatorSelection?.ResidentKey ?? (authenticatorSelection?.RequireResidentKey == true ? ResidentKeyRequirement.Required : ResidentKeyRequirement.Preferred),
                pubKeyCredParams: options.PublicKeyCredentialParameters.Select(p => p.Algorithm).ToArray(),
                attestationConveyancePreference: options.Attestation,
                timeoutMilliseconds: options.TimeoutMilliseconds ?? ApiConstants.DefaultTimeoutMilliseconds,
                extensions: options.Extensions,
                excludeCredentials: options.ExcludeCredentials,
                credentialHints: options.Hints?.ToArray(),
                publicKeyCredentialCreationOptionsJson: Encoding.UTF8.GetBytes(options.ToString()),
                hostName: hostName,
                windowHandle: windowHandle
                );
        }

        /// <summary>
        /// Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.
        /// </summary>
        /// <param name="rpEntity">Information about the relying party for which the credential is being created.</param>
        /// <param name="userEntity">Information about the user account the credential will be bound to.</param>
        /// <param name="challenge">Cryptographic challenge produced by the relying party to be signed by the authenticator.</param>
        /// <param name="userVerificationRequirement">Indicates whether user verification is required, preferred, or discouraged.</param>
        /// <param name="authenticatorAttachment">Constrains the type of authenticator that may be used (platform, cross-platform, or any).</param>
        /// <param name="residentKey">Indicates whether the credential should be created as a discoverable (resident) credential.</param>
        /// <param name="pubKeyCredParams">Ordered list of supported COSE algorithms for the new credential. Defaults to ES256 when null or empty.</param>
        /// <param name="attestationConveyancePreference">Specifies how the relying party wants attestation to be conveyed.</param>
        /// <param name="timeoutMilliseconds">Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.</param>
        /// <param name="excludeCredentials">Credentials that the authenticator must not create a new credential for. Used to prevent duplicate registrations.</param>
        /// <param name="enterpriseAttestation">Indicates whether enterprise attestation is requested and at what level.</param>
        /// <param name="extensions">Client extension inputs for the credential creation operation.</param>
        /// <param name="browserInPrivateMode">Indicates whether the request originates from a browser running in private/incognito mode.</param>
        /// <param name="linkedDevice">Optional hybrid (cross-device) storage linked data for state-assisted transactions.</param>
        /// <param name="credentialHints">Optional ordered list of public key credential hints describing the modality the relying party prefers.</param>
        /// <param name="authenticatorId">Optional identifier of a specific authenticator to target.</param>
        /// <param name="publicKeyCredentialCreationOptionsJson">Optional UTF-8 encoded JSON representation of the original creation options, forwarded to the authenticator.</param>
        /// <param name="hostName">Optional host name used for client data construction; defaults to the relying party identifier.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <returns>The attestation public key credential produced by the authenticator.</returns>
        public AttestationPublicKeyCredential AuthenticatorMakeCredential(
            RelyingPartyInformation rpEntity,
            UserInformation userEntity,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            ResidentKeyRequirement residentKey = ResidentKeyRequirement.Preferred,
            COSE.Algorithm[]? pubKeyCredParams = null,
            AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any,
            uint timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor>? excludeCredentials = null,
            EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None,
            AuthenticationExtensionsClientAttestationInputs? extensions = null,
            bool browserInPrivateMode = false,
            HybridStorageLinkedData? linkedDevice = null,
            string[]? credentialHints = null,
            byte[]? authenticatorId = null,
            byte[]? publicKeyCredentialCreationOptionsJson = null,
            string? hostName = null,
            WindowHandle windowHandle = default
        )
        {
            ArgumentNullException.ThrowIfNull(rpEntity);
            ArgumentNullException.ThrowIfNull(challenge);

            // TODO: Handle U2F attachment

            var clientData = CollectedClientData.Create(
                type: ApiConstants.ClientDataCredentialCreate,
                challenge: challenge,
                hostName: hostName,
                relyingPartyId: rpEntity.Id,
                remoteClientDataJson: extensions?.RemoteClientDataJson,
                remoteDesktopClientOverride: extensions?.RemoteDesktopClientOverride);

            // The Win32 WebAuthn API rejects a null rp.id with E_INVALIDARG. When the relying party
            // (e.g. Okta) omits it from server-issued options, substitute the host name so the
            // native call receives a well-formed RP entity.
            if (string.IsNullOrEmpty(rpEntity.Id) && !string.IsNullOrEmpty(hostName))
            {
                rpEntity = new RelyingPartyInformation
                {
                    Id = hostName,
                    Name = rpEntity.Name,
                    Icon = rpEntity.Icon
                };
            }

            return AuthenticatorMakeCredential(
                rpEntity,
                userEntity,
                clientData,
                userVerificationRequirement,
                authenticatorAttachment,
                residentKey,
                pubKeyCredParams,
                attestationConveyancePreference,
                timeoutMilliseconds,
                excludeCredentials,
                enterpriseAttestation,
                extensions,
                browserInPrivateMode,
                linkedDevice,
                credentialHints,
                authenticatorId,
                publicKeyCredentialCreationOptionsJson,
                windowHandle
                );
        }

        /// <summary>
        /// Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.
        /// </summary>
        /// <param name="rpEntity">Information about the relying party for which the credential is being created.</param>
        /// <param name="userEntity">Information about the user account the credential will be bound to.</param>
        /// <param name="clientData">The client data that contains the challenge, type, origin, and related context to be signed by the authenticator.</param>
        /// <param name="userVerificationRequirement">Indicates whether user verification is required, preferred, or discouraged.</param>
        /// <param name="authenticatorAttachment">Constrains the type of authenticator that may be used (platform, cross-platform, or any).</param>
        /// <param name="residentKey">Indicates whether the credential should be created as a discoverable (resident) credential.</param>
        /// <param name="pubKeyCredParams">Ordered list of supported COSE algorithms for the new credential. Defaults to ES256 when null or empty.</param>
        /// <param name="attestationConveyancePreference">Specifies how the relying party wants attestation to be conveyed.</param>
        /// <param name="timeoutMilliseconds">Timeout, in milliseconds, that the client should wait for the authenticator to complete the operation.</param>
        /// <param name="excludeCredentials">Credentials that the authenticator must not create a new credential for. Used to prevent duplicate registrations.</param>
        /// <param name="enterpriseAttestation">Indicates whether enterprise attestation is requested and at what level.</param>
        /// <param name="extensions">Client extension inputs for the credential creation operation.</param>
        /// <param name="browserInPrivateMode">Indicates whether the request originates from a browser running in private/incognito mode.</param>
        /// <param name="linkedDevice">Optional hybrid (cross-device) storage linked data for state-assisted transactions.</param>
        /// <param name="credentialHints">Optional ordered list of public key credential hints describing the modality the relying party prefers.</param>
        /// <param name="authenticatorId">Optional identifier of a specific authenticator to target.</param>
        /// <param name="publicKeyCredentialCreationOptionsJson">Optional UTF-8 encoded JSON representation of the original creation options, forwarded to the authenticator.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <returns>The attestation public key credential produced by the authenticator.</returns>
        public AttestationPublicKeyCredential AuthenticatorMakeCredential(
            RelyingPartyInformation rpEntity,
            UserInformation userEntity,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            ResidentKeyRequirement residentKey = ResidentKeyRequirement.Preferred,
            COSE.Algorithm[]? pubKeyCredParams = null,
            AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any,
            uint timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IReadOnlyList<PublicKeyCredentialDescriptor>? excludeCredentials = null,
            EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None,
            AuthenticationExtensionsClientAttestationInputs? extensions = null,
            bool browserInPrivateMode = false,
            HybridStorageLinkedData? linkedDevice = null,
            string[]? credentialHints = null,
            byte[]? authenticatorId = null,
            byte[]? publicKeyCredentialCreationOptionsJson = null,
            WindowHandle windowHandle = default
            )
        {
            ArgumentNullException.ThrowIfNull(rpEntity);
            ArgumentNullException.ThrowIfNull(userEntity);
            ArgumentNullException.ThrowIfNull(clientData);

            LargeBlobSupport largeBlobSupport = extensions?.LargeBlob?.Support ?? LargeBlobSupport.None;
            bool enablePseudoRandomFunction = extensions?.Prf != null;

            if (extensions?.CredProtect != UserVerification.Any && IsCredProtectExtensionSupported == false)
            {
                // This extension is only supported in API V2.
                throw new NotSupportedException("The Credential Protection extension is not supported on this OS.");
            }

            if (extensions?.CredentialBlob != null && IsCredBlobSupported == false)
            {
                // This extension is only supported in API V3.
                throw new NotSupportedException("The credential blob extension is not supported on this OS.");
            }

            if (extensions?.MinimumPinLength == true && IsMinPinLengthSupported == false)
            {
                // This extension is only supported in API V3.
                throw new NotSupportedException("The minPinLength extension is not supported on this OS.");
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
                using (var prfGlobalEval = ApiHelper.TranslatePrf(extensions?.Prf?.Eval))
                using (var options = new AuthenticatorMakeCredentialOptions())
                {
                    options.AttestationConveyancePreference = attestationConveyancePreference;
                    options.UserVerificationRequirement = userVerificationRequirement;
                    options.AuthenticatorAttachment = authenticatorAttachment;
                    options.TimeoutMilliseconds = timeoutMilliseconds;
                    options.Extensions = nativeExtensions;
                    options.ExcludeCredentials = excludeCredList;
                    options.ExcludeCredentialsEx = excludeCredListEx;

                    // Map ResidentKeyRequirement to Windows API bools
                    options.RequireResidentKey = residentKey == ResidentKeyRequirement.Required;
                    options.PreferResidentKey = residentKey == ResidentKeyRequirement.Preferred || residentKey == ResidentKeyRequirement.Required;

                    options.EnterpriseAttestation = enterpriseAttestation;
                    options.LargeBlobSupport = largeBlobSupport;
                    options.BrowserInPrivateMode = browserInPrivateMode;
                    options.EnablePseudoRandomFunction = enablePseudoRandomFunction;
                    options.CancellationId = _cancellationId;
                    options.LinkedDevice = linkedDevice;
                    options.CredentialHints = credentialHints;
                    options.ThirdPartyPayment = extensions?.Payment?.IsPayment == true;
                    options.PrfGlobalEval = prfGlobalEval;
                    options.RemoteWebOrigin = clientData.TopOrigin;
                    options.AuthenticatorId = authenticatorId;
                    options.PublicKeyCredentialCreationOptionsJson = publicKeyCredentialCreationOptionsJson;

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
                        var extensionResults = new AuthenticationExtensionsClientAttestationOutputs()
                        {
                            HmacSecret = attestation.Extensions?.HmacSecret ?? false,
                            CredProtect = attestation.Extensions?.CredProtect ?? UserVerification.Any,
                            MinimumPinLength = attestation.Extensions?.MinPinLength
                        };

                        if (extensions?.CredentialProperties == true)
                        {
                            extensionResults.CredentialProperties = new CredentialPropertiesOutputs
                            {
                                ResidentKey = attestation.ResidentKey
                            };
                        }

                        var credBlobCreated = attestation.Extensions?.CredBlobCreated;
                        if (credBlobCreated.HasValue)
                        {
                            extensionResults.CredentialBlobCreated = credBlobCreated.Value;
                        }

                        if (enablePseudoRandomFunction)
                        {
                            extensionResults.Prf = new PRFAttestationOutputs
                            {
                                Enabled = attestation.PseudoRandomFunctionEnabled,
                                Results = attestation.HmacSecret != null
                                    ? new PRFValues
                                    {
                                        First = attestation.HmacSecret.First,
                                        Second = attestation.HmacSecret.Second
                                    }
                                    : null
                            };
                        }

                        if (largeBlobSupport != LargeBlobSupport.None)
                        {
                            extensionResults.LargeBlob = new LargeBlobAttestationOutputs
                            {
                                Supported = attestation.LargeBlobSupported
                            };
                        }

                        if (extensions?.RemoteClientDataJson != null)
                        {
                            extensionResults.RemoteClientDataJson = true;
                        }

                        return new AttestationPublicKeyCredential()
                        {
                            Id = attestation.CredentialId,
                            RawId = attestation.CredentialId,
                            Type = ApiConstants.PublicKeyCredentialType,
                            Response = new AuthenticatorAttestationResponse()
                            {
                                ClientData = clientDataNative.ClientDataRaw,
                                AttestationObject = attestation.AttestationObject,
                                AuthenticatorData = attestation.AuthenticatorData,
                                Transports = ApiHelper.Translate(attestation.Transports)
                            },
                            AuthenticatorAttachment = authenticatorAttachment,
                            ClientExtensionResults = extensionResults.IsEmpty ? null : extensionResults
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

                        // Wrap the raw results
                        return new AssertionPublicKeyCredential()
                        {
                            Id = assertion.Credential?.Id,
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
        /// <returns>The list of platform credentials matching the filter, or <see langword="null"/> when none are present.</returns>
        /// <exception cref="NotSupportedException">Thrown when the running OS does not support platform credential management (added in API V4).</exception>
        public static IList<CredentialDetails>? GetPlatformCredentialList(string? rpId = null, bool browserInPrivateMode = false)
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
        /// Removes a public key credential stored on the platform authenticator.
        /// </summary>
        /// <param name="credentialId">The ID of the credential to be removed.</param>
        /// <exception cref="NotSupportedException">Thrown when the running OS does not support platform credential management (added in API V4).</exception>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="credentialId"/> is <see langword="null"/>.</exception>
        public static void DeletePlatformCredential(byte[] credentialId)
        {
            if (IsPlatformCredentialManagementSupported == false)
            {
                // This feature is only supported in API V4.
                throw new NotSupportedException("Credential API is not supported on this OS.");
            }

            ArgumentNullException.ThrowIfNull(credentialId);

            // Perform the Win32 API call
            var result = NativeMethods.DeletePlatformCredential(credentialId.Length, credentialId);

            ApiHelper.Validate(result);
        }

        /// <summary>
        /// Gets the list of available authenticators.
        /// </summary>
        /// <returns>The list of authenticators currently visible to the platform, or <see langword="null"/> when none are present.</returns>
        /// <exception cref="NotSupportedException">Thrown when the running OS does not support the authenticator list API (added in API V9).</exception>
        public static IList<AuthenticatorDetails>? GetAuthenticatorList()
        {
            if (IsAuthenticatorListSupported == false)
            {
                // This feature is only supported in API V9.
                throw new NotSupportedException("Authenticator list API is not supported on this OS.");
            }

            GetAuthenticatorListOptions options = new();

            // Perform the Win32 API call
            var result = NativeMethods.GetAuthenticatorList(options, out var authenticatorListHandle);
            ApiHelper.Validate(result);

            try
            {
                // Wrap the raw results
                var authenticatorList = authenticatorListHandle.ToManaged();
                return ApiHelper.Translate(authenticatorList);
            }
            finally
            {
                // Release native buffers.
                authenticatorListHandle.Dispose();
            }
        }

        /// <summary>
        /// Gets the list of registered authenticator plugins from the Windows registry.
        /// </summary>
        /// <returns>A list of authenticator plugin information, or null if no plugins are registered.</returns>
        /// <remarks>
        /// Authenticator plugins (e.g., 1Password, Bitwarden) are registered under
        /// HKLM\SOFTWARE\Microsoft\FIDO\{UserSID}\Plugins\{PluginGuid}.
        /// </remarks>
        public static IList<AuthenticatorPluginInformation>? GetPluginAuthenticators()
        {
            const string FidoRegistryPath = @"SOFTWARE\Microsoft\FIDO";
            var plugins = new List<AuthenticatorPluginInformation>();

            using var fidoKey = Registry.LocalMachine.OpenSubKey(FidoRegistryPath);
            if (fidoKey == null)
            {
                return null;
            }

            // Enumerate user SID subkeys
            foreach (var userSid in fidoKey.GetSubKeyNames())
            {
                using var userKey = fidoKey.OpenSubKey(userSid);
                if (userKey == null)
                {
                    continue;
                }

                using var pluginsKey = userKey.OpenSubKey("Plugins");
                if (pluginsKey == null)
                {
                    continue;
                }

                // Enumerate plugin GUID subkeys
                foreach (var pluginGuidString in pluginsKey.GetSubKeyNames())
                {
                    if (!Guid.TryParse(pluginGuidString, out var pluginGuid))
                    {
                        continue;
                    }

                    using var pluginKey = pluginsKey.OpenSubKey(pluginGuidString);
                    if (pluginKey == null)
                    {
                        continue;
                    }

                    var plugin = ReadPluginFromRegistry(pluginKey, userSid, pluginGuid);
                    plugins.Add(plugin);
                }
            }

            return plugins.Count > 0 ? plugins : null;
        }

        /// <summary>
        /// Reads authenticator plugin information from a registry key.
        /// </summary>
        /// <param name="pluginKey">The registry key that stores the plugin's values.</param>
        /// <param name="userSid">Security identifier of the user the plugin is registered for.</param>
        /// <param name="pluginGuid">CLSID that uniquely identifies the plugin.</param>
        private static AuthenticatorPluginInformation ReadPluginFromRegistry(RegistryKey pluginKey, string userSid, Guid pluginGuid)
        {
            var plugin = new AuthenticatorPluginInformation
            {
                UserSid = userSid,
                UserName = ApiHelper.ResolveSidToUserName(userSid),
                PluginClsid = pluginGuid,
                Name = pluginKey.GetValue("Name") as string,
                RpId = pluginKey.GetValue("RpId") as string,
                PackageFullName = pluginKey.GetValue("PackageFullName") as string,
                PackageFamilyName = pluginKey.GetValue("PackageFamilyName") as string,
                PublisherDisplayName = pluginKey.GetValue("PublisherDisplayName") as string,
                SigningKeyAlgorithm = pluginKey.GetValue("SigningKeyAlgorithm") as string,
                LightLogo = ApiHelper.DecodeBase64Logo(pluginKey.GetValue("Base64EncodedUtf16LightLogo") as string),
                DarkLogo = ApiHelper.DecodeBase64Logo(pluginKey.GetValue("Base64EncodedUtf16DarkLogo") as string),
                AuthenticatorInfo = pluginKey.GetValue("AuthenticatorInfo") as byte[]
            };

            // Read DWORD values
            if (pluginKey.GetValue("PackageSignatureKind") is int packageSignatureKind)
            {
                plugin.PackageSignatureKind = (PackageSignatureKind)packageSignatureKind;
            }

            if (pluginKey.GetValue("State") is int state)
            {
                plugin.Enabled = state != 0;
            }

            if (pluginKey.GetValue("StateToggled") is int stateToggled)
            {
                plugin.StateToggled = stateToggled != 0;
            }

            if (pluginKey.GetValue("UvCount") is int uvCount)
            {
                plugin.UvCount = (uint)uvCount;
            }

            // Read AaGuid - stored as a string in registry format "{GUID}"
            var aaGuidString = pluginKey.GetValue("AaGuid") as string;
            if (!string.IsNullOrEmpty(aaGuidString) && Guid.TryParse(aaGuidString, out var aaGuid))
            {
                plugin.AaGuid = aaGuid;
            }

            return plugin;
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
        /// Obtains a new cancellation identifier that can later be passed to <see cref="CancelCurrentOperation"/> to abort an in-progress WebAuthn operation.
        /// </summary>
        /// <returns>The cancellation identifier, or <see langword="null"/> if the running OS does not expose the asynchronous cancellation API.</returns>
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
