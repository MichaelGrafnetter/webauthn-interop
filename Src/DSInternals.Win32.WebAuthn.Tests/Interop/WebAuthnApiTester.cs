using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
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
    }
}
