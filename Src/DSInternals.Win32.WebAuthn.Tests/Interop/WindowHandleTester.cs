using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class WindowHandleTester
    {
        [TestMethod]
        public void WindowHandle_ForegroundWindow()
        {
            var handle = WindowHandle.ForegroundWindow;
            Assert.IsTrue(handle.IsValid);
        }

        [TestMethod]
        public void WindowHandle_IsValid()
        {
            var handle = new WindowHandle(IntPtr.Zero);
            Assert.IsFalse(handle.IsValid);
        }
    }
}
