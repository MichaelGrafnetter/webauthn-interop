using System;
using System.ComponentModel;
using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn.Interop
{
    internal static class ApiHelper
    {
        public static void Validate(HResult result)
        {
            if (result == HResult.Success)
            {
                // No error, so continue with code execution.
                return;
            }

            var win32Exception = new Win32Exception(unchecked((int)result));

            // Try to wrap the generic Win32Exception with a more specific .NET exception type.
            switch (result)
            {
                case HResult.ActionCancelled:
                case HResult.OperationCancelled:
                    throw new OperationCanceledException(win32Exception.Message, win32Exception);
                case HResult.OperationTimeout:
                    throw new TimeoutException(win32Exception.Message, win32Exception);
                case HResult.RequestNotSupported:
                case HResult.OperationNotSupported:
                    throw new NotSupportedException(win32Exception.Message, win32Exception);
                case HResult.ParameterInvalid:
                case HResult.InvalidData:
                    throw new ArgumentException(win32Exception.Message, win32Exception);
                case HResult.ObjectAlreadyExists:
                case HResult.KeyStorageFull:
                case HResult.DeviceNotFound:
                case HResult.ObjectNotFound:
                default:
                    // TODO: Differentiate between more error states using custom exception types.
                    throw win32Exception;
            }
        }

        public static DisposableList<ExtensionIn> TranslateAttestationExtensions(AuthenticationExtensionsClientInputs extensions)
        {
            var nativeExtensions = new DisposableList<ExtensionIn>();

            if (extensions != null)
            {
                if (extensions.CredProtect.HasValue)
                {
                    if (!WebAuthnApi.IsCredProtectExtensionSupported)
                    {
                        // This extension is only supported in API V2.
                        throw new NotSupportedException("The Credential Protection extension is not supported on this OS.");
                    }

                    nativeExtensions.Add(ExtensionIn.CreateCredProtect(
                        extensions.CredProtect.Value,
                        extensions.EnforceCredProtect == true));
                }

                if (extensions.HmacSecret.HasValue)
                {
                    nativeExtensions.Add(ExtensionIn.CreateHmacSecret());
                }
            }

            return nativeExtensions;
        }
    }
}
