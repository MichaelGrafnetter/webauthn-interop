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

            // Windows 11 24H2+ system passkey providers (e.g. 1Password) receive the original
            // PublicKeyCredentialCreationOptions JSON from webauthn.dll. When the caller hasn't
            // supplied one, synthesize it from the individual parameters so plug-in authenticators
            // are still routed and appear in the OS picker.
            if (publicKeyCredentialCreationOptionsJson is null or { Length: 0 })
            {
                publicKeyCredentialCreationOptionsJson = Encoding.UTF8.GetBytes(
                    new PublicKeyCredentialCreationOptions
                    {
                        RelyingParty = rpEntity,
                        User = userEntity,
                        Challenge = clientData.Challenge,
                        PublicKeyCredentialParameters = pubKeyCredParams
                            .Select(alg => new PublicKeyCredentialParameter(alg, ApiConstants.PublicKeyCredentialType))
                            .ToArray(),
                        TimeoutMilliseconds = timeoutMilliseconds,
                        ExcludeCredentials = excludeCredentials,
                        AuthenticatorSelection = new AuthenticatorSelectionCriteria
                        {
                            AuthenticatorAttachment = authenticatorAttachment,
                            UserVerificationRequirement = userVerificationRequirement,
                            ResidentKey = residentKey,
                            RequireResidentKey = residentKey == ResidentKeyRequirement.Required,
                        },
                        Attestation = attestationConveyancePreference,
                        Extensions = extensions,
                        Hints = credentialHints,
                    }.ToString());
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

                        // Prefer native RegistrationResponseJSON (WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_8+),
                        // then backfill fields that are only available through the native response structure.
                        byte[]? responseJsonBytes = attestation.RegistrationResponseJson;
                        AttestationPublicKeyCredential? parsedJsonCredential = null;
                        if (responseJsonBytes is { Length: > 0 })
                        {
                            parsedJsonCredential = AttestationPublicKeyCredential.FromJson(Encoding.UTF8.GetString(responseJsonBytes));
                        }

                        // Assemble the native fields and extension outputs. This is the full response for older
                        // Windows versions, and supplements native JSON on newer versions.
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

                        var nativeCredential = new AttestationPublicKeyCredential()
                        {
                            Id = attestation.CredentialId,
                            UnsignedExtensionOutputs = attestation.UnsignedExtensionOutputs,
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

                        return parsedJsonCredential == null
                            ? nativeCredential
                            : parsedJsonCredential.Merge(nativeCredential);
                    }
                    finally
                    {
                        attestationHandle.Dispose();
                    }
                }
            }
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
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the attestation public key credential produced by the authenticator.</returns>
        public async Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(
            PublicKeyCredentialCreationOptions options,
            string? hostName = null,
            WindowHandle windowHandle = default,
            CancellationToken cancellationToken = default)
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorMakeCredential(options, hostName, windowHandle), cancellationToken).ConfigureAwait(false);
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
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the attestation public key credential produced by the authenticator.</returns>
        public async Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(
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
            WindowHandle windowHandle = default,
            CancellationToken cancellationToken = default
        )
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorMakeCredential(
                rpEntity,
                userEntity,
                challenge,
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
                hostName,
                windowHandle
            ), cancellationToken).ConfigureAwait(false);
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
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the attestation public key credential produced by the authenticator.</returns>
        public async Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(
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
            WindowHandle windowHandle = default,
            CancellationToken cancellationToken = default
            )
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorMakeCredential(
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
            ), cancellationToken).ConfigureAwait(false);
        }
    }
}
