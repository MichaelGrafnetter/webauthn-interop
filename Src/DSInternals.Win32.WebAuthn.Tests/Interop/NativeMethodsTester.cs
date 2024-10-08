﻿using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    [TestClass]
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
#endif
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
            Assert.AreEqual(CredentialAttestationVersion.Version6, CredentialAttestationVersion.Current);
            Assert.AreEqual(AuthenticatorMakeCredentialOptionsVersion.Version7, AuthenticatorMakeCredentialOptionsVersion.Current);
            Assert.AreEqual(AuthenticatorGetAssertionOptionsVersion.Version7, AuthenticatorGetAssertionOptionsVersion.Current);
            Assert.AreEqual(CredentialDetailsVersion.Version2, CredentialDetailsVersion.Current);
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
            Assert.AreEqual(ApiVersion.Version7, ApiVersion.Current);
        }

        [TestMethod]
        public void NativeMethods_StructSizes()
        {
            // Check that the struct size increases with each version
            Assert.IsTrue(Marshal.SizeOf<AssertionV1>() < Marshal.SizeOf<AssertionV2>());
            Assert.IsTrue(Marshal.SizeOf<AssertionV2>() < Marshal.SizeOf<AssertionV3>());
            Assert.IsTrue(Marshal.SizeOf<AssertionV3>() < Marshal.SizeOf<AssertionV4>());
            Assert.IsTrue(Marshal.SizeOf<AssertionV4>() < Marshal.SizeOf<Assertion>());

            Assert.IsTrue(Marshal.SizeOf<CredentialAttestationV1>() < Marshal.SizeOf<CredentialAttestationV2>());
            Assert.IsTrue(Marshal.SizeOf<CredentialAttestationV2>() < Marshal.SizeOf<CredentialAttestationV3>());
            Assert.IsTrue(Marshal.SizeOf<CredentialAttestationV3>() < Marshal.SizeOf<CredentialAttestationV4>());
            Assert.IsTrue(Marshal.SizeOf<CredentialAttestationV4>() < Marshal.SizeOf<CredentialAttestationV5>());
            Assert.IsTrue(Marshal.SizeOf<CredentialAttestationV5>() < Marshal.SizeOf<CredentialAttestation>());
        }
    }
}
