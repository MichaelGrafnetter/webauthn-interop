using System.Net;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Assertion
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_ASSERTION.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Assertion
    {
        /// <summary>
        /// // Version of this structure, to allow for modifications in the future.
        /// </summary>
        public AssertionVersion Version { get; private set; }

        /// <summary>
        /// The size of the authenticator data.
        /// </summary>
        private int _authenticatorDataLength;

        /// <summary>
        /// A pointer to the authenticator data.
        /// </summary>
        private ByteArrayOut _authenticatorData;

        /// <summary>
        /// The size of the signature that was generated for this assertion.
        /// </summary>
        private int _signatureLength;

        /// <summary>
        /// A pointer to the signature that was generated for this assertion.
        /// </summary>
        private ByteArrayOut _signature;

        /// <summary>
        /// The credential that was used for this assertion.
        /// </summary>
        public CredentialOut Credential { get; private set; }

        /// <summary>
        /// The size of the user Id.
        /// </summary>
        private int _userIdLength;

        /// <summary>
        /// A pointer to the user Id.
        /// </summary>
        private ByteArrayOut _userId;

        /// <summary>
        /// A CBOR map from extension identifiers to their authenticator extension inputs,
        /// created by the client based on the extensions requested by the Relying Party, if any.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_2.</remarks>
        public ExtensionsOut Extensions { get; private set; }

        /// <summary>
        /// Size of the Large Blob
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_2.</remarks>
        private int _largeBlobLength;

        /// <summary>
        /// A pointer to the credential blob.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_2.</remarks>
        private ByteArrayOut _largeBlob;

        /// <summary>
        /// Status of the credential blob.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_2.</remarks>
        public CredentialLargeBlobStatus LargeBlobStatus { get; private set; }

        /// <summary>
        /// A salt used to generate the HMAC secret.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_3.</remarks>
        public HmacSecretSaltOut HmacSecret { get; private set; }

        /// <summary>
        /// The transport that was used.
        /// </summary>
        /// <remarks>This field has been added in WEBAUTHN_ASSERTION_VERSION_4.</remarks>
        public TransportContext UsedTransport { get; private set; }

        /// <summary>
        /// Authenticator data that was created for this assertion.
        /// </summary>
        public byte[] AuthenticatorData => _authenticatorData?.Read(_authenticatorDataLength);

        /// <summary>
        /// Signature that was generated for this assertion.
        /// </summary>
        public byte[] Signature => _signature?.Read(_signatureLength);

        /// <summary>
        /// User Identifier
        /// </summary>
        public byte[] UserId => _userId?.Read(_userIdLength);

        /// <summary>
        /// Credential Large Blob
        /// </summary>
        public byte[] LargeBlob => _largeBlob?.Read(_largeBlobLength);
    }
}
