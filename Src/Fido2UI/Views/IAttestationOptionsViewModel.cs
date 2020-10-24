using Fido2NetLib;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public interface IAttestationOptionsViewModel
    {
        CredentialCreateOptions Options { get; set; }
    }
}
