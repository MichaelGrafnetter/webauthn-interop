using System.Security;
using System.Windows;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Collects a nonempty PIN exclusively through a password control.</summary>
public partial class PinDialog : Window
{
    /// <summary>Gets the remaining PIN attempts, or null when unreported.</summary>
    public uint? RemainingAttempts { get; }

    /// <summary>Gets whether the previous PIN was rejected.</summary>
    public bool IncorrectPin { get; }

    /// <summary>Initializes PIN feedback and keyboard focus.</summary>
    /// <param name="remainingAttempts">Remaining PIN attempts, or null when unreported.</param>
    /// <param name="incorrectPin">Whether the previous PIN was rejected.</param>
    public PinDialog(uint? remainingAttempts, bool incorrectPin)
    {
        RemainingAttempts = remainingAttempts;
        IncorrectPin = incorrectPin;
        InitializeComponent();
        Loaded += (_, _) => PinBox.Focus();
    }

    /// <summary>Copies the accepted PIN into a caller-owned secure buffer.</summary>
    /// <returns>A secure PIN that the caller must dispose.</returns>
    public SecureString TakePin() => PinBox.SecurePassword;

    /// <summary>Clears the control when the prompt completes, including cancellation.</summary>
    public void ClearPin() => PinBox.Clear();

    /// <summary>Prevents empty PIN submission without imposing device-specific policy.</summary>
    /// <param name="sender">Password control.</param>
    /// <param name="e">Password change event.</param>
    private void OnPasswordChanged(object sender, RoutedEventArgs e)
    {
        using SecureString pin = PinBox.SecurePassword;
        AcceptButton.IsEnabled = pin.Length > 0;
    }

    /// <summary>Accepts the PIN without converting it to plain text.</summary>
    /// <param name="sender">OK button.</param>
    /// <param name="e">Click event.</param>
    private void OnAccept(object sender, RoutedEventArgs e) => DialogResult = true;
}
