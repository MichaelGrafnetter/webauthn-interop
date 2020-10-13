﻿using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about credential parameter.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_COSE_CREDENTIAL_PARAMETER.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CoseCredentialParameter
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CoseCredentialParameterVersion Version = CoseCredentialParameterVersion.Current;

        /// <summary>
        /// Well-known credential type specifying a credential to create.
        /// </summary>
        private string _credentialType = ApiConstants.CredentialTypePublicKey;

        /// <summary>
        /// Well-known COSE algorithm specifying the algorithm to use for the credential.
        /// </summary>
        private CoseAlgorithm _algorithm;

        public CoseCredentialParameter(CoseAlgorithm algorithm)
        {
            _algorithm = algorithm;
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

        public CoseCredentialParameters(CoseAlgorithm algorithm) :
            base(new CoseCredentialParameter[] { new CoseCredentialParameter(algorithm) }) { }
    }
}
