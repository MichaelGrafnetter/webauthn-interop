using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests
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
                // The API is apparently not supported on this OS.
                throw new AssertInconclusiveException();
            }
        }

        [TestMethod]
        public void WebAuthnApi_IsAvailable()
        {
            // Should not throw
            var version = WebAuthnApi.IsAvailable;
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
        public void WebAuthnApi_CancelCurrentOperation()
        {
            // Should not throw
            new WebAuthnApi().CancelCurrentOperation();
        }
    }
}
