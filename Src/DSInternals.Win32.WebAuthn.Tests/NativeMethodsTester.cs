using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class NativeMethodsTester
    {
        [TestMethod]
        public void NativeMethods_GetForegroundWindow()
        {
            var hwnd = NativeMethods.GetForegroundWindow();
            Assert.IsTrue(hwnd.IsValid);
        }

        [TestMethod]
        public void NativeMethods_GetCancellationId()
        {
            try
            {
                HResult result = NativeMethods.GetCancellationId(out Guid cancelationId);
                Assert.AreEqual(HResult.Success, result);
                Assert.AreNotEqual(Guid.Empty, cancelationId);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_GetCancellationId_RepeatedCalls()
        {
            try
            {
                HResult result = NativeMethods.GetCancellationId(out Guid cancelationId1);
                Assert.AreEqual(HResult.Success, result);

                result = NativeMethods.GetCancellationId(out Guid cancelationId2);
                Assert.AreEqual(HResult.Success, result);

                // We are always getting a different cancellation id.
                Assert.AreNotEqual(cancelationId1, cancelationId2);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_CancelCurrentOperation_EmptyInput()
        {
            try
            {
                HResult result = NativeMethods.CancelCurrentOperation(Guid.Empty);
                Assert.AreEqual(HResult.Success, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_CancelCurrentOperation_RandomInput()
        {
            try
            {
                HResult result = NativeMethods.CancelCurrentOperation(Guid.NewGuid());
                Assert.AreEqual(HResult.Success, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_CancelCurrentOperation_CorrectInput()
        {
            try
            {
                NativeMethods.GetCancellationId(out Guid cancellationId);
                HResult result = NativeMethods.CancelCurrentOperation(cancellationId);
                Assert.AreEqual(HResult.Success, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_GetErrorName_Success()
        {
            string result = NativeMethods.GetErrorName(HResult.Success);
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void NativeMethods_GetErrorName_ActionCancelled()
        {
            string result = NativeMethods.GetErrorName(HResult.ActionCancelled);
            Assert.AreEqual("NotAllowedError", result);
        }

        [TestMethod]
        public void NativeMethods_GetErrorName_KeyStorageFull()
        {
            string result = NativeMethods.GetErrorName(HResult.KeyStorageFull);
            Assert.AreEqual("ConstraintError", result);
        }
    }
}
