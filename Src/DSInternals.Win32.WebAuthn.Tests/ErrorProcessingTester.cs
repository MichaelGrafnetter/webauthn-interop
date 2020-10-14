using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class ErrorProcessingTester
    {
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

        [TestMethod]
        public void ApiMapper_Validate_Success()
        {
            // Should not throw
            ApiMapper.Validate(HResult.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public void ApiMapper_Validate_Cancelled()
        {
            ApiMapper.Validate(HResult.ActionCancelled);
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void ApiMapper_Validate_OtherError()
        {
            ApiMapper.Validate(HResult.KeyStorageFull);
        }
    }
}
