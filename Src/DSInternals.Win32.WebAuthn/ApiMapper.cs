using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Fido2NetLib;
using Fido2NetLib.Objects;
using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Performs mapping between Fido2NetLib objects and low-level data structures.
    /// </summary>
    internal static class ApiMapper
    {
        public static AttestationConveyancePreference Translate(Fido2NetLib.Objects.AttestationConveyancePreference? attestation)
        {
            switch (attestation)
            {
                case Fido2NetLib.Objects.AttestationConveyancePreference.None:
                    return AttestationConveyancePreference.None;
                case Fido2NetLib.Objects.AttestationConveyancePreference.Direct:
                    return AttestationConveyancePreference.Direct;
                case Fido2NetLib.Objects.AttestationConveyancePreference.Indirect:
                    return AttestationConveyancePreference.Indirect;
                case null:
                    return AttestationConveyancePreference.Any;
                default:
                    throw new NotSupportedException();
            }
        }

        public static RelyingPartyInformation Translate(PublicKeyCredentialRpEntity relyingParty)
        {
            if (relyingParty == null)
            {
                throw new ArgumentNullException(nameof(relyingParty));
            }

            return new RelyingPartyInformation()
            {
                Id = relyingParty.Id,
                Name = relyingParty.Name,
                Icon = relyingParty.Icon
            };
        }

        public static UserInformation Translate(Fido2User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new UserInformation()
            {
                Id = user.Id,
                Name = user.Name,
                DisplayName = user.DisplayName
            };
        }

        public static CoseCredentialParameters Translate(List<PubKeyCredParam> credParams)
        {
            if (credParams == null)
            {
                throw new ArgumentNullException(nameof(credParams));
            }

            var convertedParams = credParams.Select(item => Translate(item)).ToArray();
            return new CoseCredentialParameters(convertedParams);
        }

        public static CoseCredentialParameter Translate(PubKeyCredParam credParam)
        {
            if (credParam == null)
            {
                throw new ArgumentNullException(nameof(credParam));
            }

            return new CoseCredentialParameter(Translate(credParam.Alg), Translate(credParam.Type));
        }

        public static string Translate(PublicKeyCredentialType? credentialType)
        {
            switch (credentialType)
            {
                case PublicKeyCredentialType.PublicKey:
                    return ApiConstants.CredentialTypePublicKey;
                case null:
                    return null;
                default:
                    throw new NotSupportedException();
            }
        }

        public static DisposableList<ExtensionIn> Translate(AuthenticationExtensionsClientInputs extensions)
        {
            if (extensions == null)
            {
                // Null pointer is allowed in the parent structure.
                return null;
            }

            // TODO: Test with null Extensions
            // TODO: Test with null Excluderedentials
            var nativeExtensions = new DisposableList<ExtensionIn>();

            // TODO: Process the AppID extension

            if (extensions is WinExtensionsIn winExtensions)
            {
                if (winExtensions.CredProtect.HasValue)
                {
                    nativeExtensions.Add(ExtensionIn.CreateCredProtect(
                        winExtensions.CredProtect.Value,
                        winExtensions.EnforceCredProtect == true));
                }

                if (winExtensions.HmacSecret.HasValue)
                {
                    nativeExtensions.Add(ExtensionIn.CreateHmacSecret());
                }
            }

            return nativeExtensions;
        }

        internal static WinExtensionsOut Translate(ExtensionsOut extensions)
        {
            return new WinExtensionsOut()
            {
                HmacSecret = extensions.HmacSecret,
                CredProtect = extensions.CredProtect
            };
        }

        public static DisposableList<CredentialIn> Translate(IEnumerable<PublicKeyCredentialDescriptor> credentials)
        {
            if (credentials == null)
            {
                return null;
            }

            var result = new DisposableList<CredentialIn>();
            result.AddRange(credentials.Select(item => Translate(item)));
            return result;
        }

        public static DisposableList<CredentialEx> TranslateEx(IEnumerable<PublicKeyCredentialDescriptor> credentials)
        {
            if (credentials == null)
            {
                return null;
            }

            var result = new DisposableList<CredentialEx>();
            result.AddRange(credentials.Select(item => TranslateEx(item)));
            return result;
        }

        public static CredentialIn Translate(PublicKeyCredentialDescriptor credential)
        {
            if (credential == null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            return new CredentialIn(credential.Id, Translate(credential.Type));
        }

        public static CredentialEx TranslateEx(PublicKeyCredentialDescriptor credential)
        {
            if (credential == null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            return new CredentialEx(credential.Id, Translate(credential.Type), Translate(credential.Transports));
        }

        public static AuthenticatorGetAssertionOptions Translate(AssertionOptions options, Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment, Credentials allowCreds, CredentialList allowCredsEx)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return new AuthenticatorGetAssertionOptions()
            {
                TimeoutMilliseconds = checked((int)options.Timeout),
                AuthenticatorAttachment = Translate(authenticatorAttachment),
                AllowCredentials = allowCreds,
                AllowCredentialsEx = allowCredsEx,
                // TODO: Add support for extensions in AuthenticatorGetAssertion
                UserVerificationRequirement = ApiMapper.Translate(options.UserVerification)
                // TODO: CancellationId
                // TODO: Add support for U2fAppId
            };
        }

        public static AuthenticatorMakeCredentialOptions Translate(CredentialCreateOptions options, ExtensionsIn extensions, Credentials excludeCreds, CredentialList excludeCredsEx)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var nativeOptions = new AuthenticatorMakeCredentialOptions()
            {
                TimeoutMilliseconds = checked((int)options.Timeout),
                AuthenticatorAttachment = ApiMapper.Translate(options.AuthenticatorSelection?.AuthenticatorAttachment),
                RequireResidentKey = options.AuthenticatorSelection?.RequireResidentKey ?? false,
                AttestationConveyancePreference = ApiMapper.Translate(options.Attestation),
                UserVerificationRequirement = ApiMapper.Translate(options.AuthenticatorSelection?.UserVerification),
                Extensions = extensions,
                ExcludeCredentials = excludeCreds,
                ExcludeCredentialsEx = excludeCredsEx,
                // TODO: CancellationId
            };

            return nativeOptions;
        }

        public static ClientData Translate(CredentialCreateOptions options, bool crossOrigin = false)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var clientDataJson = JsonConvert.SerializeObject(new
            {
                Type = ApiConstants.ClientDataCredentialCreate,
                Challenge = options.Challenge,
                Origin = options.Rp?.Id,
                CrossOrigin = crossOrigin
                // TODO: Add support for TokenBinding
            });

            return new ClientData()
            {
                ClientDataJson = clientDataJson,
                // Note that SHA-256 is currently also hardcoded in Chromium and Firefox.
                HashAlgId = ApiConstants.HashAlgorithmSha256
            };
        }

        public static ClientData Translate(AssertionOptions options, bool crossOrigin = false)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var clientDataJson = JsonConvert.SerializeObject(new
            {
                Type = ApiConstants.ClientDataCredentialGet,
                Challenge = options.Challenge,
                Origin = options.RpId,
                CrossOrigin = crossOrigin
                // TODO: Add support for TokenBinding
            });

            return new ClientData()
            {
                ClientDataJson = clientDataJson,
                HashAlgId = ApiConstants.HashAlgorithmSha256
            };
        }

        public static CtapTransport Translate(AuthenticatorTransport[] transports)
        {
            if (transports == null)
            {
                return CtapTransport.NoRestrictions;
            }

            return transports.Aggregate(
                CtapTransport.NoRestrictions,
                (transportFlags, transport) => transportFlags | Translate(transport));
        }

        public static CtapTransport Translate(AuthenticatorTransport? transport)
        {
            switch (transport)
            {
                case AuthenticatorTransport.Ble:
                    return CtapTransport.BLE;
                case AuthenticatorTransport.Internal:
                    return CtapTransport.Internal;
                case AuthenticatorTransport.Nfc:
                    return CtapTransport.NFC;
                case AuthenticatorTransport.Usb:
                    return CtapTransport.USB;
                case null:
                    return CtapTransport.NoRestrictions;
                case AuthenticatorTransport.Lightning:
                default:
                    // Lightning is not supported on Windows.
                    throw new NotSupportedException();
            }
        }

        public static CoseAlgorithm Translate(long algorithm)
        {
            return checked((CoseAlgorithm)algorithm);
        }

        public static AuthenticatorAttachment Translate(Fido2NetLib.Objects.AuthenticatorAttachment? authenticatorAttachment)
        {
            switch (authenticatorAttachment)
            {
                case Fido2NetLib.Objects.AuthenticatorAttachment.CrossPlatform:
                    return AuthenticatorAttachment.CrossPlatform;
                case Fido2NetLib.Objects.AuthenticatorAttachment.Platform:
                    return AuthenticatorAttachment.Platform;
                case null:
                    return AuthenticatorAttachment.Any;
                default:
                    // TODO: Add support for AuthenticatorAttachment.CrossPlatformU2F
                    throw new NotSupportedException();
            }
        }

        public static UserVerificationRequirement Translate(Fido2NetLib.Objects.UserVerificationRequirement? userVerification)
        {
            switch (userVerification)
            {
                case Fido2NetLib.Objects.UserVerificationRequirement.Discouraged:
                    return UserVerificationRequirement.Discouraged;
                case Fido2NetLib.Objects.UserVerificationRequirement.Preferred:
                    return UserVerificationRequirement.Preferred;
                case Fido2NetLib.Objects.UserVerificationRequirement.Required:
                    return UserVerificationRequirement.Required;
                case null:
                    return UserVerificationRequirement.Any;
                default:
                    throw new NotSupportedException();
            }
        }

        public static void Validate(HResult result)
        {
            if(result == HResult.Success)
            {
                // No error, so countinue with code execution.
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
    }
}
