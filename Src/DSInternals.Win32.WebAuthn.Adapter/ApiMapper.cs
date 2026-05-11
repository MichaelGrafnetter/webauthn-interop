using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.FIDO;
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

        /// <summary>
        /// Maps a list of credential hints to their wire-format strings.
        /// </summary>
        public static string[]? Translate(IReadOnlyList<Fido2NetLib.Objects.PublicKeyCredentialHint>? hints)
        {
            if (hints == null || hints.Count == 0)
            {
                return null;
            }

            return hints.Select(Translate).ToArray();
        }

        /// <summary>
        /// Maps a single credential hint to its wire-format string.
        /// </summary>
        public static string Translate(Fido2NetLib.Objects.PublicKeyCredentialHint hint)
        {
            return hint switch
            {
                Fido2NetLib.Objects.PublicKeyCredentialHint.SecurityKey => ApiConstants.CredentialHintSecurityKey,
                Fido2NetLib.Objects.PublicKeyCredentialHint.ClientDevice => ApiConstants.CredentialHintClientDevice,
                Fido2NetLib.Objects.PublicKeyCredentialHint.Hybrid => ApiConstants.CredentialHintHybrid,
                _ => throw new NotSupportedException("This credential hint is not currently supported."),
            };
        }

        /// <summary>
        /// Maps a Fido2NetLib large blob support preference to the interop enum.
        /// </summary>
        public static LargeBlobSupport Translate(Fido2NetLib.Objects.LargeBlobSupport? support)
        {
            return support switch
            {
                Fido2NetLib.Objects.LargeBlobSupport.Required => LargeBlobSupport.Required,
                Fido2NetLib.Objects.LargeBlobSupport.Preferred => LargeBlobSupport.Preferred,
                null => LargeBlobSupport.None,
                _ => throw new NotSupportedException(),
            };
        }

        /// <summary>
        /// Maps a Fido2NetLib credential protection policy to the interop user verification value.
        /// </summary>
        public static UserVerification TranslateCredProtect(Fido2NetLib.Objects.CredentialProtectionPolicy? policy)
        {
            return policy switch
            {
                Fido2NetLib.Objects.CredentialProtectionPolicy.UserVerificationOptional => UserVerification.Optional,
                Fido2NetLib.Objects.CredentialProtectionPolicy.UserVerificationOptionalWithCredentialIdList => UserVerification.OptionalWithCredentialIDList,
                Fido2NetLib.Objects.CredentialProtectionPolicy.UserVerificationRequired => UserVerification.Required,
                null => UserVerification.Any,
                _ => throw new NotSupportedException(),
            };
        }

        /// <summary>
        /// Maps an interop user verification value back to a Fido2NetLib credential protection policy.
        /// </summary>
        public static Fido2NetLib.Objects.CredentialProtectionPolicy? TranslateCredProtect(UserVerification verification)
        {
            return verification switch
            {
                UserVerification.Optional => Fido2NetLib.Objects.CredentialProtectionPolicy.UserVerificationOptional,
                UserVerification.OptionalWithCredentialIDList => Fido2NetLib.Objects.CredentialProtectionPolicy.UserVerificationOptionalWithCredentialIdList,
                UserVerification.Required => Fido2NetLib.Objects.CredentialProtectionPolicy.UserVerificationRequired,
                _ => null,
            };
        }

        /// <summary>
        /// Maps Fido2NetLib PRF values to the interop equivalent.
        /// </summary>
        public static PRFValues? Translate(AuthenticationExtensionsPRFValues? values)
        {
            if (values == null)
            {
                return null;
            }

            return new PRFValues
            {
                First = values.First,
                Second = values.Second
            };
        }

        /// <summary>
        /// Maps interop PRF values to the Fido2NetLib equivalent.
        /// </summary>
        public static AuthenticationExtensionsPRFValues? Translate(PRFValues? values)
        {
            if (values == null)
            {
                return null;
            }

            return new AuthenticationExtensionsPRFValues
            {
                First = values.First,
                Second = values.Second
            };
        }

        /// <summary>
        /// Maps Fido2NetLib PRF inputs to the interop attestation PRF inputs.
        /// </summary>
        public static PRFAttestationInputs? TranslateAttestation(AuthenticationExtensionsPRFInputs? inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            return new PRFAttestationInputs
            {
                Eval = Translate(inputs.Eval)
            };
        }

        /// <summary>
        /// Maps Fido2NetLib PRF inputs to the interop assertion PRF inputs.
        /// </summary>
        public static PRFAssertionInputs? TranslateAssertion(AuthenticationExtensionsPRFInputs? inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            var result = new PRFAssertionInputs
            {
                Eval = Translate(inputs.Eval)
            };

            if (inputs.EvalByCredential.HasValue)
            {
                var pair = inputs.EvalByCredential.Value;
                var evalValues = Translate(pair.Value);
                if (evalValues != null)
                {
                    result.EvalByCredential = new Dictionary<string, PRFValues>
                    {
                        [pair.Key] = evalValues
                    };
                }
            }

            return result;
        }

        /// <summary>
        /// Maps Fido2NetLib large-blob inputs to the interop attestation large-blob inputs.
        /// </summary>
        public static LargeBlobAttestationInputs? TranslateAttestation(AuthenticationExtensionsLargeBlobInputs? inputs)
        {
            if (inputs == null || inputs.Support == null)
            {
                return null;
            }

            return new LargeBlobAttestationInputs(Translate(inputs.Support));
        }

        /// <summary>
        /// Maps Fido2NetLib large-blob inputs to the interop assertion large-blob inputs.
        /// </summary>
        public static LargeBlobAssertionInputs? TranslateAssertion(AuthenticationExtensionsLargeBlobInputs? inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            if (!inputs.Read && inputs.Write == null)
            {
                return null;
            }

            return new LargeBlobAssertionInputs(inputs.Read, inputs.Write);
        }

        /// <summary>
        /// Maps Fido2NetLib attestation extension inputs to the interop equivalent.
        /// </summary>
        public static AuthenticationExtensionsClientAttestationInputs? TranslateAttestationExtensions(AuthenticationExtensionsClientInputs? inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            var result = new AuthenticationExtensionsClientAttestationInputs
            {
                CredProtect = TranslateCredProtect(inputs.CredentialProtectionPolicy),
                EnforceCredProtect = inputs.EnforceCredentialProtectionPolicy == true,
                CredentialProperties = inputs.CredProps == true,
                LargeBlob = TranslateAttestation(inputs.LargeBlob),
                Prf = TranslateAttestation(inputs.PRF)
            };

            return result;
        }

        /// <summary>
        /// Maps Fido2NetLib assertion extension inputs to the interop equivalent.
        /// </summary>
        public static AuthenticationExtensionsClientAssertionInputs? TranslateAssertionExtensions(AuthenticationExtensionsClientInputs? inputs)
        {
            if (inputs == null)
            {
                return null;
            }

            return new AuthenticationExtensionsClientAssertionInputs
            {
                AppID = inputs.AppID,
                LargeBlob = TranslateAssertion(inputs.LargeBlob),
                Prf = TranslateAssertion(inputs.PRF)
            };
        }

        /// <summary>
        /// Maps interop PRF attestation outputs to the Fido2NetLib equivalent.
        /// </summary>
        public static AuthenticationExtensionsPRFOutputs? Translate(PRFAttestationOutputs? outputs)
        {
            if (outputs == null)
            {
                return null;
            }

            return new AuthenticationExtensionsPRFOutputs
            {
                Enabled = outputs.Enabled,
                Results = Translate(outputs.Results)!
            };
        }

        /// <summary>
        /// Maps interop PRF assertion outputs to the Fido2NetLib equivalent.
        /// </summary>
        public static AuthenticationExtensionsPRFOutputs? Translate(PRFAssertionOutputs? outputs)
        {
            if (outputs == null)
            {
                return null;
            }

            return new AuthenticationExtensionsPRFOutputs
            {
                Enabled = true,
                Results = Translate(outputs.Results)!
            };
        }

        /// <summary>
        /// Maps interop large-blob attestation outputs to the Fido2NetLib equivalent.
        /// </summary>
        public static AuthenticationExtensionsLargeBlobOutputs? Translate(LargeBlobAttestationOutputs? outputs)
        {
            if (outputs == null)
            {
                return null;
            }

            return new AuthenticationExtensionsLargeBlobOutputs
            {
                Supported = outputs.Supported
            };
        }

        /// <summary>
        /// Maps interop large-blob assertion outputs to the Fido2NetLib equivalent.
        /// </summary>
        public static AuthenticationExtensionsLargeBlobOutputs? Translate(LargeBlobAssertionOutputs? outputs)
        {
            if (outputs == null)
            {
                return null;
            }

            return new AuthenticationExtensionsLargeBlobOutputs
            {
                Blob = outputs.Blob,
                Written = outputs.Written == true
            };
        }

        /// <summary>
        /// Maps interop attestation extension outputs to the Fido2NetLib equivalent.
        /// </summary>
        public static Fido2NetLib.Objects.AuthenticationExtensionsClientOutputs? Translate(AuthenticationExtensionsClientAttestationOutputs? outputs)
        {
            if (outputs == null)
            {
                return null;
            }

            var result = new Fido2NetLib.Objects.AuthenticationExtensionsClientOutputs
            {
                CredProtect = TranslateCredProtect(outputs.CredProtect),
                LargeBlob = Translate(outputs.LargeBlob),
                PRF = Translate(outputs.Prf)
            };

            if (outputs.CredentialProperties != null)
            {
                result.CredProps = new CredentialPropertiesOutput
                {
                    Rk = outputs.CredentialProperties.ResidentKey == true
                };
            }

            return result;
        }

        /// <summary>
        /// Maps interop assertion extension outputs to the Fido2NetLib equivalent.
        /// </summary>
        public static Fido2NetLib.Objects.AuthenticationExtensionsClientOutputs? Translate(AuthenticationExtensionsClientAssertionOutputs? outputs)
        {
            if (outputs == null)
            {
                return null;
            }

            return new Fido2NetLib.Objects.AuthenticationExtensionsClientOutputs
            {
                AppID = outputs.AppID == true,
                LargeBlob = Translate(outputs.LargeBlob),
                PRF = Translate(outputs.Prf)
            };
        }

        /// <summary>
        /// Encodes a credential identifier to its Base64Url string form.
        /// </summary>
        public static string EncodeCredentialId(byte[]? credentialId)
        {
            return credentialId is null or { Length: 0 } ? string.Empty : Base64Url.EncodeToString(credentialId);
        }
    }
}
