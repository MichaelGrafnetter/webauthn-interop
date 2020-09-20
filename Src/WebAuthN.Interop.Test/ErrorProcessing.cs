using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAuthN.Interop.Test
{
    [TestClass]
    public class ErrorProcessing
    {
        [TestMethod]
        public void NativeMethods_GetErrorName_Success()
        {
            string result = NativeMethods.GetErrorName(HResult.Success);
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void NativeMethods_GetErrorName_NotAllowedError()
        {
            string result = NativeMethods.GetErrorName(HResult.ActionCancelled);
            Assert.AreEqual("NotAllowedError", result);
        }

        [TestMethod]
        public void HResult_GetException_Success()
        {
            Marshal.ThrowExceptionForHR((int)HResult.Success);
        }

        [TestMethod]
        [ExpectedException(typeof(COMException))]
        public void HResult_GetException_Error()
        {
            Marshal.ThrowExceptionForHR(unchecked((int)HResult.ParameterInvalid));
        }
    }
}
