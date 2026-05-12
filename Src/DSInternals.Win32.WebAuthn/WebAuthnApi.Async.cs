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
        /// Creates a public key credential source bound to a managing authenticator and returns the credential public key
        /// associated with its credential private key.
        /// </summary>
        public Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(
            PublicKeyCredentialCreationOptions options,
            CancellationToken cancellationToken = default)
        {
            return AuthenticatorMakeCredentialAsync(options, default, cancellationToken);
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator and returns the credential public key
        /// associated with its credential private key.
        /// </summary>
        public async Task<AttestationPublicKeyCredential> AuthenticatorMakeCredentialAsync(
            PublicKeyCredentialCreationOptions options,
            WindowHandle windowHandle,
            CancellationToken cancellationToken = default)
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorMakeCredential(options, windowHandle)).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator and returns the credential public key
        /// associated with its credential private key.
        /// </summary>
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
        /// Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.
        /// </summary>
        public Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(
            PublicKeyCredentialRequestOptions options,
            CancellationToken cancellationToken = default)
        {
            return AuthenticatorGetAssertionAsync(options, default, cancellationToken);
        }

        /// <summary>
        /// Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.
        /// </summary>
        public async Task<AssertionPublicKeyCredential> AuthenticatorGetAssertionAsync(
            PublicKeyCredentialRequestOptions options,
            WindowHandle windowHandle,
            CancellationToken cancellationToken = default)
        {
            using var cancellationTokenRegistration = cancellationToken.Register(() => { this.CancelCurrentOperation(); });
            return await Task.Run(() => AuthenticatorGetAssertion(options, windowHandle)).ConfigureAwait(false);
        }

        /// <summary>
        /// Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.
        /// </summary>
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
        /// Creates a public key credential source bound to a managing authenticator and returns the credential public key
        /// associated with its credential private key.
        /// </summary>
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
        /// Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.
        /// </summary>
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
