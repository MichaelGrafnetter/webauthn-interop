using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class WinExtensionsTester
    {
        [TestMethod]
        public void WinExtensionsIn_Parse_Vector1()
        {
            // Input
            string jsonRequest = @"{""hmacCreateSecret"":true,""credentialProtectionPolicy"":""userVerificationOptional""}";

            // Parse
            var request = JsonConvert.DeserializeObject<WinExtensionsIn>(jsonRequest);
            Assert.AreEqual(true, request.HmacSecret);
            Assert.AreEqual(UserVerification.Optional, request.CredProtect);
            Assert.IsNull(request.EnforceCredProtect);

            // Serialize
            string jsonRequest2 = JsonConvert.SerializeObject(request);
            Assert.AreEqual(jsonRequest, jsonRequest2);

        }

        [TestMethod]
        public void WinExtensionsIn_Parse_Vector2()
        {
            string jsonRequest = @"{}";

            // Parse
            var request = JsonConvert.DeserializeObject<WinExtensionsIn>(jsonRequest);
            Assert.IsNull(request.HmacSecret);
            Assert.IsNull(request.CredProtect);
            Assert.IsNull(request.EnforceCredProtect);

            // Serialize
            string jsonRequest2 = JsonConvert.SerializeObject(request);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void WinExtensionsOut_Parse_Vector1()
        {
            // Input
            string jsonResponse = @"{""hmacCreateSecret"":true,""credentialProtectionPolicy"":""userVerificationOptional""}";

            // Parse
            var response = JsonConvert.DeserializeObject<WinExtensionsOut>(jsonResponse);
            Assert.AreEqual(true, response.HmacSecret);
            Assert.AreEqual(UserVerification.Optional, response.CredProtect);

            // Serialize
            string jsonRsponse2 = JsonConvert.SerializeObject(response);
            Assert.AreEqual(jsonResponse, jsonRsponse2);

        }

        [TestMethod]
        public void WinExtensionsOut_Parse_Vector2()
        {
            string jsonResponse = @"{}";

            // Parse
            var response = JsonConvert.DeserializeObject<WinExtensionsOut>(jsonResponse);
            Assert.IsNull(response.HmacSecret);
            Assert.IsNull(response.CredProtect);

            // Serialize
            string jsonResponse2 = JsonConvert.SerializeObject(response);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void WinExtensionsIn_Setters()
        {
            var extensions = new WinExtensionsIn()
            {
                HmacSecret = false,
                CredProtect = UserVerification.Any
            };

            string jsonExtensions = JsonConvert.SerializeObject(extensions);
            Assert.AreEqual("{}", jsonExtensions);
        }

        [TestMethod]
        public void WinExtensionsOut_Setters()
        {
            var extensions = new WinExtensionsOut()
            {
                HmacSecret = false,
                CredProtect = UserVerification.Any
            };

            string jsonExtensions = JsonConvert.SerializeObject(extensions);
            Assert.AreEqual("{}", jsonExtensions);
        }

        [TestMethod]
        public void WinExtensionsIn_Conversion_Vector1()
        {
            var extensions = new WinExtensionsIn()
            {
                HmacSecret = true,
                CredProtect = UserVerification.Optional
            };

            using (var nativeExtensionList = ApiMapper.Translate(extensions))
            using (var nativeExtensions = new ExtensionsIn(nativeExtensionList.ToArray()))
            {
                Assert.AreEqual(2, nativeExtensionList.Count);
                var hmacSecret = nativeExtensionList.Find(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierHmacSecret);
                var credProtect = nativeExtensionList.Find(extension => extension.Identifier == ApiConstants.ExtensionsIdentifierCredProtect);
            }
        }
    }
}
