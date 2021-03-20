using System;
using System.Threading;
using System.Threading.Tasks;
using Fido2NetLib;

namespace DSInternals.Win32.WebAuthn.Adapter
{
    public class WebAuthnApiAdapter
    {
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
            using (var user = ApiMapper.Translate(options.User))
            using (var credParams = ApiMapper.Translate(options.PubKeyCredParams))
            using (var clientData = ApiMapper.Translate(options, false))
            using (var excludeCreds = ApiMapper.Translate(options.ExcludeCredentials))
            using (var excludeCredsEx = ApiMapper.TranslateEx(options.ExcludeCredentials))
            using (var excludeCredList = new Credentials(excludeCreds.ToArray()))
            using (var excludeCredListEx = new CredentialList(excludeCredsEx.ToArray()))
            using (var extensions = ApiMapper.Translate(options.Extensions))
            using (var extensionList = new ExtensionsIn(extensions.ToArray()))
            using (var nativeOptions = ApiMapper.Translate(options, _cancellationId, extensionList, excludeCredList, excludeCredListEx))
            {
                var result = NativeMethods.AuthenticatorMakeCredential(
                    WindowHandle.ForegroundWindow,
                    rp,
                    user,
                    credParams,
                    clientData,
                    nativeOptions,
                    out var attestationHandle
                );

                ApiMapper.Validate(result);

                try
                {
                    var attestation = attestationHandle.ToManaged();
                    return new AuthenticatorAttestationRawResponse()
                    {
                        Id = attestation.CredentialId,
                        RawId = attestation.CredentialId,
                        Type = Fido2NetLib.Objects.PublicKeyCredentialType.PublicKey,
                        Extensions = ApiMapper.Translate(attestation.Extensions),
                        Response = new AuthenticatorAttestationRawResponse.ResponseData()
                        {
                            AttestationObject = attestation.AttestationObject,
                            ClientDataJson = clientData.ClientDataRaw
                        }
                    };
                }
                finally
                {
                    attestationHandle.Dispose();
                }
            }
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

            using (var clientData = ApiMapper.Translate(options, false))
            using (var allowCreds = ApiMapper.Translate(options.AllowCredentials))
            using (var allowCredsEx = ApiMapper.TranslateEx(options.AllowCredentials))
            using (var allowCredList = new Credentials(allowCreds.ToArray()))
            using (var allowCredListEx = new CredentialList(allowCredsEx.ToArray()))
            using (var nativeOptions = ApiMapper.Translate(options, _cancellationId, authenticatorAttachment, allowCredList, allowCredListEx))
            {
                HResult result = NativeMethods.AuthenticatorGetAssertion(
                    WindowHandle.ForegroundWindow,
                    options.RpId,
                    clientData,
                    nativeOptions,
                    out var assertionHandle
                );

                ApiMapper.Validate(result);

                try
                {
                    var assertion = assertionHandle.ToManaged();
                    return new AuthenticatorAssertionRawResponse()
                    {
                        Id = assertion.Credential.Id,
                        RawId = assertion.Credential.Id,
                        Type = Fido2NetLib.Objects.PublicKeyCredentialType.PublicKey,
                        Response = new AuthenticatorAssertionRawResponse.AssertionResponse()
                        {
                            AuthenticatorData = assertion.AuthenticatorData,
                            Signature = assertion.Signature,
                            UserHandle = assertion.UserId,
                            ClientDataJson = clientData.ClientDataRaw
                        },
                        // TODO: Translate Extensions
                    };
                }
                finally
                {
                    assertionHandle.Dispose();
                }
            }
        }

        public async Task<AuthenticatorAssertionRawResponse> AuthenticatorGetAssertionAsync(AssertionOptions options, Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.Register(state => CancelCurrentOperation(), null, false);
            return await Task.Run(() => AuthenticatorGetAssertion(options, authenticatorAttachment), cancellationToken).ConfigureAwait(false);
        }
    }
}
