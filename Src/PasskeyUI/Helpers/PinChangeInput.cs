using System;
using System.Security;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Owns the secure current and replacement PINs for a single change attempt.</summary>
public sealed class PinChangeInput : IDisposable
{
    /// <summary>Gets the current security-key PIN.</summary>
    public SecureString CurrentPin { get; }

    /// <summary>Gets the confirmed replacement PIN.</summary>
    public SecureString NewPin { get; }

    /// <summary>Takes ownership of both secure PIN buffers.</summary>
    /// <param name="currentPin">Current PIN to dispose with this input.</param>
    /// <param name="newPin">Replacement PIN to dispose with this input.</param>
    public PinChangeInput(SecureString currentPin, SecureString newPin)
    {
        ArgumentNullException.ThrowIfNull(currentPin);
        ArgumentNullException.ThrowIfNull(newPin);
        CurrentPin = currentPin;
        NewPin = newPin;
    }

    /// <summary>Erases both secure PIN buffers.</summary>
    public void Dispose()
    {
        CurrentPin.Dispose();
        NewPin.Dispose();
    }
}
