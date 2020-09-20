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
        public void Marshalling_ClientData_SizeOf()
        {
            var calculatedSize = Marshal.SizeOf<ClientData>();
            var expectedSize = 2 * sizeof(int) + 2 * Marshal.SizeOf(typeof(IntPtr));
            Assert.AreEqual(expectedSize, calculatedSize);
        }

        [TestMethod]
        public void Marshalling_CoseCredentialParameters_SizeOf()
        {
            var calculatedSize = Marshal.SizeOf<PublicKeyCredentialParameters>();
            var expectedSize = sizeof(int) + Marshal.SizeOf(typeof(IntPtr));
            Assert.AreEqual(expectedSize, calculatedSize);
        }

        [TestMethod]
        public void Marshalling_AuthenticatorMakeCredentialOptions_SizeOf()
        {
            var calculatedSize = Marshal.SizeOf<AuthenticatorMakeCredentialOptions>();
            var expectedSize = 9 * sizeof(int) + 4 * Marshal.SizeOf(typeof(IntPtr));
            Assert.AreEqual(expectedSize, calculatedSize);
        }

        [TestMethod]
        public void Marshalling_CredentialAttestation_SizeOf()
        {
            var calculatedSize = Marshal.SizeOf<CredentialAttestation>();
            var expectedSize = 8 * sizeof(int) + 7 * Marshal.SizeOf(typeof(IntPtr));
            Assert.AreEqual(expectedSize, calculatedSize);
        }

        [TestMethod]
        public void Marshalling_AuthenticatorGetAssertionOptions_SizeOf()
        {
            var calculatedSize = Marshal.SizeOf<AuthenticatorGetAssertionOptions>();
            var expectedSize = 7 * sizeof(int) + 6 * Marshal.SizeOf(typeof(IntPtr));
            Assert.AreEqual(expectedSize, calculatedSize);
        }

        [TestMethod]
        public void Marshalling_Assertion_SizeOf()
        {
            var calculatedSize = Marshal.SizeOf<Assertion>();
            var expectedSize = 6 * sizeof(int) + 5 * Marshal.SizeOf(typeof(IntPtr));
            Assert.AreEqual(expectedSize, calculatedSize);
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
