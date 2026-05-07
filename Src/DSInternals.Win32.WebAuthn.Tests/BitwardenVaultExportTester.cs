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
          "encKeyValidation_DO_NOT_EDIT": "2.AAECAwQFBgcICQoLDA0ODw==|KDzpex/bIETxvmIReEnPfdi8exnGc1pcimLqd5BG76h95qi0+PazU1psYD7XAvSE|L//aYp3bwyLH/l84faewg+PW+j+90LgObyMae9bJD7c=",
          "data": "2.EBESExQVFhcYGRobHB0eHw==|tOPTF19vdZMUbjiybWSMKpcJcSdBJbyLZZapV8/F4Y2EoPGsyfKD3oiqDo9Cgr3cJg0pQZI9FWqg9ZkKqVCKXhz2nnNDI1st/LnDsSFUmbbCNPzt5ejScqOGSiFA+MxXQnYZ7llLHNuApp7Ep3gLYqjcSo8yGwPRObbB+pxLWn6aOac1wOjky04resNRCx0ClAklaGJFIdt1YYYc22TWUe9Rv2dacJhwFybEVApew+sKtwCOScBDmkh3whCx1KR59Lv8wIpG/59fIns+mwTkHbpHIxDPQJEAD4Jm/7orsdGtXHASeOiby0TwDXv2/afHQ40E0eWJFfKLBbqeLdAjhMhutj5KXi91MwBT/U++XRJOGneQptDEqmo7PR+gjUnd8Mgn+Qg6XpMNe1TbEN2zTJW+LRUqget8Z7Tz84m0iFh3Nz1gaiNh8a631fqGTuwi18HewW1K8b9YwxQkEFSwJA1/X3krwxuVfl4nfc0BclWbNUQHqShEIF/11V7WELyM3XvdKAgP0U5/USO+dzxBXMnLWFs5a2Bdx4HiBBsli7kmOsPprZT4RCIvlSvBGXw5UMLp8pVl7NtJTmbdUbl/RgjwQ9NDl/loRaVfNg44Zm7gJK3Oo/Bzgu6tv8fsIgQMhuB4ImCV8r6CM6UTLQCiqkAG52Gj4WOUIrNlB+vLMTUzop5dfIdYUiu/9ocNpcXGmsUxaj0nKx0r0pa213ZMTlhUfWhKGyeYiUfOCv7QJJI=|VJr+auCUy1Ub0q9Ju2cOPNdT/p1sfOsNCQ9N5SynLus="
        }
        """;
}

#endif
