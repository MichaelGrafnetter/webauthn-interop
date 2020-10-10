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

        public void AuthenticatorMakeCredential(CredentialCreateOptions options)
        {
            if(options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var rp = ApiMapper.Translate(options.Rp);
            var user = ApiMapper.Translate(options.User);
            var credParams = ApiMapper.Translate(options.PubKeyCredParams);
            var clientData = ApiMapper.Translate(options, false);
            var nativeOptions = ApiMapper.Translate(options);

            var result = NativeMethods.AuthenticatorMakeCredential(
                WindowHandle.ForegroundWindow,
                rp,
                user,
                credParams,
                clientData,
                nativeOptions,
                out var attestationHandle
                );

            Validate(result);
            // TODO: Handle destroy
        }

        public void AuthenticatorGetAssertion(AssertionOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            HResult result = NativeMethods.AuthenticatorGetAssertion(
                WindowHandle.ForegroundWindow,
                options.RpId,
                ApiMapper.Translate(options, false),
                ApiMapper.Translate(options),
                out var assertionHandle
                );

            Validate(result);
            // TODO: Handle destroy
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
