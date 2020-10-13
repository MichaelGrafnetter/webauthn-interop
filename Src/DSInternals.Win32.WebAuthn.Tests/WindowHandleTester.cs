using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class WindowHandleTester
    {
        [TestMethod]
        public void WindowHandle_MainWindow()
        {
            var handle = WindowHandle.MainWindow;
            // Tests are typically executed from command line, without any main window.
            Assert.IsFalse(handle.IsValid);
        }

        [TestMethod]
        public void WindowHandle_ForegroundWindow()
        {
            var handle = WindowHandle.ForegroundWindow;
            Assert.IsTrue(handle.IsValid);
        }
    }
}
