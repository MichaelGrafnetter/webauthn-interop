#if NET8_0_OR_GREATER

using System;
using System.Security.Cryptography;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Cryptography;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests;

[TestClass]
public sealed class BitwardenVaultExportTester
{
    private const string ExpectedCredentialId = "dsO1yNiARuafV87QYGB1Vw";
    private static readonly byte[] ExpectedCredentialIdBytes = Base64UrlConverter.FromBase64UrlString(ExpectedCredentialId);

    [TestMethod]
    public void LoadFromJson_CleartextExport_ParsesFido2Credential()
    {
        string json = CreateCleartextExportJson(CreateP256KeyValue());

        var export = BitwardenCleartextVaultExport.LoadFromJson(json);
        var passkeys = export.GetPasskeys();

        Assert.HasCount(1, passkeys);
        var passkey = passkeys[0];
        Assert.AreEqual(ApiConstants.BitwardenAaGuid, passkey.AaGuid);
        Assert.AreEqual("example.com", passkey.RelyingParty);
        Assert.AreEqual("Example", passkey.RelyingPartyName);
        Assert.AreEqual("alice", passkey.Username);
        Assert.AreEqual("Alice", passkey.UserDisplayName);
        CollectionAssert.AreEqual(ExpectedCredentialIdBytes, passkey.CredentialId);
        Assert.AreEqual(5u, passkey.SignatureCounter);
        Assert.IsFalse(passkey.Discoverable);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, passkey.UserHandle);
        Assert.AreEqual(KeyType.EC2, passkey.KeyType);
        Assert.AreEqual(Algorithm.ES256, passkey.KeyAlgorithm);
        Assert.AreEqual(EllipticCurve.P256, passkey.KeyCurve);

        using var key = passkey.PrivateKey;
        Assert.AreEqual(Algorithm.ES256, SoftwareAuthenticator.DetectAlgorithm(key));
    }

    [TestMethod]
    public void LoadFromJson_EncryptedExport_DecryptsAndParsesFido2Credential()
    {
        var export = BitwardenEncryptedVaultExport.LoadFromJson(EncryptedExportJson);
        var passkeys = export.Decrypt("secret").GetPasskeys();

        Assert.HasCount(1, passkeys);
        Assert.AreEqual("example.com", passkeys[0].RelyingParty);
        CollectionAssert.AreEqual(ExpectedCredentialIdBytes, passkeys[0].CredentialId);
        Assert.AreEqual(5u, passkeys[0].SignatureCounter);
    }

    [TestMethod]
    public void LoadFromJson_EncryptedExportWithoutPassword_Throws()
    {
        var export = BitwardenEncryptedVaultExport.LoadFromJson(EncryptedExportJson);

        Assert.ThrowsExactly<ArgumentNullException>(() => export.Decrypt(null!));
    }

    private static string CreateCleartextExportJson(string keyValue)
    {
        return $$"""
            {
              "encrypted": false,
              "items": [
                {
                  "name": "example.com",
                  "login": {
                    "fido2Credentials": [
                      {
                        "credentialId": "76c3b5c8-d880-46e6-9f57-ced060607557",
                        "keyType": "public-key",
                        "keyAlgorithm": "ECDSA",
                        "keyCurve": "P-256",
                        "keyValue": "{{keyValue}}",
                        "rpId": "example.com",
                        "userHandle": "AQID",
                        "userName": "alice",
                        "counter": "5",
                        "rpName": "Example",
                        "userDisplayName": "Alice",
                        "discoverable": "false"
                      }
                    ]
                  }
                }
              ]
            }
            """;
    }

    private static string CreateP256KeyValue()
    {
        using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        return ToBase64UrlString(key.ExportPkcs8PrivateKey());
    }

    private static string ToBase64UrlString(byte[] value)
    {
        return Convert.ToBase64String(value)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }

    private const string EncryptedExportJson = """
        {
          "encrypted": true,
          "passwordProtected": true,
          "salt": "dGVzdHNhbHQ=",
          "kdfType": 0,
          "kdfIterations": 1000,
          "encKeyValidation_DO_NOT_EDIT": "2.AAECAwQFBgcICQoLDA0ODw==|b9XNIQPuTmILvjmC3OOajqtRyvJUnQh8s6vEoZm3gvFFbcv0RcM4DNRQtTYvEWNn|Fl3xOjOFYWWd+LQy6tpmlZn9L+y4qalS+vyIQ+5WZ14=",
          "data": "2.EBESExQVFhcYGRobHB0eHw==|OJOrLzdeQMVTyCBKF5GkD+Ge6KSegDmb6uBEtQ0DJLMs4PBiI+kmdiLingsWHW8zdlrlcwXOo6FeJns5RZiCRlsKKqKVWvwN2FuVJv+INFHJxA6Gyw89/mVcZRB0a4ChjXp2/ASQUla3wfJ3QAcLjGM7Xiy9QTzwf40sC1/X9dhVwH5HVDbLRQHlZeq3Mtm+9CbLjquj7uB6ENC+8nOoD+oMCR9pkE/S6nttq/YNPvcA4tWN+J8jnIn91J/aH+8E+nwRUdW1k5+oLeepo/Kwfgia34LutIkw3SywrrVB6TaMA6gMDoVcQPjNuNxMsXh0JOV0H/bEha4odO3ySCsQc2zOgZyvNfMdhHEXpjXDKmQHadIkrsvz9M1D05fVGBtpvfUt6L5HAfTXkIQMboNQS7vm5A1HvbcTyArRxmO4usDWqs/Pxk5m8C9EaiKt21sfvfJvfuPF/KNyiMludTw+lb8U6tD2aAy02g0wNC8qrsgM1ivhG/7G2PU1tG2vxyX999brlXrjNybRG6gVC7ek8ay0tfGp4dQ8luBdn2cQ8lzmI5/brBpiJ4LFtXdYLtSLsOOy/YaudOe1lLe+bAcW6EleXmGNQWGjUJLds5gBueq+d1w498npDFJxIyxySxWE9zb7YNJIUTw4x8jH7CaUa/xNqazU6ZmONboDp4cQiEwkhYGc2ynA3KSdMlNhHJFs0c8Lt/wX9KdkmZpLF7RNcnEzPFxGXfvKofrgRvsglTCGFHGHNC4UReS6XW0lOhvNMKxUU4zhQprIJmaOuHLPk/Wxe6IHHqp4dfMRcHzcJ8qo26NIBnYO4Ky4FIXj9T2G+yJq1Fhd8LyCeLz8lwULjp/SYZ9OB3l71EIlEyuneXk+dDVaV/IkXdOOwOd80JWCIPthQQGZxJ935uYJ313LZQI8TDWZIK1c0OnyOIhVdK8qNeGLdXnRE6xq6bNS9UiUJCQDEptOarGCExJzyx7Xlo1A1i0OkEOJkHRlH4J0ImVrxxwDE0pH73l6tZ/QCQSJ/bn1MPSEXtZx2h4L+YmD0WEGTIEGyE2TijkJc3GOOjI=|AZY43t0paYjd/O+nOPLspsEoRZVK/qsBJOPDI7fde94="
        }
        """;
}

#endif
