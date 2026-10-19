using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Interop.Tests
{
    /// <summary>Verifies session snapshots and pre-dispatch checks without selecting or modifying an authenticator.</summary>
    [TestClass]
    public class ManagementSessionTester
    {
        /// <summary>Copies independent snapshots and releases a synthetic context exactly once.</summary>
        [TestMethod]
        public void GetCapabilities_CopiesContextAndDisposeReleasesItOnce()
        {
            IntPtr memory = Marshal.AllocHGlobal(Marshal.SizeOf<ManagementContextV2>());
            Marshal.StructureToPtr(new ManagementContextV2 { Version = 2, PinState = AuthenticatorPinState.Set }, memory, false);
            using var context = new ManagementTestContextSafeHandle(memory);
            var session = new AuthenticatorManagementSession(context);
            var snapshot = session.GetCapabilities();
            session.Dispose();
            session.Dispose();
            Assert.AreEqual(1, context.ReleaseCount);
            Assert.AreEqual(AuthenticatorPinState.Set, snapshot.PinState);
            Assert.ThrowsExactly<ObjectDisposedException>(() => session.GetCapabilities());
        }

        /// <summary>Exposes the PIN diagnostic and retry count from both authentication entry points.</summary>
        [TestMethod]
        public void AuthenticatePin_ReturnsDiagnosticsTuple()
        {
            var authenticate = typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.AuthenticatePin));
            var authenticateAsync = typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.AuthenticatePinAsync));
            Assert.IsNotNull(authenticate);
            Assert.IsNotNull(authenticateAsync);
            Assert.AreEqual(typeof((PinAuthenticationStatus, uint)), authenticate.ReturnType);
            Assert.AreEqual(typeof(System.Threading.Tasks.Task<(PinAuthenticationStatus, uint)>), authenticateAsync.ReturnType);
        }

        /// <summary>Exposes the PIN diagnostic from both initial PIN installation entry points.</summary>
        [TestMethod]
        public void SetPin_ReturnsDiagnosticStatus()
        {
            var setPin = typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.SetPin));
            var setPinAsync = typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.SetPinAsync));
            Assert.IsNotNull(setPin);
            Assert.IsNotNull(setPinAsync);
            Assert.AreEqual(typeof(PinAuthenticationStatus), setPin.ReturnType);
            Assert.AreEqual(typeof(System.Threading.Tasks.Task<PinAuthenticationStatus>), setPinAsync.ReturnType);
        }

        /// <summary>Exposes PIN diagnostics and retries from both PIN change entry points.</summary>
        [TestMethod]
        public void ChangePin_ReturnsDiagnosticsTuple()
        {
            var changePin = typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.ChangePin));
            var changePinAsync = typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.ChangePinAsync));
            Assert.IsNotNull(changePin);
            Assert.IsNotNull(changePinAsync);
            Assert.AreEqual(typeof((PinAuthenticationStatus, uint)), changePin.ReturnType);
            Assert.AreEqual(typeof(System.Threading.Tasks.Task<(PinAuthenticationStatus, uint)>), changePinAsync.ReturnType);
        }

        /// <summary>Rejects unknown context layouts without invoking a native operation.</summary>
        [TestMethod]
        public void GetCapabilities_RejectsUnknownLayout()
        {
            using var session = CreateSession(version: 3);
            Assert.ThrowsExactly<NotSupportedException>(() => session.GetCapabilities());
        }

        /// <summary>Rejects cancellation before session operations or authenticator selection dispatch.</summary>
        [TestMethod]
        public void Cancellation_PreventsNativeDispatch()
        {
            using var session = CreateSession();
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            Assert.ThrowsExactly<OperationCanceledException>(() => session.GetCredentials(cancellation.Token));
            Assert.ThrowsExactly<OperationCanceledException>(() => session.GetLargeBlobs(cancellation.Token));
            Assert.ThrowsExactly<OperationCanceledException>(() =>
                WebAuthnApi.OpenAuthenticatorManagementSession(cancellationToken: cancellation.Token));
            Assert.ThrowsExactly<OperationCanceledException>(() =>
                AuthenticatorManagementSession.Create(AuthenticatorTransport.NoRestrictions, cancellation.Token));
        }

        /// <summary>Rejects unsafe PINs before native calls and preserves caller-owned secure strings.</summary>
        [TestMethod]
        public void PinValidation_RejectsInvalidInputsBeforeDispatch()
        {
            using var session = CreateSession();
            using var nul = Secure("ab\0cd");
            using var belowDeviceMinimum = Secure("1234");
            using var malformed = Secure("\uD800");
            Assert.ThrowsExactly<ArgumentException>(() => session.AuthenticatePin(nul));
            Assert.ThrowsExactly<ArgumentException>(() => session.SetPin(malformed));
            Assert.ThrowsExactly<ArgumentException>(() => session.ChangePin(belowDeviceMinimum, malformed));
            Assert.AreEqual(4, belowDeviceMinimum.Length);
        }

        /// <summary>Rejects an empty credential ID before native deletion.</summary>
        [TestMethod]
        public void DeleteCredential_RejectsEmptyId()
        {
            using var session = CreateSession();
            var error = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => session.DeleteCredential(Array.Empty<byte>()));
            Assert.AreEqual("credentialId", error.ParamName);
        }

        /// <summary>Checks private PIN-wrapper validation before any P/Invoke can be dispatched.</summary>
        [TestMethod]
        public void NativePinWrappers_RejectInvalidInputsBeforeDispatch()
        {
            using var cancellation = new EventWaitHandle(false, EventResetMode.ManualReset);
            using var context = new ManagementContextSafeHandle();
            using var malformed = Secure("\uD800");
            using var current = Secure("1234");
            Assert.ThrowsExactly<ArgumentException>(() =>
                NativeMethods.CtapManageAuthenticatePin(cancellation.SafeWaitHandle, context, malformed, out _, out _));
            Assert.ThrowsExactly<ArgumentException>(() =>
                NativeMethods.CtapManageSetPin(cancellation.SafeWaitHandle, context, malformed, out _));
            Assert.ThrowsExactly<ArgumentException>(() =>
                NativeMethods.CtapManageChangePin(cancellation.SafeWaitHandle, context, current, malformed, out _, out _));
            Assert.AreEqual(4, current.Length);
        }

        /// <summary>Leaves CTAP PIN length policy to Windows while preserving caller-owned PINs.</summary>
        /// <param name="length">The number of two-byte UTF-8 characters in the PIN.</param>
        [TestMethod]
        [DataRow(0)]
        [DataRow(3)]
        [DataRow(4)]
        [DataRow(32)]
        [DataRow(255)]
        [DataRow(256)]
        [DataRow(1024)]
        public void PinBuffer_DoesNotEnforceCtapPolicy(int length)
        {
            string text = new string('\u00E9', length);
            using var pin = Secure(text);
            using (var buffer = new ManagementPinBuffer(pin))
            {
                Assert.AreEqual(text, Marshal.PtrToStringUni(buffer.DangerousGetHandle()));
            }
            Assert.AreEqual(text.Length, pin.Length);
        }

        /// <summary>Preserves surrogate pairs and rejects malformed Unicode.</summary>
        [TestMethod]
        public void PinBuffer_ValidatesUnicode()
        {
            using var valid = Secure("a\U0001F600b");
            using (var buffer = new ManagementPinBuffer(valid))
            {
                Assert.AreEqual("a\U0001F600b", Marshal.PtrToStringUni(buffer.DangerousGetHandle()));
            }
            foreach (string text in new[] { "ab\0cd", "\uDC00", "\uD800", "\uD800a" })
            {
                using var pin = Secure(text);
                Assert.ThrowsExactly<ArgumentException>(() => new ManagementPinBuffer(pin));
            }
        }

        /// <summary>Checks policy validation, configuration cancellation, and return types without native dispatch.</summary>
        [TestMethod]
        public void Configuration_ValidatesArgumentsCancellationAndReturnTypes()
        {
            using var session = CreateSession();
            Assert.ThrowsExactly<ArgumentException>(() => session.SetPinPolicy());
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            Assert.ThrowsExactly<OperationCanceledException>(() => session.SetPinPolicy(forcePinChange: true, cancellationToken: cancellation.Token));
            Assert.ThrowsExactly<OperationCanceledException>(() => session.SetAlwaysUv(true, cancellation.Token));
            Assert.AreEqual(typeof(void), typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.SetPinPolicy))!.ReturnType);
            Assert.AreEqual(typeof(System.Threading.Tasks.Task), typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.SetPinPolicyAsync))!.ReturnType);
            Assert.AreEqual(typeof(void), typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.SetAlwaysUv))!.ReturnType);
            Assert.AreEqual(typeof(System.Threading.Tasks.Task), typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.SetAlwaysUvAsync))!.ReturnType);
            Assert.ThrowsExactly<OperationCanceledException>(() => session.EnableEnterpriseAttestation(cancellation.Token));
            Assert.AreEqual(typeof(void), typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.EnableEnterpriseAttestation))!.ReturnType);
            Assert.AreEqual(typeof(System.Threading.Tasks.Task), typeof(AuthenticatorManagementSession).GetMethod(nameof(AuthenticatorManagementSession.EnableEnterpriseAttestationAsync))!.ReturnType);
        }

        /// <summary>Rejects explicitly supplied minimum lengths outside CTAP limits before dispatch.</summary>
        /// <param name="minimum">The invalid minimum length.</param>
        [TestMethod]
        [DataRow(0u)]
        [DataRow(1u)]
        [DataRow(3u)]
        [DataRow(64u)]
        [DataRow(uint.MaxValue)]
        public void SetPinPolicy_RejectsOutOfRangeMinimum(uint minimum)
        {
            using var session = CreateSession();
            var error = Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => session.SetPinPolicy(minimum));
            Assert.AreEqual("minimumPinLength", error.ParamName);
        }

        /// <summary>Accepts both CTAP boundaries without dispatch when the operation is already canceled.</summary>
        /// <param name="minimum">A valid minimum length at either CTAP boundary.</param>
        [TestMethod]
        [DataRow(4u)]
        [DataRow(63u)]
        public void SetPinPolicy_AcceptsCtapBoundaries(uint minimum)
        {
            using var session = CreateSession();
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            Assert.ThrowsExactly<OperationCanceledException>(() =>
                session.SetPinPolicy(minimum, cancellationToken: cancellation.Token));
        }

        /// <summary>Leaves duplicate RP-ID validation to Windows.</summary>
        [TestMethod]
        public void SetPinPolicy_DoesNotPrevalidateDuplicateRpIds()
        {
            using var session = CreateSession();
            using var cancellation = new CancellationTokenSource();
            cancellation.Cancel();
            Assert.ThrowsExactly<OperationCanceledException>(() =>
                session.SetPinPolicy(relyingPartyIds: new[] { "one", "one" }, cancellationToken: cancellation.Token));
        }

        /// <summary>Accepts stores above the former local size limit and rejects unavailable storage before native dispatch.</summary>
        [TestMethod]
        public void LargeBlobs_RejectUnsupportedStorageBeforeDispatch()
        {
            byte[] store = { 0x80, 0x76, 0xBE, 0x8B, 0x52, 0x8D, 0x00, 0x75, 0xF7,
                0xAA, 0xE9, 0x8D, 0x6F, 0xA5, 0x7A, 0x6D, 0x3C };
            using (var session = CreateSession())
            {
                using var cancellation = new CancellationTokenSource();
                cancellation.Cancel();
                Assert.ThrowsExactly<OperationCanceledException>(() =>
                    session.SetLargeBlobs(new byte[16 * 1024 * 1024 + 1], cancellation.Token));
            }
            using (var session = CreateSession(blobCapacity: 16))
            {
                using var cancellation = new CancellationTokenSource();
                cancellation.Cancel();
                Assert.ThrowsExactly<OperationCanceledException>(() => session.SetLargeBlobs(new byte[] { 0 }, cancellation.Token));
            }
            using (var session = CreateSession(blobCapacity: 0))
            {
                Assert.ThrowsExactly<NotSupportedException>(() => session.SetLargeBlobs(store));
            }
        }

        /// <summary>Uses the shared HRESULT mapping without management-specific exception metadata.</summary>
        /// <param name="result">The native failure to validate.</param>
        /// <param name="expectedType">The exception category supplied by the shared HRESULT mapper.</param>
        [TestMethod]
        [DataRow(unchecked((int)0x800704C7), typeof(OperationCanceledException))]
        [DataRow(unchecked((int)0x800705B4), typeof(TimeoutException))]
        [DataRow(unchecked((int)0x80070032), typeof(NotSupportedException))]
        [DataRow(unchecked((int)0x8007000D), typeof(ArgumentException))]
        [DataRow(unchecked((int)0x8001011B), typeof(UnauthorizedAccessException))]
        [DataRow(unchecked((int)0x80004005), typeof(Win32Exception))]
        public void ApiHelperValidate_MapsNativeErrorsWithoutManagementMetadata(int result, Type expectedType)
        {
            Exception? error = null;
            try
            {
                ApiHelper.Validate(new HRESULT(result));
            }
            catch (Exception ex)
            {
                error = ex;
            }
            Assert.IsNotNull(error);
            Assert.AreEqual(expectedType, error.GetType());
            Assert.AreEqual(0, error.Data.Count);
            var native = error.GetBaseException() as Win32Exception;
            Assert.IsNotNull(native);
            Assert.AreEqual(result, native.NativeErrorCode);
        }

        /// <summary>Accepts a successful HRESULT.</summary>
        [TestMethod]
        public void ApiHelperValidate_Success()
        {
            ApiHelper.Validate(HRESULT.S_OK);
        }

        /// <summary>Checks that management support follows the reported public API version.</summary>
        [TestMethod]
        public void IsAuthenticatorManagementSupported_UsesApi9Gate()
        {
            Assert.AreEqual(WebAuthnApi.ApiVersion == ApiVersion.Version9, WebAuthnApi.IsAuthenticatorManagementSupported);
        }

        /// <summary>Checks that management entry points do not expose a version-check bypass.</summary>
        [TestMethod]
        public void ManagementEntryPoints_ExposeNoVersionBypass()
        {
            var open = typeof(WebAuthnApi).GetMethod(nameof(WebAuthnApi.OpenAuthenticatorManagementSession));
            var openAsync = typeof(WebAuthnApi).GetMethod(nameof(WebAuthnApi.OpenAuthenticatorManagementSessionAsync));
            var transports = typeof(WebAuthnApi).GetMethod(nameof(WebAuthnApi.GetSupportedManagementTransports));
            Assert.IsNotNull(open);
            Assert.IsNotNull(openAsync);
            Assert.IsNotNull(transports);
            Assert.AreEqual(2, open.GetParameters().Length);
            Assert.AreEqual(typeof(CancellationToken), open.GetParameters()[1].ParameterType);
            Assert.AreEqual(2, openAsync.GetParameters().Length);
            Assert.AreEqual(typeof(CancellationToken), openAsync.GetParameters()[1].ParameterType);
            Assert.AreEqual(0, transports.GetParameters().Length);
        }

        /// <summary>Queries broker transport support without selecting an authenticator.</summary>
        [TestMethod]
        [SuppressMessage("MSTest.Analyzers", "MSTEST0025", Justification = "Unavailable private APIs are intentionally inconclusive.")]
        public void GetSupportedManagementTransports_ReadOnlySmokeTest()
        {
            if (!WebAuthnApi.IsAuthenticatorManagementSupported)
            {
                throw new AssertInconclusiveException("Read-only private transport discovery requires WebAuthn API 9.");
            }
            try
            {
                _ = WebAuthnApi.GetSupportedManagementTransports();
                ApiHelper.Validate(
                    NativeMethods.CtapGetSupportedTransports(out _, Guid.NewGuid()));
            }
            catch (Exception ex) when (ex is PlatformNotSupportedException or NotSupportedException or Win32Exception)
            {
                throw new AssertInconclusiveException("The OS does not expose private transport discovery.", ex);
            }
        }

        /// <summary>Creates a session owning synthetic context memory; tests must not dispatch native operations with it.</summary>
        /// <param name="version">The context layout version.</param>
        /// <param name="blobCapacity">The advertised serialized-store capacity.</param>
        /// <returns>A disposable session that frees its synthetic allocation without calling Windows.</returns>
        private static AuthenticatorManagementSession CreateSession(uint version = 2, int blobCapacity = 1024)
        {
            IntPtr memory = Marshal.AllocHGlobal(Marshal.SizeOf<ManagementContextV2>());
            var context = new ManagementTestContextSafeHandle(memory);
            try
            {
                Marshal.StructureToPtr(new ManagementContextV2 { Version = version }, memory, false);
                WriteField(memory, "_authenticatorConfig", 1);
                WriteField(memory, "_setMinPinLength", 1);
                WriteField(memory, "_minimumPinLength", 8);
                WriteField(memory, "_maximumRpIds", 1);
                WriteField(memory, "_forcePinChange", 1);
                WriteField(memory, "_alwaysUv", 1);
                WriteField(memory, "_enterpriseAttestation", 1);
                WriteField(memory, "_maximumLargeBlobBytes", blobCapacity);
                return new AuthenticatorManagementSession(context);
            }
            catch
            {
                context.Dispose();
                throw;
            }
        }

        /// <summary>Populates a synthetic native context field using the platform's marshaled offset.</summary>
        /// <param name="memory">The allocated context memory.</param>
        /// <param name="field">The native backing field name.</param>
        /// <param name="value">The native value.</param>
        private static void WriteField(IntPtr memory, string field, int value) =>
            Marshal.WriteInt32(memory, checked((int)Marshal.OffsetOf<ManagementContextV2>(field)), value);

        /// <summary>Creates a caller-owned secure PIN from test characters.</summary>
        /// <param name="value">The PIN fixture text.</param>
        /// <returns>A read-only secure PIN to dispose after testing.</returns>
        private static SecureString Secure(string value)
        {
            var result = new SecureString();
            foreach (char character in value)
            {
                result.AppendChar(character);
            }
            result.MakeReadOnly();
            return result;
        }
    }
}
