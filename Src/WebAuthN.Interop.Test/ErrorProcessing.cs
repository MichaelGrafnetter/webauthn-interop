using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAuthN.Interop.Test
{
    [TestClass]
    public class ErrorProcessing
    {
        [TestMethod]
        public void NativeMethods_WebAuthNGetErrorName()
        {
            /*
             *         // Returns the following Error Names:
        //  L"Success"              - S_OK
        //  L"InvalidStateError"    - NTE_EXISTS
        //  L"ConstraintError"      - HRESULT_FROM_WIN32(ERROR_NOT_SUPPORTED),
        //                            NTE_NOT_SUPPORTED,
        //                            NTE_TOKEN_KEYSET_STORAGE_FULL
        //  L"NotSupportedError"    - NTE_INVALID_PARAMETER
        //  L"NotAllowedError"      - NTE_DEVICE_NOT_FOUND,
        //                            NTE_NOT_FOUND,
        //                            HRESULT_FROM_WIN32(ERROR_CANCELLED),
        //                            NTE_USER_CANCELLED,
        //                            HRESULT_FROM_WIN32(ERROR_TIMEOUT)
        //  L"UnknownError"         - All other hr values
            */
            bool helloAvailable = NativeMethods.;
            PrivateObject objToTestPrivateMethod = new PrivateObject(typeof(Salary));

            bool result = Convert.ToBoolean(objToTestPrivateMethod.Invoke("isValidNwd", 6));

            Assert.AreEqual(result, true);
        }

        private void Call_WebAuthNGetErrorName()
        {

        }
    }
}
