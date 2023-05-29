using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    public interface IAlgorithmSelectorViewModel
    {
        List<Algorithm> SelectedAlgorithms { get; set; }
    }
}
