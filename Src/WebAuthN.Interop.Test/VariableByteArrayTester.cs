using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAuthN.Interop.Test
{
    [TestClass]
    public class VariableByteArrayTester
    {
        [TestMethod]
        public void VariableByteArray_Marshal_Vector1()
        {
            byte[] input = new byte[] { 0, 1, 2, 3 , 4, 5, 6, 7, 8, 9, 10};
            using (var managed = new SafeByteArrayIn(input))
            {
                int size = Marshal.SizeOf<SafeByteArrayIn>();
                IntPtr unmanaged = Marshal.AllocHGlobal(size);
                try
                {
                    Marshal.StructureToPtr<SafeByteArrayIn>(managed, unmanaged, false);
                    var managed2 = Marshal.PtrToStructure<SafeByteArrayOut>(unmanaged);
                    CollectionAssert.AreEqual(input, managed2.Data);
                }
                finally
                {
                    Marshal.FreeHGlobal(unmanaged);
                }
            }
        }
    }
}
