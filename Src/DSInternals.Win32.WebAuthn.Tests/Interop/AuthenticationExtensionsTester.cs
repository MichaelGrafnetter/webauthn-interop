using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class AuthenticationExtensionsTester
    {
        [TestMethod]
        public void AuthenticationExtensionsClientInputs_Parse_Vector1()
        {
            // Input
            string jsonRequest = @"{""hmacCreateSecret"":true,""credentialProtectionPolicy"":""userVerificationOptional""}";

            // Parse
            var request = JsonSerializer.Deserialize<AuthenticationExtensionsClientInputs>(jsonRequest);
            Assert.AreEqual(true, request.HmacCreateSecret);
            Assert.AreEqual(UserVerification.Optional, request.CredProtect);
            Assert.IsFalse(request.EnforceCredProtect);

            // Serialize
            string jsonRequest2 = JsonSerializer.Serialize(request);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientInputs_Parse_Vector2()
        {
            string jsonRequest = @"{}";

            // Parse
            var request = JsonSerializer.Deserialize<AuthenticationExtensionsClientInputs>(jsonRequest);
            Assert.IsFalse(request.HmacCreateSecret);
            Assert.AreEqual(UserVerification.Any, request.CredProtect);
            Assert.IsFalse(request.EnforceCredProtect);

            // Serialize
            string jsonRequest2 = JsonSerializer.Serialize(request);
            Assert.AreEqual(jsonRequest, jsonRequest2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientOutputs_Parse_Vector1()
        {
            // Input
            string jsonResponse = @"{""hmacCreateSecret"":true,""credentialProtectionPolicy"":""userVerificationOptional""}";

            // Parse
            var response = JsonSerializer.Deserialize<AuthenticationExtensionsClientOutputs>(jsonResponse);
            Assert.AreEqual(true, response.HmacSecret);
            Assert.AreEqual(UserVerification.Optional, response.CredProtect);

            // Serialize
            string jsonRsponse2 = JsonSerializer.Serialize(response);
            Assert.AreEqual(jsonResponse, jsonRsponse2);

        }

        [TestMethod]
        public void AuthenticationExtensionsClientOutputs_Parse_Vector2()
        {
            string jsonResponse = @"{}";

            // Parse
            var response = JsonSerializer.Deserialize<AuthenticationExtensionsClientOutputs>(jsonResponse);
            Assert.IsFalse(response.HmacSecret);
            Assert.AreEqual(UserVerification.Any, response.CredProtect);

            // Serialize
            string jsonResponse2 = JsonSerializer.Serialize(response);
            Assert.AreEqual(jsonResponse, jsonResponse2);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientInputs_Setters()
        {
            var extensions = new AuthenticationExtensionsClientInputs()
            {
                HmacCreateSecret = false,
                CredProtect = UserVerification.Any
            };

            string jsonExtensions = JsonSerializer.Serialize(extensions);
            Assert.AreEqual("{}", jsonExtensions);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientOutputs_Setters()
        {
            var extensions = new AuthenticationExtensionsClientOutputs()
            {
                HmacSecret = false,
                CredProtect = UserVerification.Any
            };

            string jsonExtensions = JsonSerializer.Serialize(extensions);
            Assert.AreEqual("{}", jsonExtensions);
        }

        [TestMethod]
        public void AuthenticationExtensionsClientInputs_Conversion_Vector1()
        {
            var extensions = new AuthenticationExtensionsClientInputs()
            {
                HmacCreateSecret = true,
                CredProtect = UserVerification.Optional
            };

            using (var nativeExtensionList = ApiHelper.Translate(extensions))
            using (var nativeExtensions = new ExtensionsIn(nativeExtensionList.ToArray()))
            {
                Assert.AreEqual(2, nativeExtensionList.Count);
                var hmacSecret = nativeExtensionList.Find(extension => extension.Identifier == ApiConstants.ExtensionIdentifierHmacSecret);
                var credProtect = nativeExtensionList.Find(extension => extension.Identifier == ApiConstants.ExtensionIdentifierCredProtect);
            }
        }
    }
}
