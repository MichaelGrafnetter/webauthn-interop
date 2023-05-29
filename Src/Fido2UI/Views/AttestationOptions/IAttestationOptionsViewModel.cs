using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public interface IAttestationOptionsViewModel
    {
        RelyingPartyInformation RelyingPartyEntity { get; set; }

        UserInformation UserEntity { get; set; }

        byte[] Challenge { get; set; }

        bool RequireResidentKey { get; set; }

        AuthenticationExtensionsClientInputs ClientExtensions { get; set; }

        AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        UserVerificationRequirement UserVerificationRequirement { get; set; }

        List<Algorithm> PublicKeyCredentialParameters { get; set; }

        AttestationConveyancePreference AttestationConveyancePreference { get; set; }

        int Timeout { get; set; }
    }
}
