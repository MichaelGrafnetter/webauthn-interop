using System.Text;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests;

[TestClass]
public class PublicKeyCredentialRequestOptionsTester
{
    [TestMethod]
    public void PublicKeyCredentialRequestOptions_DeserializeChallengeAsBase64Url()
    {
        var options = JsonSerializer.Deserialize("""
            {
                "challenge": "AQIDBAU",
                "rpId": "example.com",
                "allowCredentials": [
                    {
                        "type": "public-key",
                        "id": "BgcI"
                    }
                ]
            }
            """, WebAuthnJsonContext.Default.PublicKeyCredentialRequestOptions);

        Assert.IsNotNull(options);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4, 5 }, options.Challenge);
        Assert.IsNotNull(options.AllowCredentials);
        Assert.HasCount(1, options.AllowCredentials);
        CollectionAssert.AreEqual(new byte[] { 6, 7, 8 }, options.AllowCredentials[0].Id);
    }

    [TestMethod]
    public void PublicKeyCredentialRequestOptions_DeserializePreservesCustomHints()
    {
        var options = JsonSerializer.Deserialize("""
            {
                "challenge": "AQIDBAU",
                "rpId": "example.com",
                "hints": [
                    "client-device",
                    "future-custom-hint"
                ]
            }
            """, WebAuthnJsonContext.Default.PublicKeyCredentialRequestOptions);

        Assert.IsNotNull(options);
        Assert.IsNotNull(options.Hints);
        Assert.HasCount(2, options.Hints);
        Assert.AreEqual("client-device", options.Hints[0]);
        Assert.AreEqual("future-custom-hint", options.Hints[1]);
    }

    [TestMethod]
    public void PublicKeyCredentialRequestOptions_DeserializeAssertionExtensions()
    {
        var options = JsonSerializer.Deserialize("""
            {
              "challenge": "EGYtAMgi8B2Ey1FNVfVF93m5LEz_CfwTy00W2zoPEN4",
              "timeout": 120000,
              "allowCredentials": [],
              "hints": [
                "client-device"
              ],
              "userVerification": "preferred",
              "extensions": {
                "prf": {
                  "eval": {
                    "first": "UlAgcHJvdmlkZWQgY29uc3RhbnQ"
                  }
                },
                "uvm": true,
                "largeBlob": {
                  "write": "dGhpcyBpcyBkdW1teQ"
                },
                "payment": {
                  "isPayment": true,
                  "rpId": "opotonniee.github.io",
                  "topOrigin": "opotonniee.github.io",
                  "payeeName": "Merchant Shop",
                  "payeeOrigin": "https://merchant.com",
                  "total": {
                    "currency": "USD",
                    "value": "5.00"
                  },
                  "instrument": {
                    "displayName": "Fancy Card ****1234",
                    "icon": "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAQCAYAAAAMJL+VAAAACXBIWXMAAACxAAAAsQHGLUmNAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAAvJJREFUOI11lMtu5FQQhj+fc9ydZATkPkQzmeeAQQKx5AV4AZ4JiRcYiQ0L9qyQgB2gDBoxkJAQSOfSCbm1+2K76mdhdydphoV17HKduvxV/599/sU331ZV+RwRhQCQBAIhJACRZYEQAmaGWT2zS2r9m9vTb0l2dHT0Q6qr6vnJ8UmUhLvPHKbv0zPPu4QQGY0GVFWJu+MuXN74uLc2n9ri6dn5+wmIktjaegwEbm4Lri4v7ioDJAgx4OZUdXWvyiaw1ASV/H4HIGKaQnJ8fAqAmT1wkkRKOXKoqhK58Ln/s67nkkoiId4IyTRxCImUctyNshzPBWsfn0/S2EDMOvjsk3224l/o9h9+/angy4tPyfMuMSbcnfGowN3mYHmYZOnRIpubG3Q6C7zceQmCFFOi211kffGK7vUFdnXGGgUL3SVcws0YjQoqqx9C4prD3tl++oSFxUe89fYq0g6CBqIQIns7N6zXN6iYcFhs4mown0xGzVzehL3fweISv73eZWVtjZT6SN5AVNUlo3HBV7sfI8BlWG1U1XVTod9VOE3yf0Muq4qTXq/tquFQQmB1Td7JwbzZSUQWAjFE3IzaDLKMEJoC3DPMMmIMQMa4nNDNu4QsQMgQzrAYEUJBUsvGDz96j37/hmJQEmKHi4tT1jeWGY+NkDrg4vqyj5mzvLqOmVNVY1aWlzg8PGNldQNRsvzOImurS7x48TXA3Rb9/OMvVLVRljXmRjkeMxxcMZlUWDtscMyMs7NjyKDTWeC8H7m8vOa8f4xUE2NGljmj4Q1m9V2C09P+A6rLncHgdo7+Qu4NfKlLMRi2izDEzWaycV9yklqibb37mJSnNrhwHFqtUXua1RwcHBFjjssoJ2PKasL2s6d08pw/Dw7ZfvaE8WTC7u97DUSSTFI86vUa8XooWA9ELKaclDoNq0fDVpecg/2Dmc+rV69xN9wdkKW/e73v9v7Y/0BS/I/kTpVOIgsRyFDbSRtgto6ileyZSMrc9f2/Mc5XutMXjh8AAAAASUVORK5CYII="
                  }
                }
              }
            }
            """, WebAuthnJsonContext.Default.PublicKeyCredentialRequestOptions);

        Assert.IsNotNull(options);
        CollectionAssert.AreEqual(
            Base64UrlConverter.FromBase64UrlString("EGYtAMgi8B2Ey1FNVfVF93m5LEz_CfwTy00W2zoPEN4"),
            options.Challenge);
        Assert.AreEqual((uint?)120000, options.Timeout);
        Assert.IsNotNull(options.AllowCredentials);
        Assert.HasCount(0, options.AllowCredentials);
        Assert.IsNotNull(options.Hints);
        Assert.HasCount(1, options.Hints);
        Assert.AreEqual("client-device", options.Hints[0]);
        Assert.AreEqual(UserVerificationRequirement.Preferred, options.UserVerification);
        Assert.IsNotNull(options.Extensions);
        Assert.IsNotNull(options.Extensions.Prf);
        Assert.IsNotNull(options.Extensions.Prf.Eval);
        CollectionAssert.AreEqual(Encoding.UTF8.GetBytes("RP provided constant"), options.Extensions.Prf.Eval.First);
        Assert.IsNull(options.Extensions.Prf.Eval.Second);
        Assert.IsNotNull(options.Extensions.LargeBlob);
        CollectionAssert.AreEqual(Encoding.UTF8.GetBytes("this is dummy"), options.Extensions.LargeBlob.Write);
        Assert.AreEqual(CredentialLargeBlobOperation.Set, options.Extensions.LargeBlob.Operation);
    }

    [TestMethod]
    public void PublicKeyCredentialRequestOptions_SerializeChallengeAsBase64Url()
    {
        var options = new PublicKeyCredentialRequestOptions
        {
            Challenge = new byte[] { 1, 2, 3, 4, 5 },
            RpId = "example.com"
        };

        string json = JsonSerializer.Serialize(options, WebAuthnJsonContext.Default.PublicKeyCredentialRequestOptions);

        StringAssert.Contains(json, @"""challenge"":""AQIDBAU""");
    }

    [TestMethod]
    public void PublicKeyCredentialRequestOptions_SerializePreservesCustomHints()
    {
        var options = new PublicKeyCredentialRequestOptions
        {
            Challenge = new byte[] { 1, 2, 3, 4, 5 },
            RpId = "example.com",
            Hints =
            [
                "future-custom-hint"
            ]
        };

        string json = JsonSerializer.Serialize(options, WebAuthnJsonContext.Default.PublicKeyCredentialRequestOptions);

        StringAssert.Contains(json, @"""future-custom-hint""");
    }
}
