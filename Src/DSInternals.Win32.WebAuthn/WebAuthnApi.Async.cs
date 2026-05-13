using System.Collections.Generic;
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
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the attestation public key credential produced by the authenticator.</returns>
        public Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(
            PublicKeyCredentialCreationOptions options,
            CancellationToken cancellationToken = default)
        {
            return AuthenticatorMakeCredentialAsync(options, default, cancellationToken);
        }

        /// <summary>
        /// Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.
        /// </summary>
        /// <param name="options">The credential creation options that describe the relying party, the user, and the desired authenticator behavior.</param>
        /// <param name="windowHandle">Handle to the window that will own the authenticator UI. When invalid, the foreground window is used.</param>
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the attestation public key credential produced by the authenticator.</returns>
        public async Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(
            PublicKeyCredentialCreationOptions options,
            WindowHandle windowHandle,
            CancellationToken cancellationToken = default)
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorMakeCredential(options, windowHandle)).ConfigureAwait(false);
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
            )).ConfigureAwait(false);
        }

        /// <summary>
        /// Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="options">The credential request options that describe the relying party, allowed credentials, and the desired authenticator behavior.</param>
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>A task that completes with the signed assertion public key credential produced by the authenticator.</returns>
        public Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(
            PublicKeyCredentialRequestOptions options,
            CancellationToken cancellationToken = default)
        {
            return AuthenticatorGetAssertionAsync(options, default, cancellationToken);
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
            WindowHandle windowHandle,
            CancellationToken cancellationToken = default)
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorGetAssertion(options, windowHandle)).ConfigureAwait(false);
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
            )).ConfigureAwait(false);
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
            )).ConfigureAwait(false);
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
            )).ConfigureAwait(false);
        }
    }
}
