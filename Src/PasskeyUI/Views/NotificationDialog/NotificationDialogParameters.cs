using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class NotificationDialogParameters
{
    private const string MessageKey = "Message";

    public required string Message { get; init; }

    public static NotificationDialogParameters From(IDialogParameters parameters) => new()
    {
        Message = parameters.GetValue<string>(MessageKey) ?? string.Empty
    };

    public IDialogParameters ToDialogParameters() =>
        new DialogParameters { { MessageKey, Message } };
}
