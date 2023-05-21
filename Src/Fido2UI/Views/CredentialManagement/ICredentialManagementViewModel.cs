using DSInternals.Win32.WebAuthn.FIDO;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public interface ICredentialManagementViewModel
    {
        string RelyingPartyId { get; set; }

        bool IsBrowserPrivateMode { get; set; }
    }
}
