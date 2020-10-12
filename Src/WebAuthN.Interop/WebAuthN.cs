using System;
using Fido2NetLib;

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
                    throw new NotSupportedException();
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

        public AuthenticatorAttestationRawResponse AuthenticatorMakeCredential(CredentialCreateOptions options)
        {
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var rp = ApiMapper.Translate(options.Rp);
            var user = ApiMapper.Translate(options.User);
            using (var credParams = ApiMapper.Translate(options.PubKeyCredParams))
            using (var clientData = ApiMapper.Translate(options, false))
            using (var excludeCreds = ApiMapper.Translate(options.ExcludeCredentials))
            // TODO: Get rid of ToArray
            using (var excludeCredList = new CredentialExListIn(excludeCreds.ToArray()))
            using (var nativeOptions = ApiMapper.Translate(options))
            {
                // TODO: Add ExcludeCredentialsEx = excludeCredList to Translate
                nativeOptions.ExcludeCredentialsEx = excludeCredList;

                var result = NativeMethods.AuthenticatorMakeCredential(
                    WindowHandle.ForegroundWindow.Handle,
                    rp,
                    user,
                    credParams,
                    clientData,
                    nativeOptions,
                    out var attestationHandle
                );

                Validate(result);

                try
                {
                    var attestation = attestationHandle.ToManaged();
                    /* TODO:
                    attestation.Attestation
                    attestation.AttestationDecoded
                    attestation.AttestationObject,
                    attestation.AuthenticatorData,
                    attestation.FormatType
                    attestation.UsedTransport
                    */
                    return new AuthenticatorAttestationRawResponse()
                    {
                        // TODO: Extensions = attestation.Extensions.Data,
                        Id = attestation.CredentialId.Data,
                        RawId = attestation.CredentialId.Data,
                        Type = Fido2NetLib.Objects.PublicKeyCredentialType.PublicKey,
                        Response = new AuthenticatorAttestationRawResponse.ResponseData()
                        {
                            AttestationObject = attestation.AttestationObject.Data
                            // TODO: ClientDataJson = attestation.
                        }
                    };
                }
                finally
                {
                    attestationHandle.Dispose();
                }
            }
        }

        public AuthenticatorAssertionRawResponse AuthenticatorGetAssertion(AssertionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            using (var clientData = ApiMapper.Translate(options, false))
            using (var nativeOptions = ApiMapper.Translate(options))
            {
                HResult result = NativeMethods.AuthenticatorGetAssertion(
                    WindowHandle.ForegroundWindow.Handle,
                    options.RpId,
                    clientData,
                    nativeOptions,
                    out var assertionHandle
                );

                Validate(result);
                // TODO: Handle destroy
                return null;
            }
        }

        public void CancelCurrentOperation()
        {
            // Note: WebAuthNCancelCurrentOperation and WebAuthNGetCancellationId are only available in newer systems.
        }

        protected static Guid? CancellationId
        {
            get
            {
                try
                {
                    HResult result = NativeMethods.GetCancellationId(out Guid cancelationId);
                    Validate(result);
                    return cancelationId;
                }
                catch(EntryPointNotFoundException)
                {
                    // Async support is not present in earlier versions of Windows 10.
                    return null;
                }
            }
        }

        private static void Validate(HResult result)
        {
            switch (result)
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
                case HResult.InvalidData:
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
    }
}
