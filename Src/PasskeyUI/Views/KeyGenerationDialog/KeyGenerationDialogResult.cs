using DSInternals.Win32.WebAuthn.COSE;
using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class KeyGenerationDialogResult
{
    private const string FilePathKey = "FilePath";
    private const string AlgorithmKey = "Algorithm";

    public required string FilePath { get; init; }
    public required Algorithm Algorithm { get; init; }

    public static KeyGenerationDialogResult From(IDialogParameters parameters) => new()
    {
        FilePath = parameters.GetValue<string>(FilePathKey),
        Algorithm = parameters.GetValue<Algorithm>(AlgorithmKey)
    };

    public IDialogParameters ToDialogParameters() => new DialogParameters
    {
        { FilePathKey, FilePath },
        { AlgorithmKey, Algorithm }
    };
}
