using System;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using Fido2NetLib;
using Fido2NetLib.Objects;

namespace DSInternals.Win32.WebAuthn.Adapter
{
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    public class WebAuthnApiAdapter
    {
        private WebAuthnApi _api;

        public WebAuthnApiAdapter()
        {
            _api = new WebAuthnApi();
        }

        /// <summary>
        /// Creates a public key credential source bound to a managing authenticator.
        /// </summary>
        /// <param name="options">Options for credential creation.</param>
        /// <returns>The credential public key associated with the credential private key.</returns>
        public AuthenticatorAttestationRawResponse AuthenticatorMakeCredential(CredentialCreateOptions options)
        {
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var rp = ApiMapper.Translate(options.Rp);
            var credParams = ApiMapper.Translate(options.PubKeyCredParams);
            var excludeCreds = ApiMapper.Translate(options.ExcludeCredentials);
            int timeout = checked((int)options.Timeout);
            var uv = ApiMapper.Translate(options.AuthenticatorSelection?.UserVerification);
            bool rk = options.AuthenticatorSelection?.RequireResidentKey ?? false;
            var attachment = ApiMapper.Translate(options.AuthenticatorSelection?.AuthenticatorAttachment);
            var attestationPref = ApiMapper.Translate(options.Attestation);
            var user = ApiMapper.Translate(options.User);

            var attestation = _api.AuthenticatorMakeCredential(
                rp,
                user,
                options.Challenge,
                uv,
                attachment,
                rk,
                credParams,
                attestationPref,
                timeout,
                null,
                excludeCreds
            );

            return new AuthenticatorAttestationRawResponse()
            {
                // TODO: Id = attestation.CredentialId,
                // TODO: RawId = attestation.CredentialId,
                Type = PublicKeyCredentialType.PublicKey,
                // TODO: Extensions = ApiMapper.Translate(attestation.Extensions),
                Response = new AuthenticatorAttestationRawResponse.ResponseData()
                {
                    AttestationObject = attestation.AttestationObject,
                    ClientDataJson = attestation.ClientDataJson
                }
            };
        }

        public async Task<AuthenticatorAttestationRawResponse> AuthenticatorMakeCredentialAsync(CredentialCreateOptions options, CancellationToken cancellationToken = default)
        {
            cancellationToken.Register(state => CancelCurrentOperation(), null, false);
            return await Task.Run(() => AuthenticatorMakeCredential(options), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Signs a challenge and other collected data into an assertion, which is used as a credential.
        /// </summary>
        /// <param name="options">Assertion options.</param>
        /// <param name="authenticatorAttachment">Optionally filters the eligible authenticators by their attachment.</param>
        /// <returns>The cryptographically signed Authenticator Assertion Response object returned by an authenticator.</returns>
        public AuthenticatorAssertionRawResponse AuthenticatorGetAssertion(AssertionOptions options, Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment = null)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var allowCreds = ApiMapper.Translate(options.AllowCredentials);
            int timeout = checked((int)options.Timeout);
            var attachment = ApiMapper.Translate(authenticatorAttachment);
            var uv = ApiMapper.Translate(options.UserVerification);
            // TODO: U2fAppId = options.Extensions?.AppID
            //    if(options.Extensions?.AppID != null)
            //    {
            //        clientData.Add("ClientExtensions", new
            //        {
            //            AppId = options.Extensions.AppID
            //        });
            //    }

            var assertion = _api.AuthenticatorGetAssertion(
                options.RpId,
                options.Challenge,
                uv,
                attachment,
                timeout,
                allowCreds
            );
            
            return new AuthenticatorAssertionRawResponse()
            {
                // TODO: Id = assertion..Credential.Id,
                // TODO: RawId = assertion.Credential.Id,
                Type = PublicKeyCredentialType.PublicKey,
                Response = new AuthenticatorAssertionRawResponse.AssertionResponse()
                {
                    AuthenticatorData = assertion.AuthenticatorData,
                    Signature = assertion.Signature,
                    UserHandle = assertion.UserHandle,
                    ClientDataJson = assertion.ClientDataJson
                },
            };
        }

        public async Task<AuthenticatorAssertionRawResponse> AuthenticatorGetAssertionAsync(AssertionOptions options, Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.Register(state => CancelCurrentOperation(), null, false);
            return await Task.Run(() => AuthenticatorGetAssertion(options, authenticatorAttachment), cancellationToken).ConfigureAwait(false);
        }

        public void CancelCurrentOperation()
        {
            _api.CancelCurrentOperation();
        }
    }
}
