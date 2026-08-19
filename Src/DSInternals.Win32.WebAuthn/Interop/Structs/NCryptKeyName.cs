using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DSInternals.Win32.WebAuthn.Interop;

/// <summary>
/// Identifies a CNG key returned by <c>NCryptEnumKeys</c>.
/// </summary>
/// <remarks>Corresponds to NCryptKeyName.</remarks>
[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public sealed class NCryptKeyName
{
    /// <summary>
    /// The name of the key.
    /// </summary>
    public string? Name;

    /// <summary>
    /// The identifier of the cryptographic algorithm associated with the key.
    /// </summary>
    public string? AlgorithmGroup;

    /// <summary>
    /// Legacy CryptoAPI key spec (AT_KEYEXCHANGE or AT_SIGNATURE), or zero for CNG-only keys.
    /// </summary>
    public KeyNumber LegacyKeySpec;

    /// <summary>
    /// Flags describing the key, e.g. whether it is a machine key.
    /// </summary>
    public NCryptKeyNameFlags Flags;
}
