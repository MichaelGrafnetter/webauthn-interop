using System;
using System.Runtime.InteropServices;
using Fido2NetLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebAuthN.Interop.Test
{
    [TestClass]
    public class CredentialsExListTester
    {
        [TestMethod]
        public void CredentialsExList_Marshal_Vector1()
        {
            var id1 = Base64Url.Decode("lz6_hw1jzaRNhhu9dt_M1Q=");
            var id2 = Base64Url.Decode("Zod6YhgNV2dQeT3v8ekjRpU0nVlEkPlpXF5Vx6f4P9g");
            using (var managedCred1 = new CredentialExIn() { Id = id1, Transports = AuthenticatorTransport.NoRestrictions, Type = "public-key" })
            using (var managedCred2 = new CredentialExIn() { Id = id2, Transports = AuthenticatorTransport.NFC, Type = "public-key" })
            using (var managedCreds = new CredentialExListIn(new CredentialExIn[] { managedCred1, managedCred2 }))
            {
                int size = Marshal.SizeOf<CredentialExListIn>();
                IntPtr unmanagedCreds = Marshal.AllocHGlobal(size);
                try
                {
                    Marshal.StructureToPtr<CredentialExListIn>(managedCreds, unmanagedCreds, false);
                    var managedCreds2 = Marshal.PtrToStructure<CredentialExListOut>(unmanagedCreds);
                    Assert.AreEqual(2, managedCreds2.Data.Length);

                    Assert.AreEqual(managedCred1.Type, managedCreds2.Data[0].Type);
                    Assert.AreEqual(managedCred2.Type, managedCreds2.Data[1].Type);
                    Assert.AreEqual(managedCred1.Transports, managedCreds2.Data[0].Transports);
                    Assert.AreEqual(managedCred2.Transports, managedCreds2.Data[1].Transports);
                    CollectionAssert.AreEqual(id1, managedCreds2.Data[0].Id);
                    CollectionAssert.AreEqual(id2, managedCreds2.Data[1].Id);
                }
                finally
                {
                    Marshal.FreeHGlobal(unmanagedCreds);
                }
            }
        }

        [TestMethod]
        public void CredentialsExList_Marshal_Empty()
        {
            throw new AssertInconclusiveException();
        }
    }
}
