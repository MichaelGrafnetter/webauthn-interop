#if NET5_0_OR_GREATER

using System;
using System.Text.Json;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Cryptography;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests;

[TestClass]
public sealed class KeePassXCPasskeyTester
{
    [TestMethod]
    public void KeePassXCPasskey_DeserializesBase64UrlFieldsAsBytes()
    {
        var passkey = KeePassXCPasskey.LoadFromJson("""
            {
              "username": "alice",
              "relyingParty": "example.com",
              "userHandle": "Cgs",
              "credentialId": "AQIDBAU",
              "privateKey": "-----BEGIN PRIVATE KEY-----\n-----END PRIVATE KEY-----"
            }
            """);

        Assert.IsNotNull(passkey);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5 }, passkey.CredentialId);
        CollectionAssert.AreEqual(new byte[] { 10, 11 }, passkey.UserHandle);
    }

    [TestMethod]
    public void KeePassXCPasskey_SerializesBytesAsBase64UrlFields()
    {
        var passkey = new KeePassXCPasskey
        {
            Username = "alice",
            RelyingParty = "example.com",
            CredentialId = new byte[] { 1, 2, 3, 4, 5 },
            UserHandle = new byte[] { 10, 11 },
            PrivateKey = P256PrivateKeyPem
        };

        string json = JsonSerializer.Serialize(passkey, WebAuthnJsonContext.Default.KeePassXCPasskey);

        StringAssert.Contains(json, @"""credentialId"":""AQIDBAU""");
        StringAssert.Contains(json, @"""userHandle"":""Cgs""");
    }

    [TestMethod]
    public void GetPasskeys_ReturnsSingleExportedPasskey()
    {
        var passkey = new KeePassXCPasskey
        {
            Username = "alice",
            RelyingParty = "example.com",
            Url = "https://example.com",
            CredentialId = new byte[] { 1, 2, 3, 4, 5 },
            UserHandle = new byte[] { 10, 11 },
            PrivateKey = P256PrivateKeyPem
        };

        var exportedPasskeys = passkey.GetPasskeys();

        Assert.HasCount(1, exportedPasskeys);
        var exportedPasskey = exportedPasskeys[0];
        Assert.AreEqual("alice", exportedPasskey.Username);
        Assert.AreEqual("example.com", exportedPasskey.RelyingParty);
        Assert.AreEqual("https://example.com", exportedPasskey.Url);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5 }, exportedPasskey.CredentialId);
        CollectionAssert.AreEqual(new byte[] { 10, 11 }, exportedPasskey.UserHandle);
        Assert.AreEqual(KeyType.EC2, exportedPasskey.KeyType);
        Assert.AreEqual(Algorithm.ES256, exportedPasskey.KeyAlgorithm);
        Assert.AreEqual(EllipticCurve.P256, exportedPasskey.KeyCurve);
        Assert.AreEqual(ApiConstants.KeePassXCAaGuid, exportedPasskey.AaGuid);
        Assert.IsTrue(exportedPasskey.Discoverable);
        exportedPasskey.PrivateKey.Dispose();
    }

    private const string P256PrivateKeyPem = """
        -----BEGIN PRIVATE KEY-----
        MIGHAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBG0wawIBAQQgcrrfVB2kcb14tSA9
        ltXeqavcfA7farw514tXHwC3KQuhRANCAAQS7Cl+ip8w8U6dVhVEy9sgcrpUgHCc
        vZAOVeEvaoAas9xYy/KkfZgdUiXAh62Q2d90sooDDYakr90Scb5j9Qnl
        -----END PRIVATE KEY-----
        """;
}

#endif
