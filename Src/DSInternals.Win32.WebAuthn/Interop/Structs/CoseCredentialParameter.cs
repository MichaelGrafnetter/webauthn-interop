using System.Linq;
using System.Runtime.InteropServices;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Information about credential parameter.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETER.</remarks>
    /// <see>https://www.w3.org/TR/webauthn-2/#dictionary-credential-params</see>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CoseCredentialParameter
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private CoseCredentialParameterVersion Version = CoseCredentialParameterVersion.Current;

        /// <summary>
        /// Well-known credential type specifying a credential to create.
        /// </summary>
        private string _credentialType;

        /// <summary>
        /// Well-known COSE algorithm specifying the algorithm to use for the credential.
        /// </summary>
        private Algorithm _algorithm;

        public CoseCredentialParameter(Algorithm algorithm, string credentialType = ApiConstants.CredentialTypePublicKey)
        {
            _algorithm = algorithm;
            _credentialType = credentialType;
        }
    }

    /// <summary>
    /// Information about credential parameters.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETERS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CoseCredentialParameters : SafeStructArrayIn<CoseCredentialParameter>
    {
        public CoseCredentialParameters(CoseCredentialParameter[] data) : base(data) { }

        public CoseCredentialParameters(Algorithm[] algorithms) :
            base(algorithms.Select(alg => new CoseCredentialParameter(alg)).ToArray()) { }

        public CoseCredentialParameters(Algorithm algorithm) :
            base(new [] { new CoseCredentialParameter(algorithm) }) { }
    }
}
