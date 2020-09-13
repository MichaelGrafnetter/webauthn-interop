using Fido2NetLib;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about credential parameter.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETER.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PublicKeyCredentialParameter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETER_CURRENT_VERSION.</remarks>
        private const int CurrentVersion = 1;

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        public int Version;

        /// <summary>
        /// Well-known credential type specifying a credential to create.
        /// </summary>
        string CredentialType;

        /// <summary>
        /// Well-known COSE algorithm specifying the algorithm to use for the credential.
        /// </summary>
        long Alg;
    }

    /// <summary>
    /// Information about credential parameters.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETERS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public struct PublicKeyCredentialParameterList
    {
        int Count;
        PublicKeyCredentialParameter[] Values;
    }
}
