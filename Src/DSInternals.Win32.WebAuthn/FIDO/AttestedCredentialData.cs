using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Attested credential data is a variable-length byte array added to the authenticator 
    /// data when generating an attestation object for a given credential.
    /// </summary>
    /// <see>https://www.w3.org/TR/webauthn/#sec-attested-credential-data</see>
    public class AttestedCredentialData
    {
        /// <summary>
        /// The AAGUID of the authenticator. Can be used to identify the make and model of the authenticator.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn/#aaguid</see>
        public Guid AaGuid
        {
            get;
            private set;
        }

        /// <summary>
        /// A probabilistically-unique byte sequence identifying a public key credential source and its authentication assertions.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn/#credential-id</see>
        public byte[] CredentialId
        {
            get;
            private set;
        }

        /// <summary>
        /// The credential public key encoded in COSE_Key format, as defined in 
        /// Section 7 of RFC8152, using the CTAP2 canonical CBOR encoding form.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn/#credential-public-key</see>
        public CredentialPublicKey CredentialPublicKey
        {
            get;
            private set;
        }

        /// <summary>
        /// Decodes attested credential data.
        /// </summary>
        public AttestedCredentialData(BinaryReader reader)
        {
            if(reader == null)
            {
                throw new ArgumentNullException(nameof(reader));
            }

            // First 16 bytes is AAGUID
            byte[] aaguidBytes = reader.ReadBytes(Marshal.SizeOf(typeof(Guid)));

            // GUID from authenticator is big endian. If we are on a little endian system, convert.
            this.AaGuid = aaguidBytes.ToGuidBigEndian();

            // Byte length of Credential ID, 16-bit unsigned big-endian integer. 
            byte[] credentialIDLenBytes = reader.ReadBytes(sizeof(UInt16));

            // Credential ID length from authenticator is big endian.  If we are on little endian system, convert.
            ushort credentialIDLen = credentialIDLenBytes.ToUInt16BigEndian();

            // Read the credential ID bytes
            this.CredentialId = reader.ReadBytes(credentialIDLen);

            // "Determining attested credential data's length, which is variable, involves determining 
            // credentialPublicKey's beginning location given the preceding credentialId's length, and 
            // then determining the credentialPublicKey's length"

            // "CBORObject.Read: This method will read from the stream until the end 
            // of the CBOR object is reached or an error occurs, whichever happens first."
            
            // Read the CBOR object from the stream
            var cpk = PeterO.Cbor.CBORObject.Read(reader.BaseStream);

            // Encode the CBOR object back to a byte array.
            this.CredentialPublicKey = new CredentialPublicKey(cpk);
        }

        /// <summary>
        /// Displays the data in a human-readable form.
        /// </summary>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "AAGUID: {0}, CredentialID: {1}, CredentialPublicKey: {2}",
                AaGuid.ToString(),
                CredentialId.ToHex(true),
                CredentialPublicKey.ToString());
        }
    }
}
