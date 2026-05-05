#if NET8_0_OR_GREATER

using System;
using System.Security.Cryptography;
using NSec.Cryptography;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// An <see cref="AsymmetricAlgorithm"/> wrapper around an NSec Ed25519 private key.
/// </summary>
public sealed class Ed25519 : AsymmetricAlgorithm
{
    // Fully-qualified to avoid resolving to the inherited AsymmetricAlgorithm.SignatureAlgorithm string property.
    private static readonly NSec.Cryptography.SignatureAlgorithm _algorithm = NSec.Cryptography.SignatureAlgorithm.Ed25519;

    private Key? _key;

    private Ed25519()
    {
        LegalKeySizesValue = [new KeySizes(256, 256, 0)];
    }

    /// <summary>
    /// Creates a new <see cref="Ed25519"/> instance. Import a key with <see cref="AsymmetricAlgorithm.ImportFromPem"/>.
    /// </summary>
    public static new Ed25519 Create() => new();

    public override string? SignatureAlgorithm => "Ed25519";

    /// <summary>
    /// Imports an Ed25519 private key from a PKCS#8 DER blob.
    /// Called internally by <see cref="AsymmetricAlgorithm.ImportFromPem"/> for "BEGIN PRIVATE KEY" blocks.
    /// </summary>
    public override void ImportPkcs8PrivateKey(ReadOnlySpan<byte> source, out int bytesRead)
    {
        _key?.Dispose();
        _key = Key.Import(_algorithm, source, KeyBlobFormat.PkixPrivateKey);
        KeySizeValue = _key.Size;
        bytesRead = source.Length;
    }

    /// <summary>
    /// Signs the given data using Ed25519.
    /// </summary>
    public byte[] SignData(ReadOnlySpan<byte> data, HashAlgorithmName hashAlgorithm)
    {
        if (hashAlgorithm != HashAlgorithmName.SHA512)
            throw new ArgumentException("Ed25519 requires SHA-512 as the hash algorithm.", nameof(hashAlgorithm));

        if (_key == null) throw new InvalidOperationException("No key has been imported.");

        return _algorithm.Sign(_key, data);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
            _key?.Dispose();
        base.Dispose(disposing);
    }
}

#endif
