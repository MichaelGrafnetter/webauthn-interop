using System;
using System.Threading;
using System.Threading.Tasks;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <remarks>Private authenticator management uses explicitly owned sessions and is gated by the reported API version.</remarks>
    public partial class WebAuthnApi
    {
        /// <summary>Selects an authenticator and opens a disposable private management session.</summary>
        /// <param name="transports">Provider-selection mask validated by Windows. Zero requests default selection.</param>
        /// <param name="cancellationToken">Signals the native selection cancellation event.</param>
        /// <returns>An owned session; dispose it even when subsequent operations fail.</returns>
        /// <remarks>Undocumented ABI compatibility is not guaranteed by the public API version.</remarks>
        public static AuthenticatorManagementSession OpenAuthenticatorManagementSession(
            AuthenticatorTransport transports = AuthenticatorTransport.NoRestrictions,
            CancellationToken cancellationToken = default) =>
            AuthenticatorManagementSession.Create(transports, cancellationToken);

        /// <summary>Asynchronously selects an authenticator for private management.</summary>
        /// <param name="transports">Provider-selection mask, or zero for default selection.</param>
        /// <param name="cancellationToken">Cancels selection.</param>
        /// <returns>An owned disposable management session.</returns>
        public static Task<AuthenticatorManagementSession> OpenAuthenticatorManagementSessionAsync(
            AuthenticatorTransport transports = AuthenticatorTransport.NoRestrictions,
            CancellationToken cancellationToken = default) =>
            Task.Run(() => OpenAuthenticatorManagementSession(transports, cancellationToken), cancellationToken);

        /// <summary>Gets Windows' broker-wide supported CTAP transport mask without selecting a device.</summary>
        /// <returns>Reported bits; zero means none reported, not unrestricted selection.</returns>
        public static AuthenticatorTransport GetSupportedManagementTransports()
        {
            if (!IsAuthenticatorManagementSupported)
            {
                throw new NotSupportedException("Authenticator management is not supported on this OS.");
            }
            ApiHelper.Validate(NativeMethods.CtapGetSupportedTransports(out var transports));
            return transports;
        }

    }
}
