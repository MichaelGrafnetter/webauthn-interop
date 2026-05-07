#if NET8_0_OR_GREATER

using System;
using System.Security.Cryptography;
using DSInternals.Win32.WebAuthn.COSE;
using DSInternals.Win32.WebAuthn.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests;

[TestClass]
public sealed class CredentialExchangeFileTester
{
    [TestMethod]
    public void LoadFromJson_PasskeyCredential_IsParsed()
    {
        string json = CreateExportJson(CreateP256KeyValue());

        var export = CredentialExchangeFile.LoadFromJson(json);

        Assert.IsNotNull(export.Version);
        Assert.AreEqual((byte)1, export.Version!.Major);
        Assert.AreEqual((byte)0, export.Version!.Minor);
        Assert.AreEqual("example-exporter.com", export.ExporterRelyingPartyId);
        Assert.AreEqual("Example Exporter", export.ExporterDisplayName);
        Assert.AreEqual(1700000000UL, export.Timestamp);
        Assert.HasCount(1, export.Accounts!);

        var account = export.Accounts![0];
        Assert.AreEqual("alice", account.Username);
        Assert.AreEqual("alice@example.com", account.Email);
        Assert.HasCount(1, account.Items!);

        var item = account.Items![0];
        Assert.AreEqual("example.com", item.Title);
        Assert.HasCount(1, item.Credentials!);

        var credential = item.Credentials![0];
        Assert.IsInstanceOfType<CredentialExchangePasskey>(credential);
        var passkey = (CredentialExchangePasskey)credential;
        Assert.AreEqual("example.com", passkey.RelyingPartyId);
        Assert.AreEqual("alice", passkey.Username);
        Assert.AreEqual("Alice", passkey.UserDisplayName);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5 }, passkey.CredentialId);
        CollectionAssert.AreEqual(new byte[] { 10, 11 }, passkey.UserHandle);
    }

    [TestMethod]
    public void GetPasskeys_PasskeyCredential_ReturnsExportedPasskey()
    {
        string json = CreateExportJson(CreateP256KeyValue());

        var export = CredentialExchangeFile.LoadFromJson(json);
        var passkeys = export.GetPasskeys();

        Assert.HasCount(1, passkeys);
        var passkey = passkeys[0];
        Assert.AreEqual(Guid.Empty, passkey.AaGuid);
        Assert.AreEqual("example.com", passkey.RelyingParty);
        Assert.AreEqual("alice", passkey.Username);
        Assert.AreEqual("Alice", passkey.UserDisplayName);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5 }, passkey.CredentialId);
        CollectionAssert.AreEqual(new byte[] { 10, 11 }, passkey.UserHandle);
        Assert.AreEqual(0u, passkey.SignatureCounter);
        Assert.IsTrue(passkey.Discoverable);
        Assert.AreEqual(KeyType.EC2, passkey.KeyType);
        Assert.AreEqual(Algorithm.ES256, passkey.KeyAlgorithm);
        Assert.AreEqual(EllipticCurve.P256, passkey.KeyCurve);

        using var key = passkey.PrivateKey;
        Assert.AreEqual(Algorithm.ES256, SoftwareAuthenticator.DetectAlgorithm(key));
    }

    [TestMethod]
    public void GetPasskeys_WithExplicitAaGuid_AssignsAaGuid()
    {
        string json = CreateExportJson(CreateP256KeyValue());
        var expectedAaGuid = new Guid("11111111-2222-3333-4444-555555555555");

        var export = CredentialExchangeFile.LoadFromJson(json);
        var passkeys = export.GetPasskeys(expectedAaGuid);

        Assert.HasCount(1, passkeys);
        Assert.AreEqual(expectedAaGuid, passkeys[0].AaGuid);
        passkeys[0].PrivateKey.Dispose();
    }

    [TestMethod]
    public void GetPasskeys_NonPasskeyCredential_IsIgnored()
    {
        string keyValue = CreateP256KeyValue();
        string json = $$"""
            {
              "version": { "major": 1, "minor": 0 },
              "exporterRpId": "example-exporter.com",
              "exporterDisplayName": "Example Exporter",
              "timestamp": 1700000000,
              "accounts": [
                {
                  "id": "AAAA",
                  "username": "alice",
                  "email": "alice@example.com",
                  "items": [
                    {
                      "id": "BBBB",
                      "title": "example.com",
                      "credentials": [
                        {
                          "type": "basic-auth",
                          "username": "alice",
                          "password": "secret"
                        },
                        {
                          "type": "passkey",
                          "credentialId": "AQIDBAU",
                          "rpId": "example.com",
                          "username": "alice",
                          "userDisplayName": "Alice",
                          "userHandle": "Cgs",
                          "key": "{{keyValue}}"
                        }
                      ]
                    }
                  ]
                }
              ]
            }
            """;

        var export = CredentialExchangeFile.LoadFromJson(json);
        var passkeys = export.GetPasskeys();

        Assert.HasCount(1, passkeys);
        Assert.AreEqual("example.com", passkeys[0].RelyingParty);
        passkeys[0].PrivateKey.Dispose();
    }

    [TestMethod]
    public void LoadFromJson_PasskeyWithFido2Extensions_ParsesExtensions()
    {
        string keyValue = CreateP256KeyValue();
        string json = $$"""
            {
              "version": { "major": 1, "minor": 0 },
              "exporterRpId": "example-exporter.com",
              "exporterDisplayName": "Example Exporter",
              "timestamp": 1700000000,
              "accounts": [
                {
                  "id": "AAAA",
                  "username": "alice",
                  "email": "alice@example.com",
                  "items": [
                    {
                      "id": "BBBB",
                      "title": "example.com",
                      "credentials": [
                        {
                          "type": "passkey",
                          "credentialId": "AQIDBAU",
                          "rpId": "example.com",
                          "username": "alice",
                          "userDisplayName": "Alice",
                          "userHandle": "Cgs",
                          "key": "{{keyValue}}",
                          "fido2Extensions": {
                            "credBlob": "QUJD",
                            "hmacCredentials": {
                              "algorithm": "hmac-sha256",
                              "credWithUV": "AAEC",
                              "credWithoutUV": "AwQF"
                            },
                            "largeBlob": {
                              "uncompressedSize": 42,
                              "data": "BgcICQ"
                            },
                            "payments": true
                          }
                        }
                      ]
                    }
                  ]
                }
              ]
            }
            """;

        var export = CredentialExchangeFile.LoadFromJson(json);
        var credential = export.Accounts![0].Items![0].Credentials![0];
        var extensions = ((CredentialExchangePasskey)credential).Fido2Extensions;

        Assert.IsNotNull(extensions);
        CollectionAssert.AreEqual(new byte[] { 0x41, 0x42, 0x43 }, extensions!.CredBlob);
        Assert.IsNotNull(extensions.HmacCredentials);
        Assert.AreEqual("hmac-sha256", extensions.HmacCredentials!.Algorithm);
        CollectionAssert.AreEqual(new byte[] { 0, 1, 2 }, extensions.HmacCredentials!.CredentialWithUserVerification);
        CollectionAssert.AreEqual(new byte[] { 3, 4, 5 }, extensions.HmacCredentials!.CredentialWithoutUserVerification);
        Assert.IsNotNull(extensions.LargeBlob);
        Assert.AreEqual(42UL, extensions.LargeBlob!.UncompressedSize);
        CollectionAssert.AreEqual(new byte[] { 6, 7, 8, 9 }, extensions.LargeBlob!.Data);
        Assert.IsTrue(extensions.Payments);
    }

    private static string CreateExportJson(string keyValue)
    {
        return $$"""
            {
              "version": { "major": 1, "minor": 0 },
              "exporterRpId": "example-exporter.com",
              "exporterDisplayName": "Example Exporter",
              "timestamp": 1700000000,
              "accounts": [
                {
                  "id": "AAAA",
                  "username": "alice",
                  "email": "alice@example.com",
                  "items": [
                    {
                      "id": "BBBB",
                      "title": "example.com",
                      "credentials": [
                        {
                          "type": "passkey",
                          "credentialId": "AQIDBAU",
                          "rpId": "example.com",
                          "username": "alice",
                          "userDisplayName": "Alice",
                          "userHandle": "Cgs",
                          "key": "{{keyValue}}"
                        }
                      ]
                    }
                  ]
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
}

#endif
