using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class WebAuthnApiTester
    {
        [TestMethod]
        public void WebAuthN_ApiVersion()
        {
            try
            {
                var version = WebAuthnApi.ApiVersion;
            }
            catch(NotSupportedException ex)
            {
                throw new AssertInconclusiveException("WebAuthn API does not seem to be supported on this system.", ex);
            }
        }

        [TestMethod]
        public void WebAuthN_IsAvailable()
        {
            // Should not throw
            var version = WebAuthnApi.IsAvailable;
        }

        [TestMethod]
        public void WebAuthN_IsCredProtectExtensionSupported()
        {
            // Should not throw
            bool result = WebAuthnApi.IsCredProtectExtensionSupported;
        }


        [TestMethod]
        public void WebAuthN_IsPlatformAuthenticatorAvailable()
        {
            bool helloAvailable = WebAuthnApi.IsPlatformAuthenticatorAvailable;
        }
    }
}
