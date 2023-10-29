namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public interface IAssertionOptionsViewModel
    {
        string RelyingPartyId { get; set; }

        byte[] Challenge { get; set; }

        UserVerificationRequirement UserVerificationRequirement { get; set; }

        AuthenticatorAttachment AuthenticatorAttachment { get; set; }

        AuthenticationExtensionsClientInputs ClientExtensions { get; set; }

        CredentialLargeBlobOperation LargeBlobOperation { get; set; }

        byte[] LargeBlob { get; set; }

        int Timeout { get; set; }

        bool IsBrowserPrivateMode { get; set; }
    }
}
