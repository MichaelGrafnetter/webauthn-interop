using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Displays device-interaction progress until native work has finished, even after cancellation.</summary>
public partial class TouchDialog : Window
{
    /// <summary>Signals the pending operation's cancellation source.</summary>
    private readonly Action _cancel;

    /// <summary>Allows programmatic close only after operation completion.</summary>
    private bool _completed;

    /// <summary>Initializes instructions and a cancellation callback.</summary>
    /// <param name="prompt">Touch or device-selection instructions.</param>
    /// <param name="cancel">Requests cancellation of pending work.</param>
    public TouchDialog(string prompt, Action cancel)
    {
        InitializeComponent();
        PromptText.Text = prompt;
        _cancel = cancel;
        Closing += OnClosing;
        PreviewKeyDown += OnKeyDown;
    }

    /// <summary>Reports that cancellation is pending without closing or releasing native resources.</summary>
    public void ShowCancellation()
    {
        CancelButton.IsEnabled = false;
        PromptText.Text = "Canceling. Waiting for the security key operation to finish. A write may already have applied.";
    }

    /// <summary>Closes the prompt after the operation and any session cleanup have finished.</summary>
    public void Complete()
    {
        _completed = true;
        Close();
    }

    /// <summary>Signals cancellation once and keeps showing progress until completion.</summary>
    private void Cancel()
    {
        if (CancelButton.IsEnabled)
        {
            ShowCancellation();
            _cancel();
        }
    }

    /// <summary>Treats the close button as a cancellation request, not operation completion.</summary>
    /// <param name="sender">Dialog window.</param>
    /// <param name="e">Cancelable closing request.</param>
    private void OnClosing(object? sender, CancelEventArgs e)
    {
        if (!_completed)
        {
            e.Cancel = true;
            Cancel();
        }
    }

    /// <summary>Requests cancellation from the cancel button.</summary>
    /// <param name="sender">Cancel button.</param>
    /// <param name="e">Click event.</param>
    private void OnCancel(object sender, RoutedEventArgs e) => Cancel();

    /// <summary>Requests cancellation when Escape is pressed.</summary>
    /// <param name="sender">Dialog window.</param>
    /// <param name="e">Keyboard event.</param>
    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Escape)
        {
            Cancel();
            e.Handled = true;
        }
    }
}
