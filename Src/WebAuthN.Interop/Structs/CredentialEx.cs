using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about credential with extra information, such as, Transports.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  CredentialEx
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialExVersion Version = CredentialExVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        public VariableByteArray Id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;

        /// <summary>
        /// Transports.
        /// </summary>
        AuthenticatorTransport Transports;
    }

    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_LIST.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct CredentialExList
    {
        int Count;
        CredentialEx[] Values;
    }
}
