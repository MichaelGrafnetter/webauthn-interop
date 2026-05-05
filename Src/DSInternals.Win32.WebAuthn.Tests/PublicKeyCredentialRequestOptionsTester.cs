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
}
