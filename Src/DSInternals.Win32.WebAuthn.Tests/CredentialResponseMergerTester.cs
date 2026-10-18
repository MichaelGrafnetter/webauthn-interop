using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests;

[TestClass]
public class CredentialResponseMergerTester
{
    [TestMethod]
    public void MergeAttestation_PreservesJsonAndBackfillsNativeOutputs()
    {
        var jsonCredential = AttestationPublicKeyCredential.FromJson("""
            {
                "id": "AQID",
                "type": "public-key",
                "response": {
                    "clientDataJSON": "BAU",
                    "attestationObject": "Bgc"
                },
                "clientExtensionResults": {
                    "hmacCreateSecret": false,
                    "prf": {
                        "enabled": false
                    },
                    "futureExtension": {
                        "enabled": true
                    }
                }
            }
            """);
        Assert.IsNotNull(jsonCredential);

        var nativeCredential = new AttestationPublicKeyCredential
        {
            Id = [9],
            UnsignedExtensionOutputs = [18, 19],
            RawId = [9],
            Type = "public-key",
            Response = new AuthenticatorAttestationResponse
            {
                ClientData = [10],
                AttestationObject = [11],
                AuthenticatorData = [12],
                Transports = ["internal"]
            },
            ClientExtensionResults = new AuthenticationExtensionsClientAttestationOutputs
            {
                HmacSecret = true,
                MinimumPinLength = 6,
                CredentialProperties = new CredentialPropertiesOutputs { ResidentKey = true },
                LargeBlob = new LargeBlobAttestationOutputs { Supported = true },
                Prf = new PRFAttestationOutputs
                {
                    Enabled = true,
                    Results = new PRFValues { First = [13] }
                }
            }
        };

        var merged = jsonCredential.Merge(nativeCredential);

        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, merged.Id);
        CollectionAssert.AreEqual(new byte[] { 9 }, merged.RawId);
        CollectionAssert.AreEqual(new byte[] { 4, 5 }, merged.Response.ClientData);
        CollectionAssert.AreEqual(new byte[] { 6, 7 }, merged.Response.AttestationObject);
        CollectionAssert.AreEqual(new byte[] { 12 }, merged.Response.AuthenticatorData);
        CollectionAssert.AreEqual(new[] { "internal" }, merged.Response.Transports);
        Assert.IsTrue(merged.ClientExtensionResults!.HmacSecret);
        Assert.AreEqual((uint)6, merged.ClientExtensionResults.MinimumPinLength);
        Assert.IsTrue(merged.ClientExtensionResults.CredentialProperties!.ResidentKey);
        Assert.IsTrue(merged.ClientExtensionResults.LargeBlob!.Supported);
        Assert.IsTrue(merged.ClientExtensionResults.Prf!.Enabled);
        CollectionAssert.AreEqual(new byte[] { 13 }, merged.ClientExtensionResults.Prf.Results!.First);
        CollectionAssert.AreEqual(new byte[] { 18, 19 }, merged.UnsignedExtensionOutputs);

        using var serialized = JsonDocument.Parse(merged.ToString());
        var extensionResults = serialized.RootElement.GetProperty("clientExtensionResults");
        Assert.IsTrue(extensionResults.GetProperty("futureExtension").GetProperty("enabled").GetBoolean());
        Assert.IsFalse(serialized.RootElement.TryGetProperty("unsignedExtensionOutputs", out _));
    }

    [TestMethod]
    public void MergeAssertion_BackfillsNativeOutputsAndPreservesJsonValues()
    {
        var jsonCredential = AssertionPublicKeyCredential.FromJson("""
            {
                "id": "AQID",
                "rawId": "AQID",
                "type": "public-key",
                "response": {
                    "clientDataJSON": "BAU",
                    "signature": "Bgc"
                },
                "clientExtensionResults": {
                    "appid": true,
                    "futureExtension": "preserved"
                }
            }
            """);
        Assert.IsNotNull(jsonCredential);

        var nativeCredential = new AssertionPublicKeyCredential
        {
            Id = [9],
            UnsignedExtensionOutputs = [18, 19],
            RawId = [9],
            Type = "public-key",
            Response = new AuthenticatorAssertionResponse
            {
                ClientData = [10],
                AuthenticatorData = [11],
                Signature = [12],
                UserHandle = [13]
            },
            ClientExtensionResults = new AuthenticationExtensionsClientAssertionOutputs
            {
                AppID = false,
                CredentialBlob = [14],
                LargeBlob = new LargeBlobAssertionOutputs { Blob = [15] },
                Prf = new PRFAssertionOutputs
                {
                    Results = new PRFValues { First = [16], Second = [17] }
                }
            }
        };

        var merged = jsonCredential.Merge(nativeCredential);

        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, merged.Id);
        CollectionAssert.AreEqual(new byte[] { 4, 5 }, merged.Response.ClientData);
        CollectionAssert.AreEqual(new byte[] { 6, 7 }, merged.Response.Signature);
        CollectionAssert.AreEqual(new byte[] { 11 }, merged.Response.AuthenticatorData);
        CollectionAssert.AreEqual(new byte[] { 13 }, merged.Response.UserHandle);
        Assert.IsTrue(merged.ClientExtensionResults!.AppID);
        CollectionAssert.AreEqual(new byte[] { 14 }, merged.ClientExtensionResults.CredentialBlob);
        CollectionAssert.AreEqual(new byte[] { 15 }, merged.ClientExtensionResults.LargeBlob!.Blob);
        CollectionAssert.AreEqual(new byte[] { 16 }, merged.ClientExtensionResults.Prf!.Results!.First);
        CollectionAssert.AreEqual(new byte[] { 17 }, merged.ClientExtensionResults.Prf.Results.Second);
        CollectionAssert.AreEqual(new byte[] { 18, 19 }, merged.UnsignedExtensionOutputs);

        using var serialized = JsonDocument.Parse(merged.ToString());
        var extensionResults = serialized.RootElement.GetProperty("clientExtensionResults");
        Assert.AreEqual("preserved", extensionResults.GetProperty("futureExtension").GetString());
        Assert.IsFalse(serialized.RootElement.TryGetProperty("unsignedExtensionOutputs", out _));
    }

    [TestMethod]
    public void MergeAssertion_BackfillsHmacGetSecretOutput()
    {
        var jsonCredential = AssertionPublicKeyCredential.FromJson("""
            {
                "id": "AQID",
                "type": "public-key",
                "response": {
                    "authenticatorData": "BAU",
                    "signature": "Bgc"
                }
            }
            """);
        Assert.IsNotNull(jsonCredential);

        var nativeCredential = new AssertionPublicKeyCredential
        {
            Id = [9],
            Type = "public-key",
            Response = new AuthenticatorAssertionResponse
            {
                ClientData = [10],
                AuthenticatorData = [11],
                Signature = [12]
            },
            ClientExtensionResults = new AuthenticationExtensionsClientAssertionOutputs
            {
                HmacGetSecret = new HMACGetSecretOutput
                {
                    Output1 = [13],
                    Output2 = [14]
                }
            }
        };

        var merged = jsonCredential.Merge(nativeCredential);

        Assert.IsNotNull(merged.ClientExtensionResults?.HmacGetSecret);
        CollectionAssert.AreEqual(new byte[] { 13 }, merged.ClientExtensionResults.HmacGetSecret.Output1);
        CollectionAssert.AreEqual(new byte[] { 14 }, merged.ClientExtensionResults.HmacGetSecret.Output2);
    }
}
