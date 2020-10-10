using System;
using Newtonsoft.Json;

namespace WebAuthN.Interop
{
    public class WebAuthN
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
                    return ApiVersion.Version1;
                }
            }
        }

        public static bool IsPlatformAuthenticatorAvailable
        {
            get
            {
                HResult result = NativeMethods.IsUserVerifyingPlatformAuthenticatorAvailable(out bool value);
                Validate(result);
                return value;
            }
        }

        public void AuthenticatorMakeCredential(Fido2NetLib.CredentialCreateOptions options)
        {
            // TODO: Check options == null

            var windowHandle = NativeMethods.GetForegroundWindow();

            // Wrap options to ClientData
            // TODO: Extract to ClientData Translate(Fido2NetLib.CredentialCreateOptions options)
            var clientData = new
            {
                // TODO: Convert "webauthn.create" to constant
                Type = "webauthn.create",
                Challenge = options.Challenge,
                Origin = options.Rp?.Id,
                CrossOrigin = false
                // TODO: TokenBinding
            };
            var clientDataJson = JsonConvert.SerializeObject(clientData);
            
            HResult result = NativeMethods.AuthenticatorMakeCredential(
                windowHandle,
                ApiMapper.Translate(options.Rp),
                ApiMapper.Translate(options.User),
                ApiMapper.Translate(options.PubKeyCredParams),
                new ClientData()
                {
                    ClientDataJSON = clientDataJson,
                    // TODO: Convert "SHA-256" to a constant
                    HashAlgId = "SHA-256"
                },
                new AuthenticatorMakeCredentialOptions()
                {
                    TimeoutMilliseconds = checked((int)options.Timeout),
                    AuthenticatorAttachment = ApiMapper.Translate(options.AuthenticatorSelection?.AuthenticatorAttachment),
                    RequireResidentKey = options.AuthenticatorSelection.RequireResidentKey,
                    AttestationConveyancePreference = ApiMapper.Translate(options.Attestation),
                    UserVerificationRequirement = ApiMapper.Translate(options.AuthenticatorSelection?.UserVerification),
                    Extensions = ApiMapper.Translate(options.Extensions),
                    // TODO: ExcludeCredentials vs. ExcludeCredentialsEx
                    ExcludeCredentialsEx = ApiMapper.Translate(options.ExcludeCredentials),
                    // TODO: CancellationId
                },
                out var attestationHandle
                );
            Validate(result);
            // TODO: Handle destroy
        }

        public void AuthenticatorGetAssertion(Fido2NetLib.AssertionOptions options)
        {
            // TODO: Check options == null

            var windowHandle = NativeMethods.GetForegroundWindow();

            // Wrap options to ClientData
            // TODO: Extract to ClientData Translate(Fido2NetLib.AssertionOptions options)
            var clientData = new
            {
                // TODO: Convert "webauthn.create" to constant
                Type = "webauthn.get",
                Challenge = options.Challenge,
                Origin = options.RpId,
                CrossOrigin = false
                // TODO: TokenBinding
            };
            var clientDataJson = JsonConvert.SerializeObject(clientData);

            HResult result = NativeMethods.AuthenticatorGetAssertion(
                windowHandle,
                options.RpId,
                new ClientData()
                {
                    ClientDataJSON = clientDataJson,
                    // TODO: Convert "SHA-256" to a constant
                    HashAlgId = "SHA-256"
                },
                new AuthenticatorGetAssertionOptions()
                {
                    TimeoutMilliseconds = checked((int)options.Timeout),
                    // TODO: AllowedCredentials vs. AllowCredentialList

                    // TODO: AllowCredentialList = ApiMapper.Translate(options.AllowCredentials),



                    Extensions = ApiMapper.Translate(options.Extensions),
                    UserVerificationRequirement = ApiMapper.Translate(options.UserVerification)
                    // TODO: AuthenticatorAttachment
                    // TODO: CancellationId
                    // TODO: U2fAppId
                },
                out var assertionHandle
                );

            Validate(result);
            // TODO: Handle destroy
        }

        public void CancelCurrentOperation()
        {
            // Note: WebAuthNCancelCurrentOperation and WebAuthNGetCancellationId are only available in newer systems.
        }

        private static void Validate(HResult result)
        {
            switch(result)
            {
                case HResult.Success:
                    break;
                case HResult.ActionCancelled:
                    throw new OperationCanceledException();
                case HResult.OperationCancelled:
                    throw new OperationCanceledException();
                case HResult.ObjectAlreadyExists:
                    // TODO: Exception type.
                    throw new NotImplementedException();
                case HResult.RequestNotSupported:
                    throw new NotSupportedException();
                case HResult.OperationNotSupported:
                    throw new NotSupportedException();
                case HResult.KeyStorageFull:
                    // TODO: Exception type.
                    throw new NotImplementedException();
                case HResult.ParameterInvalid:
                    throw new ArgumentException();
                case HResult.DeviceNotFound:
                    // TODO: Exception type.
                    throw new NotImplementedException();
                case HResult.ObjectNotFound:
                    // TODO: Exception type.
                    throw new NotImplementedException();
                case HResult.OperationTimeout:
                    throw new TimeoutException();
                default:
                    // TODO: Exception type.
                    throw new NotImplementedException();
            }
        }

        private Guid? CancellationId
        {
            get
            {
                HResult result = NativeMethods.GetCancellationId(out Guid cancelationId);
            }
        }
    }
}
