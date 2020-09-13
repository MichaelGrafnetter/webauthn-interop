using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    // TODO: Transports enum
    /*
    #define WEBAUTHN_CTAP_TRANSPORT_USB         0x00000001
    #define WEBAUTHN_CTAP_TRANSPORT_NFC         0x00000002
    #define WEBAUTHN_CTAP_TRANSPORT_BLE         0x00000004
    #define WEBAUTHN_CTAP_TRANSPORT_TEST        0x00000008
    #define WEBAUTHN_CTAP_TRANSPORT_INTERNAL    0x00000010
    #define WEBAUTHN_CTAP_TRANSPORT_FLAGS_MASK  0x0000001F
    */

    /// <summary>
    /// Information about credential with extra information, such as, Transports.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct  CredentialEx
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX_CURRENT_VERSION.</remarks>
        private const int CurrentVersion = 1;

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        public int Version;

        /// <summary>
        /// Size of ID.
        /// </summary>
        public int IdLength;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        public byte[] Id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;

        /// <summary>
        /// Transports. 0 implies no transport restrictions.
        /// </summary>
        int Transports;
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
