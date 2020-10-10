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
            var hwnd = NativeMethods.GetForegroundWindow();
            Assert.AreNotEqual(IntPtr.Zero, hwnd);
        }

        [TestMethod]
        public void NativeMethods_GetCancellationId()
        {
            try
            {
                HResult result = NativeMethods.GetCancellationId(out Guid cancelationId);
            }
            catch(EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }
    }
}
