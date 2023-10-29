using System.Runtime.Versioning;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
    public class WebAuthnApiTester
    {
        [TestMethod]
        public void WebAuthnApi_ApiVersion()
        {
            var version = WebAuthnApi.ApiVersion;
            if (version == null)
            {
                throw new AssertInconclusiveException("The WebAuthn API is not supported on this OS.");
            }
        }

        [TestMethod]
        public void WebAuthnApi_IsAvailable()
        {
            // Should not throw
            bool isAvailable = WebAuthnApi.IsAvailable;
        }

        [TestMethod]
        public void WebAuthnApi_IsCredProtectExtensionSupported()
        {
            // Should not throw
            bool result = WebAuthnApi.IsCredProtectExtensionSupported;
        }


        [TestMethod]
        public void WebAuthnApi_IsPlatformAuthenticatorAvailable()
        {
            // Should not throw
            bool helloAvailable = WebAuthnApi.IsUserVerifyingPlatformAuthenticatorAvailable;
        }

        [TestMethod]
        public void WebAuthnApi_IsCancellationSupported()
        {
            // Should not throw
            bool asyncSupported = new WebAuthnApi().IsCancellationSupported;
        }

        [TestMethod]
        [TestCategory("Interactive")]
        public void WebAuthnApi_Register_Vector1()
        {
            var rp = new RelyingPartyInformation()
            {
                Id = "login.microsoft.com",
                Name = "Microsoft"
            };

            var user = new UserInformation()
            {
                Name = "john.doe@outlook.com",
                DisplayName = "John Doe",
                Id = Base64UrlConverter.FromBase64UrlString("TUY65dH-Otl4jMdTRvlFQ1aApACYsuqGKSPQDQc1Bd4WVyw")
            };

            var challenge = new byte[] { 0, 1, 2, 3 };

            var api = new WebAuthnApi();

            var response = api.AuthenticatorMakeCredential(rp, user, challenge, UserVerificationRequirement.Required, AuthenticatorAttachment.Any);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        [TestCategory("Interactive")]
        public void WebAuthnApi_Authenticate_Vector1()
        {
            var api = new WebAuthnApi();
            var challenge = new byte[] { 0, 1, 2, 3 };
            var response = api.AuthenticatorGetAssertion("login.microsoft.com", challenge, UserVerificationRequirement.Required, AuthenticatorAttachment.CrossPlatform);
            Assert.IsNotNull(response);
        }
    }
}
