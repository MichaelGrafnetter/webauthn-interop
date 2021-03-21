using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class ApiHelperTester
    {
        [TestMethod]
        public void ApiHelper_Validate_Success()
        {
            // Should not throw
            ApiHelper.Validate(HResult.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(OperationCanceledException))]
        public void ApiHelper_Validate_Cancelled()
        {
            ApiHelper.Validate(HResult.ActionCancelled);
        }

        [TestMethod]
        [ExpectedException(typeof(Win32Exception))]
        public void ApiHelper_Validate_OtherError()
        {
            ApiHelper.Validate(HResult.KeyStorageFull);
        }
    }
}
