using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class NotificationDialogParameters
{
    /// <summary>Title displayed in the notification window.</summary>
    public string Title { get; init; } = "Error";

    public required string Message { get; init; }

    public static NotificationDialogParameters From(IDialogParameters parameters) => new()
    {
        Message = parameters.GetValue<string>(nameof(Message)) ?? string.Empty,
        Title = parameters.GetValue<string>(nameof(Title)) ?? "Error"
    };

    public IDialogParameters ToDialogParameters() =>
        new DialogParameters { { nameof(Message), Message }, { nameof(Title), Title } };
}
