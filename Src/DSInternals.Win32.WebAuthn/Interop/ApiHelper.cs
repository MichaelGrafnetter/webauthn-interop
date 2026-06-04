using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Windows.Win32;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Interop
{
    internal static class ApiHelper
    {
        /// <summary>
        /// Validates the HRESULT and throws the appropriate exception if an error occurred.
        /// </summary>
        /// <param name="result">The HRESULT to validate.</param>
        /// <exception cref="OperationCanceledException"></exception>
        /// <exception cref="TimeoutException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="Win32Exception"></exception>
        public static void Validate(HRESULT result)
        {
            if (result == HRESULT.S_OK)
            {
                // No error, so continue with code execution.
                return;
            }

            var win32Exception = new Win32Exception(result.Value);

            // Try to wrap the generic Win32Exception with a more specific .NET exception type.
            if (result == HRESULT.NTE_USER_CANCELLED ||
                result == PInvoke.HRESULT_FROM_WIN32(WIN32_ERROR.ERROR_CANCELLED))
            {
                throw new OperationCanceledException(win32Exception.Message, win32Exception);
            }
            else if (result == PInvoke.HRESULT_FROM_WIN32(WIN32_ERROR.ERROR_TIMEOUT))
            {
                throw new TimeoutException(win32Exception.Message, win32Exception);
            }
            else if (result == PInvoke.HRESULT_FROM_WIN32(WIN32_ERROR.ERROR_NOT_SUPPORTED) ||
                     result == HRESULT.NTE_NOT_SUPPORTED)
            {
                throw new NotSupportedException(win32Exception.Message, win32Exception);
            }
            else if (result == HRESULT.NTE_INVALID_PARAMETER ||
                     result == PInvoke.HRESULT_FROM_WIN32(WIN32_ERROR.ERROR_INVALID_DATA))
            {
                throw new ArgumentException(win32Exception.Message, win32Exception);
            }
            else if (result == HRESULT.RPC_E_ACCESS_DENIED)
            {
                throw new UnauthorizedAccessException(win32Exception.Message, win32Exception);
            }
            else
            {
                // Throw the generic Win32Exception if we don't have a more specific mapping for this error code.
                throw win32Exception;
            }
        }

        public static DisposableList<ExtensionIn> Translate(AuthenticationExtensionsClientAttestationInputs? extensions)
        {
            var nativeExtensions = new DisposableList<ExtensionIn>();

            if (extensions != null)
            {
                if (extensions.CredProtect != UserVerification.Any)
                {
                    nativeExtensions.Add(ExtensionIn.CreateCredProtect(
                        extensions.CredProtect,
                        extensions.EnforceCredProtect == true));
                }

                if (extensions.HmacCreateSecret == true)
                {
                    nativeExtensions.Add(ExtensionIn.CreateHmacSecret());
                }

                if (extensions.MinimumPinLength == true)
                {
                    nativeExtensions.Add(ExtensionIn.CreateMinPinLength());
                }

                if (extensions.CredentialBlob != null)
                {
                    nativeExtensions.Add(ExtensionIn.CreateCredBlobAttestation(extensions.CredentialBlob));
                }

            }

            return nativeExtensions;
        }

        public static DisposableList<ExtensionIn> Translate(AuthenticationExtensionsClientAssertionInputs? extensions)
        {
            var nativeExtensions = new DisposableList<ExtensionIn>();

            if (extensions?.GetCredentialBlob == true)
            {
                nativeExtensions.Add(ExtensionIn.CreateCredBlobAssertion());
            }

            return nativeExtensions;
        }

        public static HmacSecretSaltIn? Translate(HMACGetSecretInput? salts)
        {
            if (salts == null)
            {
                return null;
            }

            if (salts.Salt1 == null)
            {
                throw new ArgumentException("The hmacGetSecret extension requires salt1.", nameof(salts));
            }

            return new HmacSecretSaltIn(salts.Salt1, salts.Salt2);
        }

        public static HmacSecretSaltIn? TranslatePrf(PRFValues? values)
        {
            if (values == null)
            {
                return null;
            }

            byte[] first = HashPrfInput(values.First);
            byte[]? second = values.Second != null ? HashPrfInput(values.Second) : null;

            return new HmacSecretSaltIn(first, second);
        }

        public static CredentialWithHmacSecretSaltIn[]? Translate(Dictionary<string, PRFValues>? evalByCredential)
        {
            if (evalByCredential == null || evalByCredential.Count == 0)
            {
                return null;
            }

            var result = new CredentialWithHmacSecretSaltIn[evalByCredential.Count];
            int index = 0;

            foreach (var pair in evalByCredential)
            {
                if (string.IsNullOrEmpty(pair.Key))
                {
                    throw new ArgumentException("The prf evalByCredential key cannot be empty.", nameof(evalByCredential));
                }

                var credentialId = Base64UrlConverter.FromBase64UrlString(pair.Key);
                var salt = TranslatePrf(pair.Value)
                    ?? throw new ArgumentException("The prf evalByCredential value is invalid.", nameof(evalByCredential));

                result[index] = new CredentialWithHmacSecretSaltIn(credentialId, salt);
                index++;
            }

            return result;
        }

        private static byte[] HashPrfInput(byte[] input)
        {
            ArgumentNullException.ThrowIfNull(input);

            byte[] context = Encoding.UTF8.GetBytes("WebAuthn PRF");
            byte[] framedInput = new byte[context.Length + 1 + input.Length];
            context.CopyTo(framedInput, 0);
            input.CopyTo(framedInput, context.Length + 1);

            return SHA256.HashData(framedInput);
        }

        public static UserInformationIn Translate(UserInformation userInfo)
        {
            return new UserInformationIn
            {
                Id = userInfo.Id,
                DisplayName = userInfo.DisplayName,
                Icon = userInfo.Icon,
                Name = userInfo.Name
            };
        }

        public static UserInformation Translate(UserInformationOut userInfo)
        {
            return new UserInformation
            {
                Id = userInfo.Id,
                DisplayName = userInfo.DisplayName,
                Icon = userInfo.Icon,
                Name = userInfo.Name
            };
        }

        public static IList<CredentialDetails>? Translate(CredentialDetailsOut[] credentials)
        {
            if (credentials == null || credentials.Length == 0)
            {
                return null;
            }

            var result = new List<CredentialDetails>();

            foreach (var credential in credentials)
            {
                result.Add(new CredentialDetails
                {
                    UserInformation = Translate(credential.UserInformation),
                    RelyingPartyInformation = credential.RelyingPartyInformation,
                    CredentialId = credential.CredentialId,
                    BackedUp = credential.BackedUp,
                    Removable = credential.Removable
                });
            }

            return result;
        }

        public static string[]? Translate(AuthenticatorTransport transports)
        {
            if (transports == AuthenticatorTransport.NoRestrictions)
            {
                return null;
            }

            var result = new List<string>();

            if (transports.HasFlag(AuthenticatorTransport.USB)) result.Add(ApiConstants.CtapTransportUsb);
            if (transports.HasFlag(AuthenticatorTransport.NFC)) result.Add(ApiConstants.CtapTransportNfc);
            if (transports.HasFlag(AuthenticatorTransport.BLE)) result.Add(ApiConstants.CtapTransportBle);
            if (transports.HasFlag(AuthenticatorTransport.Internal)) result.Add(ApiConstants.CtapTransportInternal);
            if (transports.HasFlag(AuthenticatorTransport.Hybrid)) result.Add(ApiConstants.CtapTransportHybrid);
            if (transports.HasFlag(AuthenticatorTransport.SmartCard)) result.Add(ApiConstants.CtapTransportSmartCard);

            return result.Count > 0 ? result.ToArray() : null;
        }

        public static IList<AuthenticatorDetails>? Translate(AuthenticatorDetailsOut[] authenticators)
        {
            if (authenticators == null || authenticators.Length == 0)
            {
                return null;
            }

            var result = new List<AuthenticatorDetails>();

            foreach (var authenticator in authenticators)
            {
                result.Add(new AuthenticatorDetails
                {
                    AaGuid = DecodeAaGuid(authenticator.AuthenticatorId),
                    AuthenticatorName = authenticator.AuthenticatorName,
                    AuthenticatorLogo = DecodeBinaryLogo(authenticator.AuthenticatorLogo),
                    Locked = authenticator.Locked
                });
            }

            return result;
        }

        /// <summary>
        /// Decodes the authenticator identifier returned by the Win32 API, which is the AAGUID encoded as a big-endian GUID.
        /// </summary>
        private static Guid? DecodeAaGuid(byte[]? authenticatorId)
        {
            // The AAGUID is always a 16-byte big-endian encoded GUID.
            return authenticatorId is { Length: 16 }
                ? Guid.Create(authenticatorId, bigEndian: true)
                : null;
        }

        /// <summary>
        /// Converts a byte array logo (expected UTF-8 SVG) to a string.
        /// </summary>
        private static string? DecodeBinaryLogo(byte[]? logoBytes)
        {
            if (logoBytes == null || logoBytes.Length == 0)
            {
                return null;
            }

            try
            {
                return Encoding.UTF8.GetString(logoBytes);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Decodes a Base64-encoded UTF-8 logo string to SVG content.
        /// </summary>
        public static string? DecodeBase64Logo(string? base64Logo)
        {
            if (string.IsNullOrEmpty(base64Logo))
            {
                return null;
            }

            try
            {
                byte[] utf8Bytes = Convert.FromBase64String(base64Logo);
                string svgContent = Encoding.UTF8.GetString(utf8Bytes);

                return string.IsNullOrWhiteSpace(svgContent) ? null : svgContent;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Resolves a SID string to a user name.
        /// </summary>
        public static string? ResolveSidToUserName(string? sidString)
        {
            if (string.IsNullOrEmpty(sidString))
            {
                return null;
            }

            try
            {
                var sid = new SecurityIdentifier(sidString);
                var account = (NTAccount)sid.Translate(typeof(NTAccount));
                return account.Value;
            }
            catch
            {
                // If translation fails, return the SID string as fallback
                return sidString;
            }
        }
    }
}
