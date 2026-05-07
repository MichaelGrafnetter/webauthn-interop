using System;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>
/// Caches the last used signing dialog values so they persist across dialog invocations.
/// Shared between both attestation and assertion signing dialogs.
/// </summary>
internal sealed class SigningDialogCache
{
    public Guid? AaGuid { get; set; }
    public uint? SignatureCounter { get; set; }
    public string? KeyFilePath { get; set; }
    public bool? UserPresent { get; set; }
    public byte[]? CredentialId { get; set; }
    public byte[]? UserHandle { get; set; }
}
