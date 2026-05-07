using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class ConfirmationDialogParameters
{
    private const string MessageKey = "Message";
    private const string TitleKey = "Title";

    public required string Message { get; init; }
    public string? Title { get; init; }

    public static ConfirmationDialogParameters From(IDialogParameters parameters) => new()
    {
        Message = parameters.GetValue<string>(MessageKey) ?? string.Empty,
        Title = parameters.GetValue<string>(TitleKey)
    };

    public IDialogParameters ToDialogParameters()
    {
        var p = new DialogParameters { { MessageKey, Message } };
        if (Title != null) p.Add(TitleKey, Title);
        return p;
    }
}
