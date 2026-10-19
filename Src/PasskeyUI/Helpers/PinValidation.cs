using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Compares PINs without creating managed plaintext strings.</summary>
internal static class PinValidation
{
    /// <summary>Validates required and matching PINs using WPF field-validation feedback.</summary>
    /// <param name="pinBox">New PIN control.</param>
    /// <param name="confirmationBox">Confirmation control.</param>
    /// <returns>True when the new PIN is provided and confirmed.</returns>
    internal static bool ValidateNewPin(PasswordBox pinBox, PasswordBox confirmationBox)
    {
        using SecureString pin = pinBox.SecurePassword;
        using SecureString confirmation = confirmationBox.SecurePassword;
        bool provided = SetError(pinBox, pin.Length == 0 ? "Enter a new PIN." : null);
        bool matched = SetError(confirmationBox, confirmation.Length == 0
            ? "Confirm the new PIN."
            : Matches(pin, confirmation) ? null : "The PINs do not match.");
        return provided && matched;
    }

    /// <summary>Sets or clears a standard WPF validation error without binding a plaintext password.</summary>
    /// <param name="control">Control with a self-bound Tag used as a validation anchor.</param>
    /// <param name="message">Validation message, or null when valid.</param>
    /// <returns>True when no validation error is present.</returns>
    internal static bool SetError(FrameworkElement control, string? message)
    {
        var binding = control.GetBindingExpression(FrameworkElement.TagProperty);
        if (message == null)
        {
            Validation.ClearInvalid(binding);
            return true;
        }
        Validation.MarkInvalid(binding, new ValidationError(new ExceptionValidationRule(), binding, message, null));
        return false;
    }

    /// <summary>Checks whether two nonempty secure PINs match.</summary>
    /// <param name="pin">Proposed PIN.</param>
    /// <param name="confirmation">Repeated PIN.</param>
    /// <returns>True when both PINs are nonempty and equal.</returns>
    internal static bool Matches(SecureString pin, SecureString confirmation)
    {
        if (pin.Length == 0 || pin.Length != confirmation.Length)
        {
            return false;
        }
        nint first = 0;
        nint second = 0;
        try
        {
            first = Marshal.SecureStringToGlobalAllocUnicode(pin);
            second = Marshal.SecureStringToGlobalAllocUnicode(confirmation);
            int difference = 0;
            for (int index = 0; index < pin.Length; index++)
            {
                difference |= Marshal.ReadInt16(first, index * sizeof(char)) ^ Marshal.ReadInt16(second, index * sizeof(char));
            }
            return difference == 0;
        }
        finally
        {
            if (first != 0)
            {
                Marshal.ZeroFreeGlobalAllocUnicode(first);
            }
            if (second != 0)
            {
                Marshal.ZeroFreeGlobalAllocUnicode(second);
            }
        }
    }
}
