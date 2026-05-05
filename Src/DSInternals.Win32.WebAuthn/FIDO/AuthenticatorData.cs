using System;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Diagnostics.CodeAnalysis;
using System.Formats.Cbor;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using DSInternals.Win32.WebAuthn.Interop;
using DSInternals.Win32.WebAuthn.Cryptography;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Represents parsed authenticator data from a WebAuthn assertion.
    /// </summary>
    /// <remarks>
    /// The authenticator data structure is defined in the WebAuthn specification:
    /// https://www.w3.org/TR/webauthn/#sec-authenticator-data
    /// </remarks>
    public sealed class AuthenticatorData
    {
        /// <summary>
        /// Minimum length of the authenticator data structure.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn/#sec-authenticator-data</see>
        private static readonly int MinLength = SHA256.HashSizeInBytes + sizeof(AuthenticatorFlags) + sizeof(uint);

        /// <summary>
        /// SHA-256 hash of the RP ID the credential is scoped to.
        /// </summary>
        public required byte[] RelyingPartyIdHash { get; init; }

        /// <summary>
        /// Flags contains information from the authenticator about the authentication
        /// and whether or not certain data is present in the authenticator data.
        /// </summary>
        public required AuthenticatorFlags Flags { get; init; }

        /// <summary>
        /// UserPresent indicates that the user presence test has completed successfully.
        /// </summary>
        public bool UserPresent => Flags.HasFlag(AuthenticatorFlags.UserPresent);

        /// <summary>
        /// UserVerified indicates that the user verification process has completed successfully.
        /// </summary>
        public bool UserVerified => Flags.HasFlag(AuthenticatorFlags.UserVerified);

        /// <summary>
        /// Backup eligibility is signaled in authenticator data's flags along with the current backup state.
        /// </summary>
        public bool IsBackupEligible => Flags.HasFlag(AuthenticatorFlags.BackupEligible);

        /// <summary>
        /// The current backup state of a multi-device credential as determined by the current managing authenticator.
        /// </summary>
        public bool IsBackedUp => Flags.HasFlag(AuthenticatorFlags.BackedUp);

        /// <summary>
        /// HasAttestedCredentialData indicates that the authenticator added attested credential data to the authenticator data.
        /// </summary>
        [MemberNotNullWhen(true, nameof(AttestedCredentialData))]
        public bool HasAttestedCredentialData => Flags.HasFlag(AuthenticatorFlags.AttestationData);

        /// <summary>
        /// HasExtensionsData indicates that the authenticator added extension data to the authenticator data.
        /// </summary>
        [MemberNotNullWhen(true, nameof(Extensions))]
        public bool HasExtensionsData => Flags.HasFlag(AuthenticatorFlags.ExtensionData);

        /// <summary>
        /// Signature counter, 32-bit unsigned big-endian integer.
        /// </summary>
        public required uint SignatureCount { get; init; }

        /// <summary>
        /// Attested credential data is a variable-length byte array added to the
        /// authenticator data when generating an attestation object for a given credential.
        /// </summary>
        public AttestedCredentialData? AttestedCredentialData { get; init; }

        /// <summary>
        /// Optional extensions to suit particular use cases.
        /// </summary>
        /// <see>https://www.w3.org/TR/webauthn/#extensions</see>
        public byte[]? Extensions { get; init; }

        [SetsRequiredMembers]
        public AuthenticatorData(string relyingPartyId, AuthenticatorFlags flags, uint signatureCount, AttestedCredentialData? attestedCredentialData = null, byte[]? extensions = null)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(relyingPartyId);

            byte[] rpIdBinary = Encoding.UTF8.GetBytes(relyingPartyId);
            RelyingPartyIdHash = SHA256.HashData(rpIdBinary);
            AdjustFlags(ref flags, attestedCredentialData, extensions);
            Flags = flags;
            SignatureCount = signatureCount;
            AttestedCredentialData = attestedCredentialData;
            Extensions = extensions;
        }

        [SetsRequiredMembers]
        public AuthenticatorData(byte[] rpIdHash, AuthenticatorFlags flags, uint signatureCount, AttestedCredentialData? attestedCredentialData = null, byte[]? extensions = null)
        {
            ArgumentNullException.ThrowIfNull(rpIdHash);

            RelyingPartyIdHash = rpIdHash;
            AdjustFlags(ref flags, attestedCredentialData, extensions);
            Flags = flags;
            SignatureCount = signatureCount;
            AttestedCredentialData = attestedCredentialData;
            Extensions = extensions;
        }

        private static void AdjustFlags(ref AuthenticatorFlags flags, AttestedCredentialData? attestedCredentialData, byte[]? extensions)
        {
            // The presence of attested credential data is signaled in the flags.
            if (attestedCredentialData is not null)
            {
                flags |= AuthenticatorFlags.AttestationData;
            }
            else
            {
                flags &= ~AuthenticatorFlags.AttestationData;
            }

            // The presence of extensions data is signaled in the flags.
            if (extensions is not null)
            {
                flags |= AuthenticatorFlags.ExtensionData;
            }
            else
            {
                flags &= ~AuthenticatorFlags.ExtensionData;
            }
        }

        /// <summary>
        /// Parses authenticator data from a Base64Url-encoded string.
        /// </summary>
        public static AuthenticatorData Parse(string base64UrlData)
        {
            byte[] data = Base64Url.DecodeFromChars(base64UrlData.AsSpan());
            return Parse(data);
        }

        /// <summary>
        /// Parses authenticator data from a memory buffer.
        /// </summary>
        public static AuthenticatorData Parse(ReadOnlyMemory<byte> data)
        {
            if (data.Length < MinLength)
            {
                throw new ArgumentException($"Authenticator data must be at least {MinLength} bytes.", nameof(data));
            }

            ReadOnlySpan<byte> span = data.Span;
            int position = 0;

            // rpIdHash (32 bytes)
            byte[] rpIdHash = span.Slice(position, SHA256.HashSizeInBytes).ToArray();
            position += rpIdHash.Length;

            // flags (1 byte)
            var flags = (AuthenticatorFlags)span[position];
            position += 1;

            // signCount (4 bytes, big-endian)
            uint signCount = BinaryPrimitives.ReadUInt32BigEndian(span.Slice(position, sizeof(uint)));
            position += sizeof(uint);

            AttestedCredentialData? attestedCredentialData = null;

            // Attested credential data is only present if the AT flag is set
            if (flags.HasFlag(AuthenticatorFlags.AttestationData))
            {
                attestedCredentialData = AttestedCredentialData.Parse(data[position..], out int bytesRead);
                position += bytesRead;
            }

            byte[]? extensions = null;

            // Extensions data is only present if the ED flag is set
            if (flags.HasFlag(AuthenticatorFlags.ExtensionData))
            {
                // Read the CBOR extension data
                var reader = new CborReader(data[position..]);
                reader.SkipValue();
                int bytesRead = data.Length - position - reader.BytesRemaining;
                extensions = span.Slice(position, bytesRead).ToArray();
                position += bytesRead;
            }

            return new AuthenticatorData(rpIdHash, flags, signCount, attestedCredentialData, extensions);
        }

        public void WriteTo(Stream output)
        {
            ArgumentNullException.ThrowIfNull(output);

            // Serialize RP ID hash (SHA-256, 32 bytes)
            output.Write(RelyingPartyIdHash, 0, RelyingPartyIdHash.Length);

            // Serialize flags (1 byte)
            output.WriteByte((byte)Flags);

            // Write signature count (big-endian)
            byte[] signCountBytes = new byte[sizeof(uint)];
            BinaryPrimitives.WriteUInt32BigEndian(signCountBytes, SignatureCount);
            output.Write(signCountBytes, 0, sizeof(uint));

            // Write attested credential data if present
            AttestedCredentialData?.WriteTo(output);

            // Write extensions if present
            if (Extensions != null)
            {
                output.Write(Extensions, 0, Extensions.Length);
            }
        }

        public byte[] ToByteArray()
        {
            // TODO: Estimate the required initial capacity for the buffer to avoid unnecessary resizing. The minimum size is MinLength, but it could be larger if attested credential data or extensions are included.
            using MemoryStream buffer = new();
            WriteTo(buffer);
            return buffer.ToArray();
        }

        public static byte[] PackAttestation(byte[] authenticatorData, Algorithm algorithm, byte[] signature)
        {
            ArgumentNullException.ThrowIfNull(authenticatorData);
            ArgumentNullException.ThrowIfNull(signature);

            var writer = new CborWriter(CborConformanceMode.Lax);
            writer.WriteStartMap(3);

            writer.WriteTextString("fmt");
            writer.WriteTextString(ApiConstants.AttestationTypePacked);

            writer.WriteTextString("authData");
            writer.WriteByteString(authenticatorData);

            writer.WriteTextString("attStmt");
            writer.WriteStartMap(2);

            writer.WriteTextString("alg");
            writer.WriteInt32((int)algorithm);

            writer.WriteTextString("sig");
            writer.WriteByteString(signature);

            writer.WriteEndMap();
            writer.WriteEndMap();
            return writer.Encode();
        }
    }
}
