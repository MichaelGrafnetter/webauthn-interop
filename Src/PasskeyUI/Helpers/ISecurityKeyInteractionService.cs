using System;
using System.Security;
using System.Threading;
using System.Threading.Tasks;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Collects PINs and displays cancelable device-interaction prompts.</summary>
public interface ISecurityKeyInteractionService
{
    /// <summary>Requests a PIN without converting it to a managed string.</summary>
    /// <param name="title">Dialog title.</param>
    /// <param name="remainingAttempts">Remaining PIN attempts, or null when unreported or inapplicable.</param>
    /// <param name="incorrectPin">Whether the previous PIN was rejected.</param>
    /// <param name="token">Closes the prompt when cancellation is requested.</param>
    /// <returns>A caller-owned secure PIN, or null when dismissed.</returns>
    SecureString? RequestPin(string title, uint? remainingAttempts, bool incorrectPin, CancellationToken token);

    /// <summary>Requests and confirms an initial PIN without exposing plaintext.</summary>
    /// <param name="token">Closes the prompt when cancellation is requested.</param>
    /// <returns>A caller-owned secure PIN, or null when dismissed.</returns>
    SecureString? RequestNewPin(CancellationToken token);

    /// <summary>Requests the current PIN and a confirmed replacement PIN.</summary>
    /// <param name="remainingAttempts">Remaining current-PIN attempts, or null when unreported.</param>
    /// <param name="incorrectPin">Whether the previous current PIN was rejected.</param>
    /// <param name="token">Closes the prompt when cancellation is requested.</param>
    /// <returns>Caller-owned secure PIN input, or null when dismissed.</returns>
    PinChangeInput? RequestPinChange(uint? remainingAttempts, bool incorrectPin, CancellationToken token);

    /// <summary>Edits PIN policy using nullable defaults reported by the selected authenticator.</summary>
    /// <param name="minimumPinLength">Reported minimum PIN length, or null when unreported.</param>
    /// <param name="forcePinChange">Reported forced-change state, or null when unreported.</param>
    /// <param name="maximumRpIds">Reported additional relying-party ID limit, or null when unreported.</param>
    /// <param name="token">Closes the prompt when cancellation is requested.</param>
    /// <returns>The accepted policy request, or null when dismissed.</returns>
    PinPolicyChangeInput? RequestPinPolicyChange(uint? minimumPinLength, bool? forcePinChange, uint? maximumRpIds,
        CancellationToken token);

    /// <summary>Displays a touch prompt until an operation and its cleanup complete.</summary>
    /// <typeparam name="T">Operation result type.</typeparam>
    /// <param name="prompt">Device-interaction instructions.</param>
    /// <param name="operation">Native operation to await.</param>
    /// <param name="cancel">Requests cancellation without disposing in-use resources.</param>
    /// <param name="token">Operation cancellation token.</param>
    /// <returns>The operation result.</returns>
    Task<T> WaitAsync<T>(string prompt, Func<Task<T>> operation, Action cancel, CancellationToken token);
}
