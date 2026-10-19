using System;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <remarks>Private CTAP management declarations are distinct from the documented platform credential APIs.</remarks>
    internal static partial class NativeMethods
    {
        /// <summary>Minimum number of Unicode code points required in a CTAP2 PIN.</summary>
        /// <seealso href="https://fidoalliance.org/specs/fido-v2.2-rd-20230321/fido-client-to-authenticator-protocol-v2.2-rd-20230321.html#pin-composition">CTAP2 PIN requirements</seealso>
        internal const int MinimumPinCodePoints = 4;

        /// <summary>Maximum UTF-8 encoded PIN length, excluding padding.</summary>
        /// <remarks>
        /// CTAP2 requires the encoded PIN to be shorter than 64 bytes, hence 64 minus one.
        /// Since every code point needs at least one UTF-8 byte, this also bounds the configurable
        /// minimum code-point count; it is not a guarantee that 63 multibyte code points will fit.
        /// </remarks>
        /// <seealso href="https://fidoalliance.org/specs/fido-v2.2-rd-20230321/fido-client-to-authenticator-protocol-v2.2-rd-20230321.html#pin-composition">CTAP2 PIN requirements</seealso>
        internal const int MaximumPinUtf8Bytes = 64 - 1;

        /// <summary>Queries the broker-wide supported CTAP transport mask.</summary>
        /// <param name="transports">Receives reported transport bits; zero means none reported.</param>
        /// <param name="transactionId">Optional transaction GUID pointer; zero lets Windows generate it.</param>
        /// <returns>The native HRESULT.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapGetSupportedTransports", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapGetSupportedTransports(out AuthenticatorTransport transports, IntPtr transactionId);

        /// <summary>Queries supported transports with a caller-supplied transaction identifier.</summary>
        /// <param name="transports">Receives reported transport bits.</param>
        /// <param name="transactionId">The transaction identifier supplied to Windows.</param>
        /// <returns>The native HRESULT.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapGetSupportedTransports", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapGetSupportedTransports(out AuthenticatorTransport transports, in Guid transactionId);

        /// <summary>Queries supported transports, optionally associating the request with a transaction.</summary>
        /// <param name="transports">Receives reported transport bits.</param>
        /// <param name="transactionId">The transaction identifier, or null to let Windows generate one.</param>
        /// <returns>The native HRESULT.</returns>
        internal static HRESULT CtapGetSupportedTransports(out AuthenticatorTransport transports, Guid? transactionId = null) =>
            transactionId is Guid id ? CtapGetSupportedTransports(out transports, in id) :
                CtapGetSupportedTransports(out transports, IntPtr.Zero);

        /// <summary>Selects an authenticator and allocates its owned management context.</summary>
        /// <param name="cancel">Borrowed cancellation event retained throughout selection.</param>
        /// <param name="transports">Provider-selection mask; zero requests Windows' default selection.</param>
        /// <param name="transactionId">Optional transaction GUID pointer; zero lets Windows generate it.</param>
        /// <param name="context">Receives an owned context that must be disposed even on failure.</param>
        /// <returns>The native HRESULT.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageSelect", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageSelect(SafeWaitHandle cancel, AuthenticatorTransport transports,
            IntPtr transactionId, out ManagementContextSafeHandle context);

        /// <summary>Selects an authenticator with a caller-supplied transaction identifier.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="transports">Provider-selection mask.</param>
        /// <param name="transactionId">The transaction identifier supplied to Windows.</param>
        /// <param name="context">Receives an owned context, including on failure.</param>
        /// <returns>The native HRESULT.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageSelect", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageSelect(SafeWaitHandle cancel, AuthenticatorTransport transports,
            in Guid transactionId, out ManagementContextSafeHandle context);

        /// <summary>Selects an authenticator, optionally associating the request with a transaction.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="transports">Provider-selection mask.</param>
        /// <param name="context">Receives an owned context, including on failure.</param>
        /// <param name="transactionId">The transaction identifier, or null to let Windows generate one.</param>
        /// <returns>The native HRESULT.</returns>
        internal static HRESULT CtapManageSelect(SafeWaitHandle cancel, AuthenticatorTransport transports,
            out ManagementContextSafeHandle context, Guid? transactionId = null) =>
            transactionId is Guid id ? CtapManageSelect(cancel, transports, in id, out context) :
                CtapManageSelect(cancel, transports, IntPtr.Zero, out context);

        /// <summary>Releases a management context and all its interior allocations.</summary>
        /// <param name="context">Raw owned pointer; do not reacquire a finalizing SafeHandle.</param>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageClose", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern void CtapManageClose(IntPtr context);

        /// <summary>Authenticates one writable PIN against the selected authenticator.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="pin">Writable caller-owned UTF-16 PIN buffer; native code can erase it.</param>
        /// <param name="status">Receives a Windows PIN diagnostic, not a CTAP status byte.</param>
        /// <param name="retries">Receives remaining attempts; zero can also mean unreported.</param>
        /// <returns>The native HRESULT, independent of PIN diagnostics.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageAuthenticatePin", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageAuthenticatePin(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            ManagementPinBuffer pin, out PinAuthenticationStatus status, out uint retries);

        /// <summary>Submits one PIN authentication attempt.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context.</param>
        /// <param name="pin">Caller-owned PIN, left unchanged and undisposed.</param>
        /// <param name="status">Receives the Windows PIN diagnostic.</param>
        /// <param name="retries">Receives remaining attempts; zero can also mean unreported.</param>
        /// <returns>The native HRESULT, independent of PIN diagnostics.</returns>
        internal static HRESULT CtapManageAuthenticatePin(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            SecureString pin, out PinAuthenticationStatus status, out uint retries)
        {
            using var buffer = new ManagementPinBuffer(pin);
            return CtapManageAuthenticatePin(cancel, context, buffer, out status, out retries);
        }

        /// <summary>Installs an initial PIN and refreshes the context.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="pin">Writable new-PIN buffer, erased by its owner after the call.</param>
        /// <param name="status">Receives the Windows PIN diagnostic.</param>
        /// <returns>The native HRESULT; refresh failure can follow successful PIN installation.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageSetPin", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageSetPin(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            ManagementPinBuffer pin, out PinAuthenticationStatus status);

        /// <summary>Installs an initial PIN.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context.</param>
        /// <param name="pin">Caller-owned new PIN, left unchanged and undisposed.</param>
        /// <param name="status">Receives the Windows PIN diagnostic.</param>
        /// <returns>The native HRESULT; failure can follow successful PIN installation.</returns>
        internal static HRESULT CtapManageSetPin(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            SecureString pin, out PinAuthenticationStatus status)
        {
            using var buffer = new ManagementPinBuffer(pin);
            return CtapManageSetPin(cancel, context, buffer, out status);
        }

        /// <summary>Changes a PIN directly and refreshes the context without a preliminary authentication call.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="currentPin">Writable current-PIN buffer.</param>
        /// <param name="newPin">Writable replacement-PIN buffer.</param>
        /// <param name="status">Receives the Windows PIN diagnostic.</param>
        /// <param name="retries">Receives remaining attempts; zero can also mean unreported.</param>
        /// <returns>The native HRESULT; failure can follow a successful PIN change.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageChangePin", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageChangePin(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            ManagementPinBuffer currentPin, ManagementPinBuffer newPin, out PinAuthenticationStatus status, out uint retries);

        /// <summary>Changes a PIN without a preliminary authentication attempt.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context.</param>
        /// <param name="currentPin">Caller-owned current PIN, left unchanged and undisposed.</param>
        /// <param name="newPin">Caller-owned replacement PIN, left unchanged and undisposed.</param>
        /// <param name="status">Receives the Windows PIN diagnostic.</param>
        /// <param name="retries">Receives remaining attempts; zero can also mean unreported.</param>
        /// <returns>The native HRESULT; failure can follow a successful PIN change.</returns>
        internal static HRESULT CtapManageChangePin(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            SecureString currentPin, SecureString newPin, out PinAuthenticationStatus status, out uint retries)
        {
            using var current = new ManagementPinBuffer(currentPin);
            using var replacement = new ManagementPinBuffer(newPin);
            return CtapManageChangePin(cancel, context, current, replacement, out status, out retries);
        }

        /// <summary>Allocates a private display-credential list for the selected authenticator.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="credentials">Receives an owned list that must be disposed even on failure.</param>
        /// <returns>The native HRESULT.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageGetDisplayCredentials", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern HRESULT CtapManageGetDisplayCredentials(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            out ManagementCredentialListSafeHandle credentials);

        /// <summary>Releases an entire private display-credential list, including all interior records.</summary>
        /// <param name="credentials">Raw owned list pointer; interior pointers must not be freed separately.</param>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageFreeDisplayCredentials", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern void CtapManageFreeDisplayCredentials(IntPtr credentials);

        /// <summary>Deletes an exact credential identifier and refreshes the selected context.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="length">Credential identifier byte count.</param>
        /// <param name="credentialId">Opaque identifier bytes, not a list index.</param>
        /// <returns>The native HRESULT; failure can follow successful deletion.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageDeleteCredential", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageDeleteCredential(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            uint length, [In] byte[] credentialId);

        /// <summary>Deletes the credential identified by the supplied opaque bytes.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context.</param>
        /// <param name="credentialId">The opaque credential identifier.</param>
        /// <returns>The native HRESULT; failure can follow successful deletion.</returns>
        internal static HRESULT CtapManageDeleteCredential(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            byte[] credentialId)
        {
            ArgumentNullException.ThrowIfNull(credentialId);
            return CtapManageDeleteCredential(cancel, context, checked((uint)credentialId.Length), credentialId);
        }

        /// <summary>Changes Always Require User Verification and refreshes the context.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="setting">Desired Always Require User Verification state; Unreported preserves the current setting.</param>
        /// <returns>The native HRESULT; failure can follow successful configuration.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageSetAlwaysUv", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageSetAlwaysUv(SafeWaitHandle cancel, ManagementContextSafeHandle context, ReportedBoolean setting);

        /// <summary>Changes whether the authenticator always requires user verification.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context.</param>
        /// <param name="enabled">The desired setting, or null to preserve its current value.</param>
        /// <returns>The native HRESULT; failure can follow successful configuration.</returns>
        internal static HRESULT CtapManageSetAlwaysUv(SafeWaitHandle cancel, ManagementContextSafeHandle context, bool? enabled) =>
            CtapManageSetAlwaysUv(cancel, context, enabled.ToReportedBoolean());

        /// <summary>Enables enterprise attestation and refreshes the context.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <returns>The native HRESULT; failure can follow successful configuration.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageEnableEnterpriseAttestation", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        internal static extern HRESULT CtapManageEnableEnterpriseAttestation(SafeWaitHandle cancel, ManagementContextSafeHandle context);

        /// <summary>Updates minimum PIN length, RP-ID policy, and/or force-change state, then refreshes the context.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="minimum">Minimum Unicode code-point count; zero omits this field.</param>
        /// <param name="rpCount">RP-ID pointer count; zero preserves the current list.</param>
        /// <param name="rpIds">Borrowed PCWSTR array, not a count-plus-pointer structure.</param>
        /// <param name="forceChange">True requests a PIN change; false omits the field and cannot clear a requirement.</param>
        /// <returns>The native HRESULT; failure can follow successful policy mutation.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageSetMinPinLength", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageSetMinPinLength(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            uint minimum, uint rpCount, IntPtr rpIds, BOOL forceChange);

        /// <summary>Updates minimum PIN length, disclosure RP IDs, and the PIN-change requirement.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context.</param>
        /// <param name="minimum">Minimum code-point count; zero preserves the current value.</param>
        /// <param name="rpIds">Additional disclosure RP IDs; empty preserves the current list.</param>
        /// <param name="forceChange">Whether to require a PIN change; false preserves the current requirement.</param>
        /// <returns>The native HRESULT; failure can follow successful policy mutation.</returns>
        internal static HRESULT CtapManageSetMinPinLength(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            uint minimum, string[] rpIds, bool forceChange)
        {
            ArgumentNullException.ThrowIfNull(rpIds);
            using var strings = new SafeStringArrayIn(rpIds);
            return CtapManageSetMinPinLength(cancel, context, minimum, checked((uint)rpIds.Length),
                strings.DangerousGetPointer(), new BOOL(forceChange));
        }

        /// <summary>Allocates the complete serialized large-blob store in COM task memory.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="length">Receives serialized byte count, including checksum.</param>
        /// <param name="buffer">Receives an owned buffer that must be disposed even on failure.</param>
        /// <returns>The native HRESULT.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageGetLargeBlobs", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageGetLargeBlobs(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            out uint length, out ManagementLargeBlobSafeHandle buffer);

        /// <summary>Reads the complete serialized large-blob store.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context.</param>
        /// <param name="data">Receives the serialized store and its checksum on success, or null on native failure.</param>
        /// <returns>The native HRESULT.</returns>
        internal static HRESULT CtapManageGetLargeBlobs(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            out byte[]? data)
        {
            data = null;
            ManagementLargeBlobSafeHandle? buffer = null;
            try
            {
                HRESULT result = CtapManageGetLargeBlobs(cancel, context, out uint length, out buffer);
                if (result.Value >= 0)
                {
                    data = buffer.ReadBytes(length);
                }
                return result;
            }
            finally
            {
                buffer?.Dispose();
            }
        }

        /// <summary>Replaces the entire serialized large-blob store; Windows handles fragmentation.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context, retained and serialized by the caller.</param>
        /// <param name="length">Nonzero serialized byte count, including checksum.</param>
        /// <param name="data">Complete canonical serialized store and its trailing checksum.</param>
        /// <returns>The native HRESULT; failure can follow a partial or complete replacement.</returns>
        [DllImport(WebAuthn, EntryPoint = "WebAuthNCtapManageSetLargeBlobs", ExactSpelling = true)]
        [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
        private static extern HRESULT CtapManageSetLargeBlobs(SafeWaitHandle cancel, ManagementContextSafeHandle context,
            uint length, [In] byte[] data);

        /// <summary>Replaces the complete serialized large-blob store.</summary>
        /// <param name="cancel">Borrowed cancellation event.</param>
        /// <param name="context">Selected context.</param>
        /// <param name="data">The serialized store and its checksum.</param>
        /// <returns>The native HRESULT; failure can follow a partial or complete replacement.</returns>
        internal static HRESULT CtapManageSetLargeBlobs(SafeWaitHandle cancel, ManagementContextSafeHandle context, byte[] data)
        {
            ArgumentNullException.ThrowIfNull(data);
            return CtapManageSetLargeBlobs(cancel, context, checked((uint)data.Length), data);
        }
    }
}
