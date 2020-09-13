using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAuthN.Interop.Test
{
    [TestClass]
    public class Headless
    {
        [TestMethod]
        public void WebAuthN_ApiVersion()
        {
            var version = WebAuthN.ApiVersion;
        }

        [TestMethod]
        public void WebAuthN_IsPlatformAuthenticatorAvailable()
        {
            bool helloAvailable = WebAuthN.IsPlatformAuthenticatorAvailable;
        }
    }
}
