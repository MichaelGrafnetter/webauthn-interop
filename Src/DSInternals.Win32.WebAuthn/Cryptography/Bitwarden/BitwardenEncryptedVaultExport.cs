#if NET5_0_OR_GREATER

using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn.Cryptography;

/// <summary>
/// Represents the top-level structure of an encrypted Bitwarden vault export.
/// </summary>
public sealed class BitwardenEncryptedVaultExport
{
    private const int Pbkdf2Sha256OutputLength = 32;
    private const int BitwardenEncryptedStringTypeAes256CbcHmacSha256 = 2;

    /// <summary>
    /// Indicates whether the export content is encrypted.
    /// </summary>
    [JsonPropertyName("encrypted")]
    public bool Encrypted { get; set; }

    /// <summary>
    /// Indicates whether the export is protected by a user-supplied password.
    /// </summary>
    [JsonPropertyName("passwordProtected")]
    public bool PasswordProtected { get; set; }

    /// <summary>
    /// The salt decoded from regular Base64 for Bitwarden's password-based key derivation.
    /// </summary>
    [JsonPropertyName("salt")]
    public byte[]? Salt { get; set; }

    /// <summary>
    /// The Bitwarden KDF identifier.
    /// </summary>
    [JsonPropertyName("kdfType")]
    public int KdfType { get; set; }

    /// <summary>
    /// The number of KDF iterations used to derive the export key.
    /// </summary>
    [JsonPropertyName("kdfIterations")]
    public int KdfIterations { get; set; }

    /// <summary>
    /// The memory parameter for KDFs that use one.
    /// </summary>
    [JsonPropertyName("kdfMemory")]
    public int? KdfMemory { get; set; }

    /// <summary>
    /// The parallelism parameter for KDFs that use one.
    /// </summary>
    [JsonPropertyName("kdfParallelism")]
    public int? KdfParallelism { get; set; }

    /// <summary>
    /// Encrypted validation data used to verify the password-derived key.
    /// </summary>
    [JsonPropertyName("encKeyValidation_DO_NOT_EDIT")]
    public string? EncKeyValidation { get; set; }

    /// <summary>
    /// The encrypted cleartext vault export payload.
    /// </summary>
    [JsonPropertyName("data")]
    public string? Data { get; set; }

    /// <summary>
    /// Loads and parses an encrypted Bitwarden JSON vault export.
    /// </summary>
    /// <param name="filePath">Path to the encrypted Bitwarden JSON export.</param>
    /// <returns>The parsed encrypted Bitwarden export.</returns>
    public static BitwardenEncryptedVaultExport LoadFromFile(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return LoadFromJson(json);
    }

    /// <summary>
    /// Parses an encrypted Bitwarden JSON vault export.
    /// </summary>
    /// <param name="json">The encrypted Bitwarden JSON export content.</param>
    /// <returns>The parsed encrypted Bitwarden export.</returns>
    public static BitwardenEncryptedVaultExport LoadFromJson(string json)
    {
        ArgumentNullException.ThrowIfNull(json);

        return JsonSerializer.Deserialize(json, WebAuthnJsonContext.Default.BitwardenEncryptedVaultExport)
            ?? throw new JsonException("Failed to deserialize encrypted Bitwarden vault export.");
    }

    /// <summary>
    /// Decrypts this encrypted export into a cleartext Bitwarden vault export.
    /// </summary>
    /// <param name="password">The password used to protect the export.</param>
    /// <returns>The decrypted cleartext Bitwarden export.</returns>
    public BitwardenCleartextVaultExport Decrypt(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        if (!Encrypted)
        {
            throw new InvalidOperationException("Bitwarden export is not encrypted.");
        }

        if (!PasswordProtected)
        {
            throw new NotSupportedException("Account-restricted Bitwarden encrypted exports are not supported.");
        }

        if (KdfType != 0)
        {
            throw new NotSupportedException("Only PBKDF2-SHA256 Bitwarden encrypted exports are supported.");
        }

        if (KdfIterations <= 0)
        {
            throw new JsonException("Bitwarden export is missing valid KDF iteration count.");
        }

        if (Salt is null || EncKeyValidation is null || Data is null)
        {
            throw new JsonException("Bitwarden encrypted export is missing required encryption fields.");
        }

        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] kdfKey = Rfc2898DeriveBytes.Pbkdf2(passwordBytes, Salt, KdfIterations, HashAlgorithmName.SHA256, Pbkdf2Sha256OutputLength);
        byte[] encryptionKey = new byte[Pbkdf2Sha256OutputLength];
        byte[] macKey = new byte[Pbkdf2Sha256OutputLength];
        HKDF.Expand(HashAlgorithmName.SHA256, kdfKey, encryptionKey, "enc"u8);
        HKDF.Expand(HashAlgorithmName.SHA256, kdfKey, macKey, "mac"u8);

        string validation = DecryptEncryptedString(EncKeyValidation, encryptionKey, macKey);
        if (!Guid.TryParse(validation, out _))
        {
            throw new CryptographicException("Invalid Bitwarden encrypted export password.");
        }

        string cleartextJson = DecryptEncryptedString(Data, encryptionKey, macKey);
        return BitwardenCleartextVaultExport.LoadFromJson(cleartextJson);
    }

    /// <summary>
    /// Authenticates and decrypts one Bitwarden encrypted string value.
    /// </summary>
    /// <param name="encryptedString">The Bitwarden encrypted string.</param>
    /// <param name="encryptionKey">The AES-CBC encryption key.</param>
    /// <param name="macKey">The HMAC-SHA256 key.</param>
    /// <returns>The decrypted UTF-8 text.</returns>
    private static string DecryptEncryptedString(string encryptedString, byte[] encryptionKey, byte[] macKey)
    {
        var parsed = ParseEncryptedString(encryptedString);
        byte[] computedMac = ComputeMac(macKey, parsed.Iv, parsed.Data);
        if (!CryptographicOperations.FixedTimeEquals(computedMac, parsed.Mac))
        {
            throw new CryptographicException("Invalid Bitwarden encrypted export password or data.");
        }

        using var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = encryptionKey;
        aes.IV = parsed.Iv;

        using ICryptoTransform decryptor = aes.CreateDecryptor();
        byte[] decrypted = decryptor.TransformFinalBlock(parsed.Data, 0, parsed.Data.Length);
        return Encoding.UTF8.GetString(decrypted);
    }

    /// <summary>
    /// Parses Bitwarden's type-prefixed encrypted string format.
    /// </summary>
    /// <param name="encryptedString">The encrypted string value from the export.</param>
    /// <returns>The decoded encrypted string components.</returns>
    private static (byte[] Iv, byte[] Data, byte[] Mac) ParseEncryptedString(string encryptedString)
    {
        int separator = encryptedString.IndexOf('.', StringComparison.Ordinal);
        if (separator < 0)
        {
            throw new FormatException("Invalid Bitwarden encrypted string.");
        }

        string typeText = encryptedString[..separator];
        if (!int.TryParse(typeText, NumberStyles.Integer, CultureInfo.InvariantCulture, out int type)
            || type != BitwardenEncryptedStringTypeAes256CbcHmacSha256)
        {
            throw new NotSupportedException("Only Bitwarden type 2 encrypted strings are supported.");
        }

        string[] parts = encryptedString[(separator + 1)..].Split('|');
        if (parts.Length != 3)
        {
            throw new FormatException("Invalid Bitwarden type 2 encrypted string.");
        }

        byte[] iv = Convert.FromBase64String(parts[0]);
        byte[] data = Convert.FromBase64String(parts[1]);
        byte[] mac = Convert.FromBase64String(parts[2]);

        if (iv.Length != 16)
        {
            throw new FormatException("Bitwarden encrypted string IV must be 16 bytes.");
        }

        if (mac.Length != 32)
        {
            throw new FormatException("Bitwarden encrypted string MAC must be 32 bytes.");
        }

        return (iv, data, mac);
    }

    /// <summary>
    /// Computes Bitwarden's encrypted string authentication tag.
    /// </summary>
    /// <param name="macKey">The HMAC-SHA256 key.</param>
    /// <param name="iv">The encrypted string IV.</param>
    /// <param name="data">The encrypted string payload.</param>
    /// <returns>The computed HMAC-SHA256 tag.</returns>
    private static byte[] ComputeMac(byte[] macKey, byte[] iv, byte[] data)
    {
        using IncrementalHash hmac = IncrementalHash.CreateHMAC(HashAlgorithmName.SHA256, macKey);
        hmac.AppendData(iv);
        hmac.AppendData(data);
        return hmac.GetHashAndReset();
    }
}

#endif
