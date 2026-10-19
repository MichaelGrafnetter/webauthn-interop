namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>Contains the confirmed PIN-policy request without any PIN material.</summary>
public sealed class PinPolicyChangeInput
{
    /// <summary>Gets the requested minimum length, or null to preserve the existing minimum.</summary>
    public uint? MinimumPinLength { get; }

    /// <summary>Gets the requested forced-change setting, or null when unreported and unchanged.</summary>
    public bool? ForcePinChange { get; }

    /// <summary>Gets additional relying-party IDs, or an empty array to preserve the existing list.</summary>
    public string[] RelyingPartyIds { get; }

    /// <summary>Initializes an accepted policy request.</summary>
    /// <param name="minimumPinLength">Optional minimum length.</param>
    /// <param name="forcePinChange">Optional forced-change request.</param>
    /// <param name="relyingPartyIds">Additional relying-party IDs.</param>
    public PinPolicyChangeInput(uint? minimumPinLength, bool? forcePinChange, string[] relyingPartyIds)
    {
        System.ArgumentNullException.ThrowIfNull(relyingPartyIds);
        MinimumPinLength = minimumPinLength;
        ForcePinChange = forcePinChange;
        RelyingPartyIds = relyingPartyIds;
    }
}
