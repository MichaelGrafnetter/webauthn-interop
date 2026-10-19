using System.Security;
using System.Windows;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Collects and confirms an initial security-key PIN.</summary>
public partial class SetPinDialog : Window
{
    /// <summary>Initializes the PIN controls and keyboard focus.</summary>
    public SetPinDialog()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            AcceptButton.IsEnabled = ValidatePins();
            NewPinBox.Focus();
        };
    }

    /// <summary>Copies the confirmed PIN into a caller-owned secure buffer.</summary>
    /// <returns>A secure PIN that the caller must dispose.</returns>
    public SecureString TakePin() => NewPinBox.SecurePassword;

    /// <summary>Clears both PIN controls after acceptance or cancellation.</summary>
    public void ClearPins()
    {
        NewPinBox.Clear();
        ConfirmPinBox.Clear();
    }

    /// <summary>Checks that the proposed PIN and confirmation are nonempty and identical.</summary>
    /// <returns>True when the PIN can be submitted.</returns>
    private bool ValidatePins() => PinValidation.ValidateNewPin(NewPinBox, ConfirmPinBox);

    /// <summary>Enables submission only for matching nonempty PINs.</summary>
    /// <param name="sender">Changed password control.</param>
    /// <param name="e">Password change event.</param>
    private void OnPasswordChanged(object sender, RoutedEventArgs e) => AcceptButton.IsEnabled = ValidatePins();

    /// <summary>Accepts the confirmed PIN without exposing plaintext.</summary>
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
