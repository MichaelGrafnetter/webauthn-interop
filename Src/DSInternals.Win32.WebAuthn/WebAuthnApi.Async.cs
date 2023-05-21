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
        public async Task<AuthenticatorAttestationResponse> AuthenticatorMakeCredentialAsync(
            RelyingPartyInformation rpEntity,
            UserInformation userEntity,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            bool requireResidentKey = false,
            COSE.Algorithm[] pubKeyCredParams = null,
            AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            AuthenticationExtensionsClientInputs extensions = null,
            IList<PublicKeyCredentialDescriptor> excludeCredentials = null,
            EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None,
            LargeBlobSupport largeBlobSupport = LargeBlobSupport.None,
            bool preferResidentKey = false,
            bool browserInPrivateMode = false,
            bool enablePseudoRandomFunction = false,
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
                requireResidentKey,
                pubKeyCredParams,
                attestationConveyancePreference,
                timeoutMilliseconds,
                extensions,
                excludeCredentials,
                enterpriseAttestation,
                largeBlobSupport,
                preferResidentKey,
                browserInPrivateMode,
                enablePseudoRandomFunction,
                windowHandle
            )).ConfigureAwait(false);
        }

        /// <summary>
        /// Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.
        /// </summary>
        public async Task<AuthenticatorAssertionResponse> AuthenticatorGetAssertionAsync(
            string rpId,
            byte[] challenge,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IList<PublicKeyCredentialDescriptor> allowCredentials = null,
            AuthenticationExtensionsClientInputs extensions = null,
            CredentialLargeBlobOperation largeBlobOperation = CredentialLargeBlobOperation.None,
            byte[] largeBlob = null,
            bool browserInPrivateMode = false,
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
                largeBlobOperation,
                largeBlob,
                browserInPrivateMode,
                windowHandle
            )).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator and returns the credential public key
        /// associated with its credential private key.
        /// </summary>
        public async Task<AuthenticatorAttestationResponse> AuthenticatorMakeCredentialAsync(
            RelyingPartyInformation rpEntity,
            UserInformation userEntity,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            bool requireResidentKey = false,
            COSE.Algorithm[] pubKeyCredParams = null,
            AttestationConveyancePreference attestationConveyancePreference = AttestationConveyancePreference.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IList<PublicKeyCredentialDescriptor> excludeCredentials = null,
            EnterpriseAttestationType enterpriseAttestation = EnterpriseAttestationType.None,
            LargeBlobSupport largeBlobSupport = LargeBlobSupport.None,
            bool preferResidentKey = false,
            bool browserInPrivateMode = false,
            bool enablePseudoRandomFunction = false,
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
                requireResidentKey,
                pubKeyCredParams,
                attestationConveyancePreference,
                timeoutMilliseconds,
                excludeCredentials,
                enterpriseAttestation,
                largeBlobSupport,
                preferResidentKey,
                browserInPrivateMode,
                enablePseudoRandomFunction,
                windowHandle
            )).ConfigureAwait(false);
        }

            /// <summary>
            /// Produces an assertion signature representing an assertion by the authenticator that the user has consented to a specific transaction, such as logging in or completing a purchase.
            /// </summary>
            public async Task<AuthenticatorAssertionResponse> AuthenticatorGetAssertionAsync(
            string rpId,
            CollectedClientData clientData,
            UserVerificationRequirement userVerificationRequirement,
            AuthenticatorAttachment authenticatorAttachment = AuthenticatorAttachment.Any,
            int timeoutMilliseconds = ApiConstants.DefaultTimeoutMilliseconds,
            IList<PublicKeyCredentialDescriptor> allowCredentials = null,
            CredentialLargeBlobOperation largeBlobOperation = CredentialLargeBlobOperation.None,
            byte[] largeBlob = null,
            bool browserInPrivateMode = false,
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
                largeBlobOperation,
                largeBlob,
                browserInPrivateMode,
                windowHandle
            )).ConfigureAwait(false);
        }
    }
}
