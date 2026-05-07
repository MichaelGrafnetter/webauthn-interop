using System;
using DSInternals.Win32.WebAuthn.COSE;
using Prism.Dialogs;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal static class DialogServiceExtensions
{
    public static void ShowNotificationDialog(this IDialogService service, string message)
    {
        service.ShowDialog(nameof(NotificationDialog),
            new NotificationDialogParameters { Message = message }.ToDialogParameters());
    }

    public static void ShowConfirmationDialog(this IDialogService service,
        ConfirmationDialogParameters parameters, Action<bool> callback)
    {
        service.ShowDialog(nameof(ConfirmationDialog), parameters.ToDialogParameters(),
            result => callback(result.Result == ButtonResult.OK));
    }

    public static void ShowKeyGenerationDialog(this IDialogService service,
        Algorithm? defaultAlgorithm, Action<KeyGenerationDialogResult?> callback)
    {
        service.ShowDialog(nameof(KeyGenerationDialog),
            new KeyGenerationDialogParameters { DefaultAlgorithm = defaultAlgorithm }.ToDialogParameters(),
            result =>
            {
                if (result.Result == ButtonResult.OK)
                    callback(KeyGenerationDialogResult.From(result.Parameters));
                else
                    callback(null);
            });
    }

    public static void ShowAttestationSigningDialog(this IDialogService service,
        AttestationSigningDialogParameters parameters, Action<string?> callback)
    {
        service.ShowDialog(nameof(AttestationSigningDialog), parameters.ToDialogParameters(),
            result =>
            {
                if (result.Result == ButtonResult.OK)
                    callback(SigningDialogResult.From(result.Parameters).Response);
                else
                    callback(null);
            });
    }

    public static void ShowAssertionSigningDialog(this IDialogService service,
        AssertionSigningDialogParameters parameters, Action<string?> callback)
    {
        service.ShowDialog(nameof(AssertionSigningDialog), parameters.ToDialogParameters(),
            result =>
            {
                if (result.Result == ButtonResult.OK)
                    callback(SigningDialogResult.From(result.Parameters).Response);
                else
                    callback(null);
            });
    }
}
