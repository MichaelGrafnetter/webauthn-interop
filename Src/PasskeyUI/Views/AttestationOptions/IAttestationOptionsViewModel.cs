using System.Collections.Generic;
using System.Windows.Input;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    public interface IAttestationOptionsViewModel
    {
        RelyingPartyInformation RelyingPartyEntity { get; set; }

        UserInformation UserEntity { get; set; }

        byte[] Challenge { get; set; }

        ResidentKeyRequirement ResidentKey { get; set; }

        EnterpriseAttestationType EnterpriseAttestation { get; set; }

        AuthenticationExtensionsClientInputs? ClientExtensions { get; set; }

        AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        UserVerificationRequirement UserVerificationRequirement { get; set; }

        List<Algorithm> PublicKeyCredentialParameters { get; set; }

        AttestationConveyancePreference AttestationConveyancePreference { get; set; }

        uint Timeout { get; set; }

        bool EnablePseudoRandomFunction { get; set; }

        LargeBlobSupport LargeBlobSupport { get; set; }

        bool IsBrowserPrivateMode { get; set; }

        PublicKeyCredentialHint CredentialHint { get; set; }

        bool ThirdPartyPayment { get; set; }

        string? RemoteWebOrigin { get; set; }

        ICommand ResetOptionsCommand { get; }

        ICommand GenerateChallengeCommand { get; }

        ICommand GenerateUserIdCommand { get; }

        bool IsFormValid { get; }
    }
}
