using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Attestation Info
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    public class CredentialAttestation
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private CredentialAttestationVersion Version = CredentialAttestationVersion.Current;

        /// <summary>
        /// Attestation format type
        /// </summary>
        string FormatType;

        /// <summary>
        /// Authenticator data that was created for this credential.
        /// </summary>
        VariableByteArray AuthenticatorData;

        /// <summary>
        /// Encoded CBOR attestation information
        /// </summary>
        VariableByteArray Attestation;

        // TODO: Create enum ATTESTATION_DECODE_.
        /*
         #define WEBAUTHN_ATTESTATION_DECODE_NONE                                0
         #define WEBAUTHN_ATTESTATION_DECODE_COMMON                              1
        */
        uint dwAttestationDecodeType;
        // Following depends on the dwAttestationDecodeType
        //  WEBAUTHN_ATTESTATION_DECODE_NONE
        //      NULL - not able to decode the CBOR attestation information
        //  WEBAUTHN_ATTESTATION_DECODE_COMMON
        //      PWEBAUTHN_COMMON_ATTESTATION;
        CommonAttestation AttestationDecode;

        /// <summary>
        /// The CBOR encoded Attestation Object to be returned to the RP.
        /// </summary>
        VariableByteArray AttestationObject;

        /// <summary>
        /// The CredentialId bytes extracted from the Authenticator Data.
        /// </summary>
        /// <remarks>Used by Edge to return to the RP.</remarks>
        VariableByteArray CredentialId;

        /// <summary>
        /// WebAuthn Extensions
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.</remarks>
        Extensions Extensions;

        /// <summary>
        /// The transport that was used.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.</remarks>
        AuthenticatorTransport UsedTransport;
    }
}
