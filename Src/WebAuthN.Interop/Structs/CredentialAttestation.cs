using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Attestation Info
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_ATTESTATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CredentialAttestation
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialAttestationVersion Version = CredentialAttestationVersion.Current;

        // TODO: Change FormatType to enum
        /*
        #define WEBAUTHN_ATTESTATION_TYPE_PACKED                                L"packed"
        #define WEBAUTHN_ATTESTATION_TYPE_U2F                                   L"fido-u2f"
        #define WEBAUTHN_ATTESTATION_TYPE_TPM                                   L"tpm"
        #define WEBAUTHN_ATTESTATION_TYPE_NONE                                  L"none"
        */
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

        AttestationDecode AttestationDecodeType;

        /// <summary>
        /// CBOR attestation information.
        /// </summary>
        // TODO: [MarshalAs(UnmanagedType.LPStruct)]
        // TODO: CommonAttestation AttestationDecoded;

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
        // TODO: Extensions Extensions;

        /// <summary>
        /// The transport that was used.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_CREDENTIAL_ATTESTATION_VERSION_3.</remarks>
        AuthenticatorTransport UsedTransport;
    }
}
