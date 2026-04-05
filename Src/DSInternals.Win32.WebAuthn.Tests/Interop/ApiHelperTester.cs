using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class ApiHelperTester
    {
        [TestMethod]
        public void ApiHelper_Validate_Success()
        {
            // Should not throw
            ApiHelper.Validate(HRESULT.S_OK);
        }

        [TestMethod]
        public void ApiHelper_Validate_Cancelled()
        {
            Assert.ThrowsExactly<OperationCanceledException>(() =>
            {
                ApiHelper.Validate(HRESULT.NTE_USER_CANCELLED);
            });
        }

        [TestMethod]
        public void ApiHelper_Validate_OtherError()
        {
            Assert.ThrowsExactly<Win32Exception>(() =>
            {
                ApiHelper.Validate(HRESULT.NTE_TOKEN_KEYSET_STORAGE_FULL);
            });
        }
    }
}
