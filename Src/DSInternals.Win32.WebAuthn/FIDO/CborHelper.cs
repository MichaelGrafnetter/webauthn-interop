using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.IO;

namespace DSInternals.Win32.WebAuthn.FIDO
{
    /// <summary>
    /// Internal helper for CBOR reading and writing operations using System.Formats.Cbor.
    /// </summary>
    internal static class CborHelper
    {
        /// <summary>
        /// Reads exactly one CBOR data item from the stream and returns its raw bytes.
        /// The stream position is advanced past the item.
        /// </summary>
        internal static byte[] ReadCborItemBytes(Stream stream)
        {
            // Read all remaining bytes from the stream
            long startPosition = stream.Position;
            int remaining = (int)(stream.Length - startPosition);
            byte[] buffer = new byte[remaining];

#if NET5_0_OR_GREATER
            stream.ReadExactly(buffer);
#else
            int totalRead = 0;
            while (totalRead < remaining)
            {
                int bytesRead = stream.Read(buffer, totalRead, remaining - totalRead);
                if (bytesRead == 0)
                    throw new EndOfStreamException();
                totalRead += bytesRead;
            }
#endif

            // Use CborReader to determine the length of the first CBOR item
            var reader = new CborReader(buffer, CborConformanceMode.Lax);
            reader.SkipValue();
            int itemLength = remaining - reader.BytesRemaining;

            // Reposition the stream to just past the CBOR item
            stream.Position = startPosition + itemLength;

            // Return only the bytes for the first item
            byte[] result = new byte[itemLength];
            Buffer.BlockCopy(buffer, 0, result, 0, itemLength);
            return result;
        }

        /// <summary>
        /// Parses a CBOR map with integer keys, returning values as raw encoded CBOR bytes.
        /// </summary>
        internal static Dictionary<int, ReadOnlyMemory<byte>> ReadIntKeyedMap(ReadOnlyMemory<byte> data)
        {
            var reader = new CborReader(data, CborConformanceMode.Lax);
            int? mapLength = reader.ReadStartMap();
            int count = mapLength ?? int.MaxValue;

            var result = new Dictionary<int, ReadOnlyMemory<byte>>();

            for (int i = 0; i < count; i++)
            {
                if (reader.PeekState() == CborReaderState.EndMap)
                    break;

                int key = reader.ReadInt32();
                ReadOnlyMemory<byte> value = reader.ReadEncodedValue();
                result[key] = value;
            }

            reader.ReadEndMap();
            return result;
        }

        /// <summary>
        /// Decodes a single CBOR-encoded value as a 32-bit integer.
        /// </summary>
        internal static int DecodeInt32(ReadOnlyMemory<byte> encodedValue)
        {
            var reader = new CborReader(encodedValue, CborConformanceMode.Lax);
            return reader.ReadInt32();
        }

        /// <summary>
        /// Decodes a single CBOR-encoded value as a byte string.
        /// </summary>
        internal static byte[] DecodeByteString(ReadOnlyMemory<byte> encodedValue)
        {
            var reader = new CborReader(encodedValue, CborConformanceMode.Lax);
            return reader.ReadByteString();
        }
    }
}
