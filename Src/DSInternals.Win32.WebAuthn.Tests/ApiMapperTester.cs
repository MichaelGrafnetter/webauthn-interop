using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Tests
{
    [TestClass]
    public class ApiMapperTester
    {
        [TestMethod]
        public void ApiMapper_StructVersions()
        {
            // Check that all structure definitions are in sync with the current winauthn.h
            Assert.AreEqual(CredentialAttestationVersion.Version3, CredentialAttestationVersion.Current);
            Assert.AreEqual(AuthenticatorMakeCredentialOptionsVersion.Version3, AuthenticatorMakeCredentialOptionsVersion.Current);
            Assert.AreEqual(AuthenticatorGetAssertionOptionsVersion.Version4, AuthenticatorGetAssertionOptionsVersion.Current);

            Assert.AreEqual(1, (int)UserInformationVersion.Current);
            Assert.AreEqual(1, (int)RelyingPartyInformationVersion.Current);
            Assert.AreEqual(1, (int)CredentialVersion.Current);
            Assert.AreEqual(1, (int)CredentialExVersion.Current);
            Assert.AreEqual(1, (int)CoseCredentialParameterVersion.Current);
            Assert.AreEqual(1, (int)CommonAttestationVersion.Current);
            Assert.AreEqual(1, (int)ClientDataVersion.Current);
            Assert.AreEqual(1, (int)AssertionVersion.Current);

            // Also check the API itself
            Assert.AreEqual(ApiVersion.Version2, ApiVersion.Current);
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
