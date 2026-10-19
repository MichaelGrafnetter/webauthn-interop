using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Shows owned WPF PIN and touch windows on the application UI thread.</summary>
internal sealed class SecurityKeyInteractionService : ISecurityKeyInteractionService
{
    /// <inheritdoc />
    public SecureString? RequestPin(string title, uint? remainingAttempts, bool incorrectPin, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        var dialog = new PinDialog(remainingAttempts, incorrectPin) { Title = title, Owner = Application.Current.MainWindow };
        return ShowPinDialog(dialog, dialog.TakePin, dialog.ClearPin, token);
    }

    /// <inheritdoc />
    public SecureString? RequestNewPin(CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        var dialog = new SetPinDialog { Owner = Application.Current.MainWindow };
        return ShowPinDialog(dialog, dialog.TakePin, dialog.ClearPins, token);
    }

    /// <inheritdoc />
    public PinChangeInput? RequestPinChange(uint? remainingAttempts, bool incorrectPin, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        var dialog = new ChangePinDialog(remainingAttempts, incorrectPin) { Owner = Application.Current.MainWindow };
        return ShowPinDialog(dialog, dialog.TakePins, dialog.ClearPins, token);
    }

    /// <inheritdoc />
    public PinPolicyChangeInput? RequestPinPolicyChange(uint? minimumPinLength, bool? forcePinChange, uint? maximumRpIds,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        var dialog = new ChangePinPolicyDialog(minimumPinLength, forcePinChange, maximumRpIds)
        {
            Owner = Application.Current.MainWindow
        };
        using var registration = token.Register(() => dialog.Dispatcher.BeginInvoke(new Action(dialog.Close)));
        bool? accepted = dialog.ShowDialog();
        token.ThrowIfCancellationRequested();
        return accepted == true ? dialog.TakePolicy() : null;
    }

    /// <summary>Shows a PIN window and clears its controls on every exit path.</summary>
    /// <typeparam name="T">Caller-owned secure input type.</typeparam>
    /// <param name="dialog">Owned PIN window.</param>
    /// <param name="takeInput">Copies accepted secure input.</param>
    /// <param name="clear">Clears all password controls.</param>
    /// <param name="token">Closes the window on cancellation.</param>
    /// <returns>Accepted secure input, or null when dismissed.</returns>
    private static T? ShowPinDialog<T>(Window dialog, Func<T> takeInput, Action clear, CancellationToken token) where T : class
    {
        using var registration = token.Register(() => dialog.Dispatcher.BeginInvoke(new Action(dialog.Close)));
        try
        {
            bool? accepted = dialog.ShowDialog();
            token.ThrowIfCancellationRequested();
            return accepted == true ? takeInput() : null;
        }
        finally
        {
            clear();
        }
    }

    /// <inheritdoc />
    public async Task<T> WaitAsync<T>(string prompt, Func<Task<T>> operation, Action cancel, CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        var dialog = new TouchDialog(prompt, cancel) { Owner = Application.Current.MainWindow };
        using var registration = token.Register(() => dialog.Dispatcher.BeginInvoke(new Action(dialog.ShowCancellation)));
        dialog.Show();
        try
        {
            return await operation();
        }
        finally
        {
            dialog.Complete();
        }
    }
}
