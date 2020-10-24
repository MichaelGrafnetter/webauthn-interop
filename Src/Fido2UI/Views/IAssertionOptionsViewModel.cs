using Fido2NetLib;
using Fido2NetLib.Objects;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public interface IAssertionOptionsViewModel
    {
        AssertionOptions Options { get; set; }

        AuthenticatorAttachment? SelectedAuthenticatorAttachment { get; set; }
    }
}
