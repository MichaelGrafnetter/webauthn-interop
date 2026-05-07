using DSInternals.Win32.WebAuthn.COSE;
using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class KeyGenerationDialogParameters
{
    private const string DefaultAlgorithmKey = "DefaultAlgorithm";

    public Algorithm? DefaultAlgorithm { get; init; }

    public static KeyGenerationDialogParameters From(IDialogParameters parameters) => new()
    {
        DefaultAlgorithm = parameters.ContainsKey(DefaultAlgorithmKey)
            ? parameters.GetValue<Algorithm>(DefaultAlgorithmKey)
            : null
    };

    public IDialogParameters ToDialogParameters()
    {
        var p = new DialogParameters();
        if (DefaultAlgorithm.HasValue)
            p.Add(DefaultAlgorithmKey, DefaultAlgorithm.Value);
        return p;
    }
}
