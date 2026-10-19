using System.Security;
using System.Windows;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Collects the current PIN and a confirmed replacement PIN.</summary>
public partial class ChangePinDialog : Window
{
    /// <summary>Gets the remaining current-PIN attempts, or null when unreported.</summary>
    public uint? RemainingAttempts { get; }

    /// <summary>Gets whether the previous current PIN was rejected.</summary>
    public bool IncorrectPin { get; }

    /// <summary>Initializes current-PIN feedback and keyboard focus.</summary>
    /// <param name="remainingAttempts">Remaining PIN attempts, or null when unreported.</param>
    /// <param name="incorrectPin">Whether the previous current PIN was rejected.</param>
    public ChangePinDialog(uint? remainingAttempts, bool incorrectPin)
    {
        RemainingAttempts = remainingAttempts;
        IncorrectPin = incorrectPin;
        InitializeComponent();
        Loaded += (_, _) =>
        {
            AcceptButton.IsEnabled = ValidatePins();
            CurrentPinBox.Focus();
        };
    }

    /// <summary>Copies both submitted PINs into caller-owned secure buffers.</summary>
    /// <returns>PIN input that the caller must dispose.</returns>
    public PinChangeInput TakePins() => new(CurrentPinBox.SecurePassword, NewPinBox.SecurePassword);

    /// <summary>Clears all PIN controls after acceptance or cancellation.</summary>
    public void ClearPins()
    {
        CurrentPinBox.Clear();
        NewPinBox.Clear();
        ConfirmPinBox.Clear();
    }

    /// <summary>Checks that the current PIN is nonempty and the new PIN is confirmed.</summary>
    /// <returns>True when both PINs can be submitted.</returns>
    private bool ValidatePins()
    {
        using SecureString current = CurrentPinBox.SecurePassword;
        bool provided = PinValidation.SetError(CurrentPinBox, current.Length == 0 ? "Enter the current PIN." : null);
        bool matched = PinValidation.ValidateNewPin(NewPinBox, ConfirmPinBox);
        return provided && matched;
    }

    /// <summary>Enables submission only when all PIN controls are valid.</summary>
    /// <param name="sender">Changed password control.</param>
    /// <param name="e">Password change event.</param>
    private void OnPasswordChanged(object sender, RoutedEventArgs e) => AcceptButton.IsEnabled = ValidatePins();

    /// <summary>Accepts both PINs without exposing plaintext.</summary>
    /// <param name="sender">OK button.</param>
    /// <param name="e">Click event.</param>
    private void OnAccept(object sender, RoutedEventArgs e)
    {
        if (ValidatePins())
        {
            DialogResult = true;
        }
    }
}
