using System;
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

        [TestMethod]
        public void NativeMethods_GetForegroundWindow()
        {
            IntPtr hwnd = NativeMethods.GetForegroundWindow();
            Assert.AreNotEqual(IntPtr.Zero, hwnd);
        }

        [TestMethod]
        public void NativeMethods_GetCancellationId()
        {
            HResult result = NativeMethods.GetCancellationId(out Guid cancelationId);
        }
    }
}
