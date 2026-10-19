using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using DSInternals.Win32.WebAuthn.Interop;
using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>Provides credential, PIN, configuration, and large blob management for a selected authenticator.</summary>
    /// <remarks>
    /// Dispose this object to release the selection. Operations and disposal are serialized.
    /// Authenticate explicitly before PIN-gated operations. No operation retries PINs or reselects a device.
    /// Asynchronous management operations retain the synchronous session's serialization and event-based cancellation behavior.
    /// Caller-owned input arrays must not be modified until the operation completes, including asynchronous operations.
    /// Caller-owned PINs must not be modified or disposed until the operation completes.
    /// These undocumented APIs are not guaranteed to remain compatible with future Windows updates.
    /// </remarks>
    public sealed class AuthenticatorManagementSession : IDisposable
    {
        /// <summary>Serializes native context refreshes, snapshot copies, and disposal.</summary>
        private readonly Lock _sync = new();

        /// <summary>The selected native context owned for the entire session lifetime.</summary>
        private readonly ManagementContextSafeHandle _context;

        /// <summary>Takes ownership of a reviewed selected context without selecting or authenticating again.</summary>
        /// <param name="context">Owned context transferred from a successful selection.</param>
        internal AuthenticatorManagementSession(ManagementContextSafeHandle context)
        {
            _context = context;
        }

        /// <summary>Selects an authenticator and transfers context ownership to a session.</summary>
        /// <param name="transports">Provider-selection mask passed to Windows for validation.</param>
        /// <param name="cancellationToken">Signals the native selection cancellation event.</param>
        /// <returns>A session owning the selected context.</returns>
        public static AuthenticatorManagementSession Create(
            AuthenticatorTransport transports = AuthenticatorTransport.NoRestrictions,
            CancellationToken cancellationToken = default)
        {
            if (!WebAuthnApi.IsAuthenticatorManagementSupported)
            {
                throw new NotSupportedException("Authenticator management is not supported on this OS.");
            }
            // Retain ownership here until selection, layout validation, and the final cancellation check succeed.
            ManagementContextSafeHandle? context = null;
            try
            {
                var selectedContext = RunWithCancellation(cancel =>
                {
                    ApiHelper.Validate(NativeMethods.CtapManageSelect(cancel, transports, out context));
                    context.ToManaged(); // Reject unknown layouts before exposing a session.
                    return context;
                }, cancellationToken);
                var session = new AuthenticatorManagementSession(selectedContext);
                // Ownership now belongs to the session; prevent finally from disposing its context.
                context = null;
                return session;
            }
            finally
            {
                // Release any context returned before a selection, validation, or cancellation failure.
                context?.Dispose();
            }
        }

        /// <summary>Gets the selected authenticator's management capabilities and configuration.</summary>
        /// <param name="cancellationToken">Cancels the operation before dispatch.</param>
        /// <remarks>This copies the last native context refresh, not a fresh device query. Reopen a session after an uncertain mutation.</remarks>
        /// <returns>A pointer-free capability snapshot independent of session lifetime.</returns>
        public AuthenticatorManagementCapabilities GetCapabilities(CancellationToken cancellationToken = default) =>
            Run(_ => _context.ToManaged(), cancellationToken);

        /// <summary>Gets the selected authenticator's PIN and user-verification policy.</summary>
        /// <param name="cancellationToken">Cancels the operation before dispatch.</param>
        /// <remarks>This reads the session's context snapshot. It does not independently refresh state from the device.</remarks>
        /// <returns>The copied nullable PIN/configuration policy.</returns>
        public AuthenticatorPinPolicy GetPinPolicy(CancellationToken cancellationToken = default) =>
            GetCapabilities(cancellationToken).PinPolicy;

        /// <summary>Submits a PIN exactly once to unlock PIN-gated management operations.</summary>
        /// <remarks>Invalid-password results with a reported PIN diagnostic return that diagnostic and native retry count. Other native failures still throw.</remarks>
        /// <param name="pin">Caller-owned PIN; copied to temporary writable memory and never retained.</param>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        /// <returns>The PIN diagnostic and remaining retry count; zero retries can also mean unreported.</returns>
        public (PinAuthenticationStatus Status, uint Retries) AuthenticatePin(SecureString pin, CancellationToken cancellationToken = default) =>
            Run(cancel =>
            {
                ArgumentNullException.ThrowIfNull(pin);
                var result = NativeMethods.CtapManageAuthenticatePin(cancel, _context, pin, out var status, out uint retries);
                ValidatePinResult(result, status);
                return (status, retries);
            }, cancellationToken);

        /// <summary>Enumerates copied resident credential display records on the selected authenticator.</summary>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        /// <returns>Independent credential display records, or an empty list when none exist.</returns>
        public IReadOnlyList<DiscoverableCredentialDetails> GetCredentials(CancellationToken cancellationToken = default) =>
            Run(cancel =>
            {
                ManagementCredentialListSafeHandle? list = null;
                try
                {
                    ApiHelper.Validate(NativeMethods.CtapManageGetDisplayCredentials(cancel, _context, out list));
                    return list.ToManaged();
                }
                finally
                {
                    list?.Dispose();
                }
            }, cancellationToken);

        /// <summary>Irreversibly deletes an exact credential ID. Authenticate explicitly first.</summary>
        /// <param name="credentialId">Opaque credential ID bytes, not an enumeration index.</param>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        public void DeleteCredential(byte[] credentialId, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(credentialId);
            if (credentialId.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(credentialId), "Credential IDs must not be empty.");
            }
            Run(cancel =>
            {
                ApiHelper.Validate(NativeMethods.CtapManageDeleteCredential(cancel, _context, credentialId));
                return true;
            }, cancellationToken);
        }

        /// <summary>Installs an initial PIN. This does not replace an existing PIN.</summary>
        /// <remarks>Invalid-password results with a reported PIN diagnostic return that diagnostic. Other native failures still throw.</remarks>
        /// <param name="newPin">Caller-owned new PIN; at least four code points and at most 63 UTF-8 bytes.</param>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        /// <returns>The PIN diagnostic reported by the operation.</returns>
        public PinAuthenticationStatus SetPin(SecureString newPin, CancellationToken cancellationToken = default) =>
            Run(cancel =>
            {
                ArgumentNullException.ThrowIfNull(newPin);
                var result = NativeMethods.CtapManageSetPin(cancel, _context, newPin, out var status);
                ValidatePinResult(result, status);
                return status;
            }, cancellationToken);

        /// <summary>Changes a PIN directly without first submitting a separate authentication attempt.</summary>
        /// <remarks>Invalid-password results with a reported PIN diagnostic return that diagnostic and native retry count. Other native failures still throw.</remarks>
        /// <param name="currentPin">Caller-owned current PIN.</param>
        /// <param name="newPin">Caller-owned replacement PIN.</param>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        /// <returns>The PIN diagnostic and remaining retry count; zero retries can also mean unreported.</returns>
        public (PinAuthenticationStatus Status, uint Retries) ChangePin(SecureString currentPin, SecureString newPin, CancellationToken cancellationToken = default) =>
            Run(cancel =>
            {
                ArgumentNullException.ThrowIfNull(currentPin);
                ArgumentNullException.ThrowIfNull(newPin);
                var result = NativeMethods.CtapManageChangePin(cancel, _context, currentPin, newPin, out var status, out uint retries);
                ValidatePinResult(result, status);
                return (status, retries);
            }, cancellationToken);

        /// <summary>Preserves PIN diagnostics for invalid-password results while propagating unrelated native failures.</summary>
        /// <param name="result">Native PIN operation HRESULT.</param>
        /// <param name="status">PIN diagnostic returned alongside the HRESULT.</param>
        /// <remarks>Invalid-password results require a reported PIN diagnostic; unspecified diagnostics do not suppress failures.</remarks>
        internal static void ValidatePinResult(HRESULT result, PinAuthenticationStatus status)
        {
            if (result == Windows.Win32.PInvoke.HRESULT_FROM_WIN32(WIN32_ERROR.ERROR_INVALID_PASSWORD) &&
                status != PinAuthenticationStatus.Unspecified)
            {
                return;
            }
            ApiHelper.Validate(result);
        }

        /// <summary>Updates minimum PIN length, authorized RP IDs, and/or a future PIN-change requirement.</summary>
        /// <param name="minimumPinLength">4 to 63 Unicode code points; null preserves the existing policy.</param>
        /// <param name="forcePinChange">True requests a change; false cannot clear an existing requirement.</param>
        /// <param name="relyingPartyIds">Authorized RP IDs; null/empty preserves the existing list, not clears it.</param>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        public void SetPinPolicy(uint? minimumPinLength = null, bool forcePinChange = false,
            string[]? relyingPartyIds = null, CancellationToken cancellationToken = default)
        {
            if (minimumPinLength.HasValue &&
                (minimumPinLength < NativeMethods.MinimumPinCodePoints || minimumPinLength > NativeMethods.MaximumPinUtf8Bytes))
            {
                throw new ArgumentOutOfRangeException(nameof(minimumPinLength));
            }
            string[] rpIds = relyingPartyIds ?? [];
            if (rpIds.Any(id => string.IsNullOrWhiteSpace(id) || id.Contains('\0', StringComparison.Ordinal)))
            {
                throw new ArgumentException("RP IDs must be non-empty and contain no NUL.", nameof(relyingPartyIds));
            }
            if (!minimumPinLength.HasValue && rpIds.Length == 0 && !forcePinChange)
            {
                throw new ArgumentException("Specify a minimum PIN length, RP IDs, or force PIN change.");
            }
            Run(cancel =>
            {
                // The Windows API uses zero to preserve the current minimum PIN length.
                ApiHelper.Validate(NativeMethods.CtapManageSetMinPinLength(cancel, _context, minimumPinLength ?? 0, rpIds, forcePinChange));
                return true;
            }, cancellationToken);
        }

        /// <summary>Sets the state of Always Require User Verification.</summary>
        /// <param name="enabled">True to require user verification even when the relying party does not request it; false to disable Always Require User Verification.</param>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        public void SetAlwaysUv(bool enabled, CancellationToken cancellationToken = default) =>
            Run(cancel =>
            {
                ApiHelper.Validate(NativeMethods.CtapManageSetAlwaysUv(cancel, _context, enabled));
                return true;
            }, cancellationToken);

        /// <summary>Enables enterprise attestation; this API does not provide a disable operation.</summary>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        public void EnableEnterpriseAttestation(CancellationToken cancellationToken = default) =>
            Run(cancel =>
            {
                ApiHelper.Validate(NativeMethods.CtapManageEnableEnterpriseAttestation(cancel, _context));
                return true;
            }, cancellationToken);

        /// <summary>Copies the complete serialized large-blob store without validating its size or integrity.</summary>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        /// <returns>The complete store including its 16-byte checksum, without decryption, or null if Windows returns no data.</returns>
        public byte[]? GetLargeBlobs(CancellationToken cancellationToken = default) =>
            Run(cancel =>
            {
                ApiHelper.Validate(NativeMethods.CtapManageGetLargeBlobs(cancel, _context, out byte[]? data));
                return data;
            }, cancellationToken);

        /// <summary>Replaces the entire serialized large-blob store. Authenticate explicitly first.</summary>
        /// <param name="data">Serialized large-blob store; the caller is responsible for its schema and integrity.</param>
        /// <param name="cancellationToken">Signals native cancellation.</param>
        public void SetLargeBlobs(byte[] data, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(data);
            Run(cancel =>
            {
                ApiHelper.Validate(NativeMethods.CtapManageSetLargeBlobs(cancel, _context, data));
                return true;
            }, cancellationToken);
        }

        /// <summary>Asynchronously authenticates the PIN to unlock authenticator management operations.</summary>
        /// <remarks>Keep the caller-owned PIN alive until completion. Authentication is attempted once without automatic retries.</remarks>
        /// <param name="pin">Caller-owned PIN.</param>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task producing the PIN diagnostic and remaining retry count; zero retries can also mean unreported.</returns>
        public Task<(PinAuthenticationStatus Status, uint Retries)> AuthenticatePinAsync(SecureString pin, CancellationToken cancellationToken = default) =>
            Task.Run(() => AuthenticatePin(pin, cancellationToken), cancellationToken);

        /// <summary>Asynchronously enumerates copied discoverable credentials.</summary>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task producing independent credential display records.</returns>
        public Task<IReadOnlyList<DiscoverableCredentialDetails>> GetCredentialsAsync(CancellationToken cancellationToken = default) =>
            Task.Run(() => GetCredentials(cancellationToken), cancellationToken);

        /// <summary>Asynchronously deletes an exact credential ID, without retries.</summary>
        /// <param name="credentialId">Opaque credential ID.</param>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task completing after the native deletion attempt.</returns>
        public Task DeleteCredentialAsync(byte[] credentialId, CancellationToken cancellationToken = default) =>
            Task.Run(() => DeleteCredential(credentialId, cancellationToken), cancellationToken);

        /// <summary>Asynchronously configures an initial authenticator PIN.</summary>
        /// <remarks>Keep the caller-owned PIN alive until completion.</remarks>
        /// <param name="newPin">Caller-owned new PIN.</param>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task producing the PIN diagnostic reported by the operation.</returns>
        public Task<PinAuthenticationStatus> SetPinAsync(SecureString newPin, CancellationToken cancellationToken = default) =>
            Task.Run(() => SetPin(newPin, cancellationToken), cancellationToken);

        /// <summary>Asynchronously changes the authenticator PIN.</summary>
        /// <remarks>Keep both caller-owned PINs alive until completion. The change is attempted once without automatic retries.</remarks>
        /// <param name="currentPin">Caller-owned current PIN.</param>
        /// <param name="newPin">Caller-owned replacement PIN.</param>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task producing the PIN diagnostic and remaining retry count; zero retries can also mean unreported.</returns>
        public Task<(PinAuthenticationStatus Status, uint Retries)> ChangePinAsync(SecureString currentPin, SecureString newPin, CancellationToken cancellationToken = default) =>
            Task.Run(() => ChangePin(currentPin, newPin, cancellationToken), cancellationToken);

        /// <summary>Asynchronously updates PIN policy without retries.</summary>
        /// <param name="minimumPinLength">Null preserves the current minimum.</param>
        /// <param name="forcePinChange">Requests a PIN change without clearing an existing requirement.</param>
        /// <param name="relyingPartyIds">Null or empty preserves authorized RP IDs.</param>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task completing after the PIN policy update attempt.</returns>
        public Task SetPinPolicyAsync(uint? minimumPinLength = null, bool forcePinChange = false,
            string[]? relyingPartyIds = null, CancellationToken cancellationToken = default) =>
            Task.Run(() => SetPinPolicy(minimumPinLength, forcePinChange, relyingPartyIds, cancellationToken), cancellationToken);

        /// <summary>Asynchronously sets the state of Always Require User Verification.</summary>
        /// <param name="enabled">The desired state.</param>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task completing after the Always Require User Verification configuration attempt.</returns>
        public Task SetAlwaysUvAsync(bool enabled, CancellationToken cancellationToken = default) =>
            Task.Run(() => SetAlwaysUv(enabled, cancellationToken), cancellationToken);

        /// <summary>Asynchronously enables enterprise attestation.</summary>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task completing after the enterprise attestation configuration attempt.</returns>
        public Task EnableEnterpriseAttestationAsync(CancellationToken cancellationToken = default) =>
            Task.Run(() => EnableEnterpriseAttestation(cancellationToken), cancellationToken);

        /// <summary>Asynchronously reads the complete serialized large-blob store.</summary>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task producing the serialized store and its checksum, or null if Windows returns no data.</returns>
        public Task<byte[]?> GetLargeBlobsAsync(CancellationToken cancellationToken = default) =>
            Task.Run(() => GetLargeBlobs(cancellationToken), cancellationToken);

        /// <summary>Asynchronously replaces the complete serialized large-blob store.</summary>
        /// <param name="data">Serialized store including checksum.</param>
        /// <param name="cancellationToken">Cancels the operation.</param>
        /// <returns>A task completing after the whole-store replacement attempt.</returns>
        public Task SetLargeBlobsAsync(byte[] data, CancellationToken cancellationToken = default) =>
            Task.Run(() => SetLargeBlobs(data, cancellationToken), cancellationToken);

        /// <summary>Releases the selected native context after any in-progress operation finishes.</summary>
        public void Dispose()
        {
            lock (_sync)
            {
                _context.Dispose();
            }
        }

        /// <summary>Serializes an operation while retaining a token-signaled native cancellation event.</summary>
        /// <typeparam name="T">The operation's managed result type.</typeparam>
        /// <param name="operation">Synchronous native work using the borrowed cancellation event.</param>
        /// <param name="token">Cancellation checked before dispatch and after completion.</param>
        /// <returns>The operation's managed result when not canceled.</returns>
        private T Run<T>(Func<SafeWaitHandle, T> operation, CancellationToken token)
        {
            token.ThrowIfCancellationRequested();
            lock (_sync)
            {
                ObjectDisposedException.ThrowIf(_context.IsClosed, this);
                return RunWithCancellation(operation, token);
            }
        }

        /// <summary>Runs native work with a token-signaled cancellation event.</summary>
        /// <typeparam name="T">The operation's managed result type.</typeparam>
        /// <param name="operation">Synchronous native work using the borrowed cancellation event.</param>
        /// <param name="token">Cancellation checked before dispatch and after completion.</param>
        /// <returns>The operation's managed result when not canceled.</returns>
        private static T RunWithCancellation<T>(Func<SafeWaitHandle, T> operation, CancellationToken token)
        {
            using var cancellation = new EventWaitHandle(false, EventResetMode.ManualReset);
            using var registration = token.Register(() => cancellation.Set());
            token.ThrowIfCancellationRequested();
            T result = operation(cancellation.SafeWaitHandle);
            return result;
        }
    }
}
