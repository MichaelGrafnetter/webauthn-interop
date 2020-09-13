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
        // TODO: Add enum CredentialAttestationVersion.
        /*
        #define WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_1               1
        #define WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2               2
        #define WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3               3
        #define WEBAUTHN_CREDENTIAL_ATTESTATION_CURRENT_VERSION         WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3
        */
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION_CURRENT_VERSION.</remarks>
        private const int CurrentVersion = 3;

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        uint Version;

        /// <summary>
        /// Attestation format type
        /// </summary>
        string FormatType;

        /// <summary>
        /// Size of AuthenticatorData.
        /// </summary>
        uint AuthenticatorDataLength;

        /// <summary>
        /// Authenticator data that was created for this credential.
        /// </summary>
        byte[] AuthenticatorData;

        /// <summary>
        /// Size of CBOR encoded attestation information
        /// </summary>
        /// <remarks>0 => encoded as CBOR null value.</remarks>

        uint AttestationLength;

        /// <summary>
        /// Encoded CBOR attestation information
        /// </summary>
        byte[] Attestation;

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
        IntPtr AttestationDecode;

        uint AttestationObjectLength;

        /// <summary>
        /// The CBOR encoded Attestation Object to be returned to the RP.
        /// </summary>
        byte[] AttestationObject;

        uint CredentialIdLength;
        /// <summary>
        /// The CredentialId bytes extracted from the Authenticator Data.
        /// </summary>
        /// <remarks>Used by Edge to return to the RP.</remarks>
        byte[] pbCredentialId;

        /// <summary>
        /// WebAuthn Extensions
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_2.</remarks>
        ExtensionlList Extensions;

        /// <summary>
        /// The transport that was used.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.</remarks>
        uint UsedTransport;
    }
}
