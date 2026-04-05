using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Win32.Foundation;

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
        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Uses AssertInconclusiveException to skip on unsupported OS APIs.")]
        public void NativeMethods_GetCancellationId()
        {
            try
            {
                HRESULT result = NativeMethods.GetCancellationId(out Guid cancellationId);
                Assert.AreEqual(HRESULT.S_OK, result);
                Assert.AreNotEqual(Guid.Empty, cancellationId);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Uses AssertInconclusiveException to skip on unsupported OS APIs.")]
        public void NativeMethods_GetCancellationId_RepeatedCalls()
        {
            try
            {
                HRESULT result = NativeMethods.GetCancellationId(out Guid cancellationId1);
                Assert.AreEqual(HRESULT.S_OK, result);

                result = NativeMethods.GetCancellationId(out Guid cancellationId2);
                Assert.AreEqual(HRESULT.S_OK, result);

                // We are always getting a different cancellation id.
                Assert.AreNotEqual(cancellationId1, cancellationId2);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Uses AssertInconclusiveException to skip on unsupported OS APIs.")]
        public void NativeMethods_CancelCurrentOperation_EmptyInput()
        {
            try
            {
                HRESULT result = NativeMethods.CancelCurrentOperation(Guid.Empty);
                Assert.AreEqual(HRESULT.S_OK, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Uses AssertInconclusiveException to skip on unsupported OS APIs.")]
        public void NativeMethods_CancelCurrentOperation_RandomInput()
        {
            try
            {
                HRESULT result = NativeMethods.CancelCurrentOperation(Guid.NewGuid());
                Assert.AreEqual(HRESULT.S_OK, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Uses AssertInconclusiveException to skip on unsupported OS APIs.")]
        public void NativeMethods_CancelCurrentOperation_CorrectInput()
        {
            try
            {
                NativeMethods.GetCancellationId(out Guid cancellationId);
                HRESULT result = NativeMethods.CancelCurrentOperation(cancellationId);
                Assert.AreEqual(HRESULT.S_OK, result);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new AssertInconclusiveException("Async operations are not supported on this OS.", ex);
            }
        }

        [TestMethod]
        public void NativeMethods_GetErrorName_Success()
        {
            string result = NativeMethods.GetErrorName(HRESULT.S_OK);
            Assert.AreEqual("Success", result);
        }

        [TestMethod]
        public void NativeMethods_GetErrorName_ActionCancelled()
        {
            string result = NativeMethods.GetErrorName(HRESULT.NTE_USER_CANCELLED);
            Assert.AreEqual("NotAllowedError", result);
        }

        [TestMethod]
        public void NativeMethods_GetErrorName_KeyStorageFull()
        {
            string result = NativeMethods.GetErrorName(HRESULT.NTE_TOKEN_KEYSET_STORAGE_FULL);
            Assert.AreEqual("ConstraintError", result);
        }

        [TestMethod]
        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Compares fixed interop version constants by design.")]
        [SuppressMessage("MSTest.Analyzers", "MSTEST0032", Justification = "Compares fixed interop version constants by design.")]
        public void NativeMethods_StructVersions()
        {
            // Check that all structure definitions are in sync with the current winauthn.h
            Assert.AreEqual(CredentialAttestationVersion.Version8, CredentialAttestationVersion.Current);
            Assert.AreEqual(AuthenticatorMakeCredentialOptionsVersion.Version8, AuthenticatorMakeCredentialOptionsVersion.Current);
            Assert.AreEqual(AuthenticatorGetAssertionOptionsVersion.Version8, AuthenticatorGetAssertionOptionsVersion.Current);
            Assert.AreEqual(CredentialDetailsVersion.Version4, CredentialDetailsVersion.Current);
            Assert.AreEqual(GetCredentialOptionsVersion.Version1, GetCredentialOptionsVersion.Current);
            Assert.AreEqual(AssertionVersion.Version5, AssertionVersion.Current);
            Assert.AreEqual(HybridStorageLinkedDataVersion.Version1, HybridStorageLinkedDataVersion.Current);

            Assert.AreEqual(1, (int)UserInformationVersion.Current);
            Assert.AreEqual(1, (int)RelyingPartyInformationVersion.Current);
            Assert.AreEqual(1, (int)CredentialVersion.Current);
            Assert.AreEqual(1, (int)CredentialExVersion.Current);
            Assert.AreEqual(1, (int)CoseCredentialParameterVersion.Current);
            Assert.AreEqual(1, (int)CommonAttestationVersion.Current);
            Assert.AreEqual(1, (int)ClientDataVersion.Current);

            // Also check the API itself
            Assert.AreEqual(ApiVersion.Version9, ApiVersion.Current);
        }

        [TestMethod]
        public void NativeMethods_StructSizes()
        {
            // Check that the struct size increases with each version
            Assert.IsLessThan(Marshal.SizeOf<AssertionV2>(), Marshal.SizeOf<AssertionV1>());
            Assert.IsLessThan(Marshal.SizeOf<AssertionV3>(), Marshal.SizeOf<AssertionV2>());
            Assert.IsLessThan(Marshal.SizeOf<AssertionV4>(), Marshal.SizeOf<AssertionV3>());
            Assert.IsLessThan(Marshal.SizeOf<Assertion>(), Marshal.SizeOf<AssertionV4>());

            Assert.IsLessThan(Marshal.SizeOf<CredentialAttestationV2>(), Marshal.SizeOf<CredentialAttestationV1>());
            Assert.IsLessThan(Marshal.SizeOf<CredentialAttestationV3>(), Marshal.SizeOf<CredentialAttestationV2>());
            Assert.IsLessThan(Marshal.SizeOf<CredentialAttestationV4>(), Marshal.SizeOf<CredentialAttestationV3>());
            Assert.IsLessThan(Marshal.SizeOf<CredentialAttestationV5>(), Marshal.SizeOf<CredentialAttestationV4>());
            Assert.IsLessThan(Marshal.SizeOf<CredentialAttestation>(), Marshal.SizeOf<CredentialAttestationV5>());
        }
    }
}
