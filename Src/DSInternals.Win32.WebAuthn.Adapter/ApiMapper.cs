using System;
using System.Collections.Generic;
using System.Linq;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Fido2NetLib.Objects;
using Newtonsoft.Json;
using AttestationConveyancePreference = DSInternals.Win32.WebAuthn.FIDO.AttestationConveyancePreference;
using AuthenticatorAttachment = DSInternals.Win32.WebAuthn.FIDO.AuthenticatorAttachment;
using AuthenticatorTransport = DSInternals.Win32.WebAuthn.FIDO.AuthenticatorTransport;
using PublicKeyCredentialDescriptor = DSInternals.Win32.WebAuthn.FIDO.PublicKeyCredentialDescriptor;
using UserVerificationRequirement = DSInternals.Win32.WebAuthn.FIDO.UserVerificationRequirement;

namespace DSInternals.Win32.WebAuthn.Adapter
{
    /// <summary>
    /// Performs mapping between Fido2NetLib objects and low-level data structures.
    /// </summary>
    public static class ApiMapper
    {
        public static RelyingPartyInformation Translate(Fido2NetLib.PublicKeyCredentialRpEntity relyingParty)
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

        public static UserInformation Translate(Fido2NetLib.Fido2User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return new UserInformation()
            {
                Id = user.Id,
                Name = user.Name,
                DisplayName = user.DisplayName,
                Icon = String.Empty // Browser implementations typically use an empty string instead of null
            };
        }

        public static Algorithm[] Translate(IList<Fido2NetLib.PubKeyCredParam> credParams)
        {
            if (credParams == null)
            {
                throw new ArgumentNullException(nameof(credParams));
            }

            return credParams.Select(item => Translate(item.Alg)).ToArray();
        }

        public static Algorithm Translate(long algorithm)
        {
            return checked((Algorithm)algorithm);
        }

        public static IList<PublicKeyCredentialDescriptor> Translate(IEnumerable<Fido2NetLib.Objects.PublicKeyCredentialDescriptor> credentials)
        {
            var result = new List<PublicKeyCredentialDescriptor>();

            if (credentials != null)
            {
                result.AddRange(credentials.Select(item => Translate(item)));
            }

            return result;
        }

        public static PublicKeyCredentialDescriptor Translate(Fido2NetLib.Objects.PublicKeyCredentialDescriptor credential)
        {
            return new PublicKeyCredentialDescriptor(credential.Id, Translate(credential.Transports), Translate(credential.Type));
        }

        public static AuthenticatorTransport Translate(Fido2NetLib.Objects.AuthenticatorTransport[] transports)
        {
            if (transports == null)
            {
                return AuthenticatorTransport.NoRestrictions;
            }

            return transports.Aggregate(
                AuthenticatorTransport.NoRestrictions,
                (transportFlags, transport) => transportFlags | Translate(transport));
        }

        public static AuthenticatorTransport Translate(Fido2NetLib.Objects.AuthenticatorTransport? transport)
        {
            switch (transport)
            {
                case Fido2NetLib.Objects.AuthenticatorTransport.Ble:
                    return AuthenticatorTransport.BLE;
                case Fido2NetLib.Objects.AuthenticatorTransport.Internal:
                    return AuthenticatorTransport.Internal;
                case Fido2NetLib.Objects.AuthenticatorTransport.Nfc:
                    return AuthenticatorTransport.NFC;
                case Fido2NetLib.Objects.AuthenticatorTransport.Usb:
                    return AuthenticatorTransport.USB;
                case null:
                    return AuthenticatorTransport.NoRestrictions;
                case Fido2NetLib.Objects.AuthenticatorTransport.Lightning:
                default:
                    throw new NotSupportedException("The Lightning transport is not supported on Windows.");
            }
        }

        public static string Translate(Fido2NetLib.Objects.PublicKeyCredentialType? credentialType)
        {
            switch (credentialType)
            {
                case PublicKeyCredentialType.PublicKey:
                    return ApiConstants.CredentialTypePublicKey;
                default:
                    return null;
            }
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
    }
}
