using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about credential.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  Credential
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialVersion Version = CredentialVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        VariableByteArray Id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;
    }
}
