using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class SigningDialogResult
{
    private const string ResponseKey = "Response";

    public required string Response { get; init; }

    public static SigningDialogResult From(IDialogParameters parameters) => new()
    {
        Response = parameters.GetValue<string>(ResponseKey)
    };

    public IDialogParameters ToDialogParameters() =>
        new DialogParameters { { ResponseKey, Response } };
}
