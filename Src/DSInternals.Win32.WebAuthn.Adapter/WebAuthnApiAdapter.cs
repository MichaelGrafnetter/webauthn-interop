using System;
using System.Collections.ObjectModel;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Objects;

namespace DSInternals.Win32.WebAuthn.Adapter
{
    /// <summary>
    /// Adapter that bridges Fido2NetLib request/response models to the WebAuthn interop API.
    /// </summary>
    public class WebAuthnApiAdapter
    {
        private WebAuthnApi _api;

        /// <summary>
        /// Initializes a new instance of the adapter.
        /// </summary>
        public WebAuthnApiAdapter()
        {
            _api = new WebAuthnApi();
        }

        /// <summary>
        /// Creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.
        /// </summary>
        /// <param name="options">The Fido2NetLib credential creation options describing the relying party, the user, and the desired authenticator behavior.</param>
        /// <returns>The raw attestation response that the relying party can persist to register the new credential.</returns>
        public AuthenticatorAttestationRawResponse AuthenticatorMakeCredential(CredentialCreateOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            var rp = ApiMapper.Translate(options.Rp);
            var credParams = ApiMapper.Translate(options.PubKeyCredParams);
            var excludeCreds = ApiMapper.Translate(options.ExcludeCredentials);
            uint timeout = checked((uint)options.Timeout);
            var uv = ApiMapper.Translate(options.AuthenticatorSelection?.UserVerification);
            var rk = ApiMapper.TranslateResidentKey(options.AuthenticatorSelection?.ResidentKey);
            var attachment = ApiMapper.Translate(options.AuthenticatorSelection?.AuthenticatorAttachment);
            var attestationPref = ApiMapper.Translate(options.Attestation);
            var user = ApiMapper.Translate(options.User);
            var extensions = ApiMapper.TranslateAttestationExtensions(options.Extensions);
            var hints = ApiMapper.Translate(options.Hints);

            var attestation = _api.AuthenticatorMakeCredential(
                rpEntity: rp,
                userEntity: user,
                challenge: options.Challenge,
                userVerificationRequirement: uv,
                authenticatorAttachment: attachment,
                residentKey: rk,
                pubKeyCredParams: credParams,
                attestationConveyancePreference: attestationPref,
                timeoutMilliseconds: timeout,
                excludeCredentials: new ReadOnlyCollection<PublicKeyCredentialDescriptor>(excludeCreds),
                extensions: extensions,
                credentialHints: hints
            );

            var attestationResponse = attestation.Response as AuthenticatorAttestationResponse
                ?? throw new InvalidOperationException("Unexpected attestation response type.");

            return new AuthenticatorAttestationRawResponse()
            {
                Id = ApiMapper.EncodeCredentialId(attestation.Id),
                RawId = attestation.Id,
                Type = PublicKeyCredentialType.PublicKey,
                Response = new AuthenticatorAttestationRawResponse.AttestationResponse()
                {
                    AttestationObject = attestationResponse.AttestationObject,
                    ClientDataJson = attestation.Response.ClientData,
                    Transports = Array.Empty<Fido2NetLib.Objects.AuthenticatorTransport>()
                },
                ClientExtensionResults = ApiMapper.Translate(attestation.ClientExtensionResults)!
            };
        }

        /// <summary>
        /// Asynchronously creates a new public key credential on the authenticator and returns the attestation that conveys its public key to the relying party.
        /// </summary>
        /// <param name="options">The Fido2NetLib credential creation options describing the relying party, the user, and the desired authenticator behavior.</param>
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>The raw attestation response that the relying party can persist to register the new credential.</returns>
        public async Task<AuthenticatorAttestationRawResponse> AuthenticatorMakeCredentialAsync(CredentialCreateOptions options, CancellationToken cancellationToken = default)
        {
            cancellationToken.Register(state => CancelCurrentOperation(), null, false);
            return await Task.Run(() => AuthenticatorMakeCredential(options), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="options">The Fido2NetLib assertion options describing the relying party, allowed credentials, and the desired authenticator behavior.</param>
        /// <param name="authenticatorAttachment">Optionally constrains the eligible authenticators by attachment (platform or cross-platform).</param>
        /// <returns>The raw assertion response containing the authenticator data, signature, and user handle returned by the authenticator.</returns>
        public AuthenticatorAssertionRawResponse AuthenticatorGetAssertion(AssertionOptions options, Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment = null)
        {
            ArgumentNullException.ThrowIfNull(options);
            if (string.IsNullOrWhiteSpace(options.RpId))
            {
                throw new ArgumentException("RP ID must be provided.", nameof(options));
            }

            var allowCreds = ApiMapper.Translate(options.AllowCredentials);
            uint timeout = checked((uint)options.Timeout);
            var attachment = ApiMapper.Translate(authenticatorAttachment);
            var uv = ApiMapper.Translate(options.UserVerification);
            var extensions = ApiMapper.TranslateAssertionExtensions(options.Extensions);
            var hints = ApiMapper.Translate(options.Hints);

            var assertion = _api.AuthenticatorGetAssertion(
                rpId: options.RpId,
                challenge: options.Challenge,
                userVerificationRequirement: uv,
                authenticatorAttachment: attachment,
                timeoutMilliseconds: timeout,
                allowCredentials: new ReadOnlyCollection<PublicKeyCredentialDescriptor>(allowCreds),
                extensions: extensions,
                credentialHints: hints
            );

            var assertionResponse = assertion.Response as AuthenticatorAssertionResponse
                ?? throw new InvalidOperationException("Unexpected assertion response type.");

            return new AuthenticatorAssertionRawResponse()
            {
                Id = ApiMapper.EncodeCredentialId(assertion.Id),
                RawId = assertion.Id ?? Array.Empty<byte>(),
                Type = PublicKeyCredentialType.PublicKey,
                Response = new AuthenticatorAssertionRawResponse.AssertionResponse()
                {
                    AuthenticatorData = assertionResponse.AuthenticatorData,
                    Signature = assertionResponse.Signature,
                    UserHandle = assertionResponse.UserHandle,
                    ClientDataJson = assertion.Response.ClientData
                },
                ClientExtensionResults = ApiMapper.Translate(assertion.ClientExtensionResults)!
            };
        }

        /// <summary>
        /// Asynchronously requests a signed assertion from the authenticator confirming the user's consent to a specific transaction, such as signing in or completing a purchase.
        /// </summary>
        /// <param name="options">The Fido2NetLib assertion options describing the relying party, allowed credentials, and the desired authenticator behavior.</param>
        /// <param name="authenticatorAttachment">Optionally constrains the eligible authenticators by attachment (platform or cross-platform).</param>
        /// <param name="cancellationToken">Token that, when canceled, signals the underlying WebAuthn operation to be canceled.</param>
        /// <returns>The raw assertion response containing the authenticator data, signature, and user handle returned by the authenticator.</returns>
        public async Task<AuthenticatorAssertionRawResponse> AuthenticatorGetAssertionAsync(AssertionOptions options, Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.Register(state => CancelCurrentOperation(), null, false);
            return await Task.Run(() => AuthenticatorGetAssertion(options, authenticatorAttachment), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Requests cancellation of the currently running WebAuthn operation.
        /// </summary>
        public void CancelCurrentOperation()
        {
            _api.CancelCurrentOperation();
        }
    }
}
