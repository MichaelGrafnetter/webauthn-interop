using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests;

[TestClass]
public class PublicKeyCredentialTester
{
    [TestMethod]
    public void AttestationPublicKeyCredential_FromJson_DeserializesConcreteTypes()
    {
        var credential = AttestationPublicKeyCredential.FromJson("""
            {
                "id": "AQID",
                "rawId": "AQID",
                "type": "public-key",
                "response": {
                    "clientDataJSON": "BAU",
                    "attestationObject": "Bgc"
                },
                "clientExtensionResults": {
                    "hmacCreateSecret": true
                }
            }
            """);

        Assert.IsNotNull(credential);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, credential.Id);
        CollectionAssert.AreEqual(new byte[] { 4, 5 }, credential.Response.ClientDataJson);
        CollectionAssert.AreEqual(new byte[] { 6, 7 }, credential.Response.AttestationObject);
        Assert.IsNotNull(credential.ClientExtensionResults);
        Assert.IsTrue(credential.ClientExtensionResults.HmacSecret);
    }

    [TestMethod]
    public void AssertionPublicKeyCredential_FromJson_DeserializesConcreteTypes()
    {
        var credential = AssertionPublicKeyCredential.FromJson("""
            {
                "id": "AQID",
                "rawId": "AQID",
                "type": "public-key",
                "response": {
                    "clientDataJSON": "BAU",
                    "authenticatorData": "Bgc",
                    "signature": "CAk",
                    "userHandle": "Cgs"
                },
                "clientExtensionResults": {
                    "appid": true,
                    "credBlob": "DA0"
                }
            }
            """);

        Assert.IsNotNull(credential);
        CollectionAssert.AreEqual(new byte[] { 1, 2, 3 }, credential.Id);
        CollectionAssert.AreEqual(new byte[] { 4, 5 }, credential.Response.ClientDataJson);
        CollectionAssert.AreEqual(new byte[] { 6, 7 }, credential.Response.AuthenticatorData);
        CollectionAssert.AreEqual(new byte[] { 8, 9 }, credential.Response.Signature);
        CollectionAssert.AreEqual(new byte[] { 10, 11 }, credential.Response.UserHandle);
        Assert.IsNotNull(credential.ClientExtensionResults);
        Assert.IsTrue(credential.ClientExtensionResults.AppID);
        CollectionAssert.AreEqual(new byte[] { 12, 13 }, credential.ClientExtensionResults.CredentialBlob);
    }
}
