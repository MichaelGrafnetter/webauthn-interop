using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Cbor;
using System.Globalization;
using System.IO;
using DSInternals.Win32.WebAuthn;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Attested credential data is a variable-length byte array added to the authenticator
    /// data when generating an attestation object for a given credential.
    /// </summary>
    /// <see>https://www.w3.org/TR/webauthn/#sec-attested-credential-data</see>
    public sealed class AttestedCredentialData
    {
        private const int GuidLength = 16;

        /// <summary>
        /// Minimum length of the attested credential data structure.
        /// </summary>
        private const int MinLength = GuidLength + 2 + 1 + 1; // 16 (AAGUID) + 2 (credentialID length) + 1 (min credential ID) + 1 (min CBOR)

        private const int MaxCredentialIdLength = 1023;

        /// <summary>
        /// The AAGUID of the authenticator. Can be used to identify the make and model of the authenticator.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn/#aaguid</see>
        public required Guid AaGuid { get; init; }

        /// <summary>
        /// A probabilistically-unique byte sequence identifying a public key credential source and its authentication assertions.
        /// </summary>
        public required ReadOnlyMemory<byte> CredentialId { get; init; }

        /// <summary>
        /// The credential public key encoded in COSE_Key format.
        /// </summary>
        public required ReadOnlyMemory<byte> CredentialPublicKey { get; init; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttestedCredentialData"/> class.
        /// </summary>
        /// <param name="aaGuid">The AAGUID of the authenticator.</param>
        /// <param name="credentialId">A probabilistically-unique byte sequence identifying a public key credential source and its authentication assertions.</param>
        /// <param name="credentialPublicKey">The credential public key encoded in COSE_Key format.</param>
        [SetsRequiredMembers]
        public AttestedCredentialData(Guid aaGuid, ReadOnlyMemory<byte> credentialId, ReadOnlyMemory<byte> credentialPublicKey)
        {
            AaGuid = aaGuid;
            CredentialId = credentialId;
            CredentialPublicKey = credentialPublicKey;
        }

        /// <summary>
        /// Decodes attested credential data.
        /// </summary>
        internal static AttestedCredentialData Parse(ReadOnlyMemory<byte> data, out int bytesRead)
        {
            if (data.Length < MinLength)
            {
                throw new ArgumentException($"Attested credential data must be at least {MinLength} bytes.", nameof(data));
            }

            int position = 0;

            // AAGUID (16 bytes)
            var aaGuid = Guid.Create(data[..GuidLength].Span, bigEndian: true);
            position += GuidLength;

            // Credential ID length (2 bytes, big-endian)
            ushort credentialIdLength = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(position, sizeof(ushort)).Span);
            if (credentialIdLength > MaxCredentialIdLength)
            {
                throw new ArgumentException($"Credential ID length {credentialIdLength} exceeds maximum {MaxCredentialIdLength}.", nameof(data));
            }
            position += sizeof(ushort);

            // Credential ID
            ReadOnlyMemory<byte> credentialId = data.Slice(position, credentialIdLength);
            position += credentialIdLength;

            // Credential public key (CBOR encoded)
            var reader = new CborReader(data[position..]);
            reader.SkipValue();
            int publicKeyLength = data.Length - position - reader.BytesRemaining;
            ReadOnlyMemory<byte> credentialPublicKey = data.Slice(position, publicKeyLength);
            position += publicKeyLength;

            bytesRead = position;

            return new AttestedCredentialData(aaGuid, credentialId, credentialPublicKey);
        }

        public void WriteTo(Stream output)
        {
            ArgumentNullException.ThrowIfNull(output);

            // AAGUID (16 bytes, big-endian)
            byte[] aaguidBytes = AaGuid.ToByteArray(bigEndian: true);
            output.Write(aaguidBytes, 0, aaguidBytes.Length);

            // Credential ID length (2 bytes, big-endian)
            byte[] credIdLenBytes = new byte[sizeof(ushort)];
            BinaryPrimitives.WriteUInt16BigEndian(credIdLenBytes, (ushort)CredentialId.Length);
            output.Write(credIdLenBytes, 0, sizeof(ushort));

            // Credential ID
            output.Write(CredentialId.Span);

            // COSE public key (already CBOR-encoded)
            output.Write(CredentialPublicKey.Span);
        }

        public byte[] ToByteArray()
        {
            // TODO: Determine the required buffer size.
            using MemoryStream buffer = new();
            WriteTo(buffer);
            return buffer.ToArray();
        }

        /// <summary>
        /// Displays the data in a human-readable form.
        /// </summary>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "AAGUID: {0}, CredentialID: {1}, CredentialPublicKey: {2}",
                AaGuid.ToString(),
                Convert.ToHexString(CredentialId.Span),
                Convert.ToHexString(CredentialPublicKey.Span));
        }
    }
}
