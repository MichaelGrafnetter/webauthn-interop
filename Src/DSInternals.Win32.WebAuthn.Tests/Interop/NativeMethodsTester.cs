using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
    public class NativeMethodsTester
    {
        [TestMethod]
        public void NativeMethods_GetForegroundWindow()
        {
            var hwnd = NativeMethods.GetForegroundWindow();
            Assert.IsTrue(hwnd.IsValid);
        }

        [TestMethod]
        public void NativeMethods_GetCancellationId()
        {
            try
            {
                HResult result = NativeMethods.GetCancellationId(out Guid cancellationId);
                Assert.AreEqual(HResult.Success, result);
                Assert.AreNotEqual(Guid.Empty, cancellationId);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_GetCancellationId_RepeatedCalls()
        {
            try
            {
                HResult result = NativeMethods.GetCancellationId(out Guid cancellationId1);
                Assert.AreEqual(HResult.Success, result);

                result = NativeMethods.GetCancellationId(out Guid cancellationId2);
                Assert.AreEqual(HResult.Success, result);

                // We are always getting a different cancellation id.
                Assert.AreNotEqual(cancellationId1, cancellationId2);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_CancelCurrentOperation_EmptyInput()
        {
            try
            {
                HResult result = NativeMethods.CancelCurrentOperation(Guid.Empty);
                Assert.AreEqual(HResult.Success, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_CancelCurrentOperation_RandomInput()
        {
            try
            {
                HResult result = NativeMethods.CancelCurrentOperation(Guid.NewGuid());
                Assert.AreEqual(HResult.Success, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_CancelCurrentOperation_CorrectInput()
        {
            try
            {
                NativeMethods.GetCancellationId(out Guid cancellationId);
                HResult result = NativeMethods.CancelCurrentOperation(cancellationId);
                Assert.AreEqual(HResult.Success, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

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
        public void NativeMethods_StructVersions()
        {
            // Check that all structure definitions are in sync with the current winauthn.h
            Assert.AreEqual(CredentialAttestationVersion.Version4, CredentialAttestationVersion.Current);
            Assert.AreEqual(AuthenticatorMakeCredentialOptionsVersion.Version5, AuthenticatorMakeCredentialOptionsVersion.Current);
            Assert.AreEqual(AuthenticatorGetAssertionOptionsVersion.Version6, AuthenticatorGetAssertionOptionsVersion.Current);
            Assert.AreEqual(CredentialDetailtVersion.Version1, CredentialDetailtVersion.Current);
            Assert.AreEqual(GetCredentialOptionsVersion.Version1, GetCredentialOptionsVersion.Current);

            Assert.AreEqual(1, (int)UserInformationVersion.Current);
            Assert.AreEqual(1, (int)RelyingPartyInformationVersion.Current);
            Assert.AreEqual(1, (int)CredentialVersion.Current);
            Assert.AreEqual(1, (int)CredentialExVersion.Current);
            Assert.AreEqual(1, (int)CoseCredentialParameterVersion.Current);
            Assert.AreEqual(1, (int)CommonAttestationVersion.Current);
            Assert.AreEqual(1, (int)ClientDataVersion.Current);
            Assert.AreEqual(1, (int)AssertionVersion.Current);

            // Also check the API itself
            Assert.AreEqual(ApiVersion.Version4, ApiVersion.Current);
        }
    }
}
