using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAuthN.Interop.Test
{
    [TestClass]
    public class Marshalling
    {
        [TestMethod]
        public void Marshalling_UserInformation_Marshal()
        {
            // Init
            var info = new UserInformation();
            info.Name = "test@contoso.com";
            info.DisplayName = "Test";
            info.Id = new byte[] { 0, 1, 2, 3 };

            // Marshal
            int structSize = Marshal.SizeOf<UserInformation>();
            IntPtr nativeStruct = Marshal.AllocHGlobal(structSize);
            Marshal.StructureToPtr<UserInformation>(info, nativeStruct, false);

            // Free
            Marshal.DestroyStructure<UserInformation>(nativeStruct);
        }

        [TestMethod]
        public void Marshalling_UserInformation_SizeOf()
        {
            var calculatedSize = Marshal.SizeOf<UserInformation>();
            var actualSize = 2 * sizeof(int) + 4 * Marshal.SizeOf(typeof(IntPtr));
            Assert.AreEqual(actualSize, calculatedSize);
        }

        [TestMethod]
        public void Marshalling_BinaryArray_SizeOf()
        {
            var calculatedSize = Marshal.SizeOf<VariableByteArray>();
            var actualSize = sizeof(int) + Marshal.SizeOf(typeof(IntPtr));
            Assert.AreEqual(actualSize, calculatedSize);
        }
    }
}
