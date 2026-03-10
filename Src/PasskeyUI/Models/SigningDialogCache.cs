using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>
/// Caches the last used signing dialog values so they persist across dialog invocations.
/// Shared between both attestation and assertion signing dialogs.
/// </summary>
public static class SigningDialogCache
{
    public static string? AaGuidString { get; set; }
    public static Algorithm? SelectedAlgorithm { get; set; }
    public static uint? SignatureCounter { get; set; }
    public static string? KeyFilePath { get; set; }
    public static bool? UserPresent { get; set; }
    public static KeePassXCPasskey? LoadedPasskey { get; set; }
    public static string? CredentialIdString { get; set; }
}
