using System;
using System.Collections.Generic;
using System.Linq;
using Fido2NetLib;
using Fido2NetLib.Objects;

namespace WebAuthN.Interop
{
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
                    // TODO: AttestationConveyancePreference.Enterprise
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
                // TODO: Icon = user.Icon
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
            // TODO Check credParam null
            // TODO: Process credParam.PublicKeyCredentialType
            return new CoseCredentialParameter(Translate(credParam.Alg));
        }

        public static string Translate(PublicKeyCredentialType? credentialType)
        {
            switch (credentialType)
            {
                case PublicKeyCredentialType.PublicKey:
                    // TODO: Use a constant instead
                    return "public-key";
                case null:
                    return null;
                default:
                    throw new NotSupportedException();
            }
        }

        public static ExtensionsIn Translate(AuthenticationExtensionsClientInputs extensions)
        {
            if(extensions == null)
            {
                return null;
            }

            throw new NotImplementedException();
        }

        public static CredentialExListIn Translate(IEnumerable<PublicKeyCredentialDescriptor> credentials)
        {
            // TODO: This is sometimes IEnumerable and Sometimes List in FIDO2 API

            if (credentials == null)
            {
                // Null pointer is allowed in the parent structure.
                return null;
            }

            var translatedCredentials = credentials.Select(item => Translate(item)).ToArray();
            return new CredentialExListIn(translatedCredentials);
        }

        public static CredentialExIn Translate(PublicKeyCredentialDescriptor credential)
        {
            // TODO: Test null

            return new CredentialExIn()
            {
                Id = credential.Id,
                // TODO: Should the type be nullable?
                Type = Translate(credential.Type),
                Transports = Translate(credential.Transports)
            };
        }

        public static AuthenticatorTransport Translate(Fido2NetLib.Objects.AuthenticatorTransport[] transports)
        {
            if(transports == null)
            {
                return AuthenticatorTransport.NoRestrictions;
            }

            return transports.Aggregate(
                AuthenticatorTransport.NoRestrictions,
                (transportFlags, transport) => transportFlags | Translate(transport));
        }

        public static AuthenticatorTransport Translate(Fido2NetLib.Objects.AuthenticatorTransport? transport)
        {
            switch(transport)
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
            switch(authenticatorAttachment)
            {
                case Fido2NetLib.Objects.AuthenticatorAttachment.CrossPlatform:
                    return AuthenticatorAttachment.CrossPlatform;
                case Fido2NetLib.Objects.AuthenticatorAttachment.Platform:
                    return AuthenticatorAttachment.Platform;
                case null:
                    return AuthenticatorAttachment.Any;
                default:
                    // TODO: AuthenticatorAttachment.CrossPlatformU2F
                    throw new NotSupportedException();
            }
        }

        public static UserVerificationRequirement Translate(Fido2NetLib.Objects.UserVerificationRequirement? userVerification)
        {
            switch(userVerification)
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
    }
}
