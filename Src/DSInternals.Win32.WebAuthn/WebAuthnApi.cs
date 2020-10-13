using System;
using Fido2NetLib;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Windows WebAuthn API
    /// </summary>
    /// <remarks>
    /// Requires Windows 10 1903+ to work.
    /// </remarks>
    public class WebAuthnApi
    {
        // TODO: Async operations
        public static ApiVersion ApiVersion
        {
            get
            {
                try
                {
                    return NativeMethods.GetApiVersionNumber();
                }
                catch(EntryPointNotFoundException)
                {
                    // The WebAuthNGetApiVersionNumber() function was added in Windows 10 1903.
                    throw new NotSupportedException();
                }
            }
        }

        public static bool IsAvailable
        {
            get
            {
                // TODO: Check the real API availability: return is_bound_ && (api_version_ >= WEBAUTHN_API_VERSION_1);
                return true;
            }
        }

        public static bool IsCredProtectExtensionSupported
        {
            get
            {
                return IsAvailable && ApiVersion >= ApiVersion.Version2;
            }
        }

        public static bool IsPlatformAuthenticatorAvailable
        {
            get
            {
                HResult result = NativeMethods.IsUserVerifyingPlatformAuthenticatorAvailable(out bool value);
                ApiMapper.Validate(result);
                return value;
            }
        }

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
            using (var nativeOptions = ApiMapper.Translate(options, extensionList, excludeCredList, excludeCredListEx))
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
                        // TODO: Extensions = attestation.Extensions.Data,
                        Id = attestation.CredentialId,
                        RawId = attestation.CredentialId,
                        Type = Fido2NetLib.Objects.PublicKeyCredentialType.PublicKey,
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
            using (var nativeOptions = ApiMapper.Translate(options, authenticatorAttachment, allowCredList, allowCredListEx))
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

        public void CancelCurrentOperation()
        {
            // TODO: Implement cancellation
            // Note: WebAuthNCancelCurrentOperation and WebAuthNGetCancellationId are only available in newer systems.
        }

        protected static Guid? CancellationId
        {
            get
            {
                try
                {
                    HResult result = NativeMethods.GetCancellationId(out Guid cancelationId);
                    ApiMapper.Validate(result);
                    return cancelationId;
                }
                catch(EntryPointNotFoundException)
                {
                    // Async support is not present in earlier versions of Windows 10.
                    return null;
                }
            }
        }
    }
}
