using System.Windows.Input;

namespace DSInternals.Win32.WebAuthn.PasskeyUI
{
    public interface IAssertionOptionsViewModel
    {
        string RelyingPartyId { get; set; }

        byte[] Challenge { get; set; }

        UserVerificationRequirement UserVerificationRequirement { get; set; }

        AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        AuthenticationExtensionsClientInputs? ClientExtensions { get; set; }

        CredentialLargeBlobOperation LargeBlobOperation { get; set; }

        byte[]? LargeBlob { get; set; }

        uint Timeout { get; set; }

        bool IsBrowserPrivateMode { get; set; }

        PublicKeyCredentialHint CredentialHint { get; set; }

        string? RemoteWebOrigin { get; set; }

        ICommand ResetOptionsCommand { get; }

        ICommand GenerateChallengeCommand { get; }

        ICommand GenerateHmacSecretSalt1Command { get; }

        ICommand GenerateHmacSecretSalt2Command { get; }

        bool IsFormValid { get; }
    }
}
