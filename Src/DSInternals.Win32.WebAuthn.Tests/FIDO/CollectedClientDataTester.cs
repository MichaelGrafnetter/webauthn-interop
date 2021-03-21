using System;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn.FIDO.Tests
{
    [TestClass]
    public class CollectedClientDataTester
    {
        // TODO: Perform serialization tests based on https://www.w3.org/TR/webauthn/#clientdatajson-verification

        [TestMethod]
        public void CollectedClientData_Serialize_Vector1()
        {
            var input = new CollectedClientData()
            {
                Type = "webauthn.create",
                Challenge = new byte[]{ 0x01, 0x02, 0x03, 0x04, 0x05},
                Origin = "https://login.microsoft.com"
            };

            string expected = @"{""type"":""webauthn.create"",""challenge"":""AQIDBAU"",""origin"":""https://login.microsoft.com""}";

            string clientDataJson = JsonConvert.SerializeObject(input);
            Assert.AreEqual(expected, clientDataJson);

            byte[] clientDataBinary = new ClientData(input).ClientDataRaw;
            // TODO: Test the binary value
        }

        [TestMethod]
        public void CollectedClientData_Deserialize_Vector1()
        {
            string clientDataJson = @"{""challenge"":""FsxBWwUb1jOFRA3ILdkCsPdCZkzohvd3JrCNeDqWpJQ"",""origin"":""http://localhost:8080"",""type"":""webauthn.create""}";
            var clientData = JsonConvert.DeserializeObject<CollectedClientData>(clientDataJson);

            Assert.AreEqual("http://localhost:8080", clientData.Origin);
            Assert.AreEqual("webauthn.create", clientData.Type);
            Assert.IsNull(clientData.CrossOrigin);
            Assert.IsNull(clientData.HashAlgorithm);
            CollectionAssert.AreEqual(Convert.FromBase64String("FsxBWwUb1jOFRA3ILdkCsPdCZkzohvd3JrCNeDqWpJQ="), clientData.Challenge);
        }

        [TestMethod]
        public void CollectedClientData_Deserialize_Vector2()
        {
            string clientDataJson = @"
{
  ""challenge"": ""qNqrdXUrk5S7dCM1MAYH3qSVDXznb-6prQoGqiACR10"",
  ""origin"": ""https://demo.yubico.com"",
  ""type"": ""webauthn.create""
}";
            var clientData = JsonConvert.DeserializeObject<CollectedClientData>(clientDataJson);
            Assert.AreEqual("https://demo.yubico.com", clientData.Origin);
            Assert.AreEqual(null, clientData.CrossOrigin);
            Assert.AreEqual("webauthn.create", clientData.Type);
            CollectionAssert.AreEqual(Convert.FromBase64String("qNqrdXUrk5S7dCM1MAYH3qSVDXznb+6prQoGqiACR10="), clientData.Challenge);
        }
    }
}
