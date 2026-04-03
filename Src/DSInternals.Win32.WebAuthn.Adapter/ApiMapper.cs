using System;
using System.Collections.Generic;
using System.Linq;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Interop;
using Fido2NetLib.Objects;

namespace DSInternals.Win32.WebAuthn.Adapter
{
    /// <summary>
    /// Performs mapping between Fido2NetLib objects and low-level data structures.
    /// </summary>
    public static class ApiMapper
    {
        /// <summary>
        /// Maps relying party metadata to the interop model.
        /// </summary>
        public static RelyingPartyInformation Translate(Fido2NetLib.PublicKeyCredentialRpEntity relyingParty)
        {
            ArgumentNullException.ThrowIfNull(relyingParty);

            return new RelyingPartyInformation()
            {
                Id = relyingParty.Id,
                Name = relyingParty.Name,
                Icon = relyingParty.Icon ?? string.Empty
            };
        }

        /// <summary>
        /// Maps user information to the interop model.
        /// </summary>
        public static UserInformation Translate(Fido2NetLib.Fido2User user)
        {
            ArgumentNullException.ThrowIfNull(user);

            return new UserInformation()
            {
                Id = user.Id,
                Name = user.Name,
                DisplayName = user.DisplayName,
                Icon = String.Empty // Browser implementations typically use an empty string instead of null
            };
        }

        /// <summary>
        /// Maps credential parameter list to supported COSE algorithms.
        /// </summary>
        public static Algorithm[] Translate(IReadOnlyList<Fido2NetLib.PubKeyCredParam> credParams)
        {
            ArgumentNullException.ThrowIfNull(credParams);

            return credParams.Select(item => Translate(item.Alg)).ToArray();
        }

        /// <summary>
        /// Maps numeric COSE algorithm identifier to the interop enum.
        /// </summary>
        public static Algorithm Translate(long algorithm)
        {
            return checked((Algorithm)algorithm);
        }

        /// <summary>
        /// Maps Fido2NetLib COSE algorithm to the interop enum.
        /// </summary>
        public static Algorithm Translate(Fido2NetLib.Objects.COSE.Algorithm algorithm)
        {
            return checked((Algorithm)algorithm);
        }

        /// <summary>
        /// Maps allow/exclude credential descriptors to interop descriptors.
        /// </summary>
        public static IList<PublicKeyCredentialDescriptor> Translate(IEnumerable<Fido2NetLib.Objects.PublicKeyCredentialDescriptor> credentials)
        {
            var result = new List<PublicKeyCredentialDescriptor>();

            if (credentials != null)
            {
                result.AddRange(credentials.Select(item => Translate(item)));
            }

            return result;
        }

        /// <summary>
        /// Maps a single credential descriptor to the interop descriptor model.
        /// </summary>
        public static PublicKeyCredentialDescriptor Translate(Fido2NetLib.Objects.PublicKeyCredentialDescriptor credential)
        {
            ArgumentNullException.ThrowIfNull(credential);

            return new PublicKeyCredentialDescriptor(credential.Id, Translate(credential.Transports ?? Array.Empty<Fido2NetLib.Objects.AuthenticatorTransport>()), Translate(credential.Type));
        }

        /// <summary>
        /// Maps transport hints to interop transport flags.
        /// </summary>
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

        /// <summary>
        /// Maps a single transport hint to interop transport flags.
        /// </summary>
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
                default:
                    throw new NotSupportedException("This transport type is not currently supported.");
            }
        }

        /// <summary>
        /// Maps credential type to the WebAuthn wire type string.
        /// </summary>
        public static string Translate(Fido2NetLib.Objects.PublicKeyCredentialType? credentialType)
        {
            switch (credentialType)
            {
                case PublicKeyCredentialType.PublicKey:
                case null:
                    return ApiConstants.PublicKeyCredentialType;
                default:
                    throw new NotSupportedException("This credential type is not currently supported.");
            }
        }

        /// <summary>
        /// Maps authenticator attachment preference to interop attachment enum.
        /// </summary>
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

        /// <summary>
        /// Maps user verification requirement to interop requirement enum.
        /// </summary>
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

        /// <summary>
        /// Maps attestation conveyance preference to interop preference enum.
        /// </summary>
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

        /// <summary>
        /// Maps Level 1 resident key boolean to interop resident key requirement.
        /// </summary>
        public static ResidentKeyRequirement TranslateResidentKey(bool? requireResidentKey)
        {
            // Map from WebAuthn Level 1 boolean to ResidentKeyRequirement
            if (requireResidentKey == true)
            {
                return ResidentKeyRequirement.Required;
            }

            return ResidentKeyRequirement.Discouraged;
        }

        /// <summary>
        /// Maps resident key requirement to interop resident key requirement.
        /// </summary>
        public static ResidentKeyRequirement TranslateResidentKey(Fido2NetLib.Objects.ResidentKeyRequirement? residentKey)
        {
            return residentKey switch
            {
                Fido2NetLib.Objects.ResidentKeyRequirement.Required => ResidentKeyRequirement.Required,
                Fido2NetLib.Objects.ResidentKeyRequirement.Preferred => ResidentKeyRequirement.Preferred,
                Fido2NetLib.Objects.ResidentKeyRequirement.Discouraged => ResidentKeyRequirement.Discouraged,
                null => ResidentKeyRequirement.Discouraged,
                _ => throw new NotSupportedException(),
            };
        }
    }
}
