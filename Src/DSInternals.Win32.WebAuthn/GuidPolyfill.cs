using System;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Provides compatibility members for <see cref="Guid"/> APIs added in newer .NET versions.
/// </summary>
public static class GuidPolyfill
{
#if !NET8_0_OR_GREATER
    private const int GuidLength = 16;

    extension(Guid guid)
    {
        /// <summary>
        /// Returns the bytes of this <see cref="Guid"/> in little-endian or big-endian order.
        /// </summary>
        public byte[] ToByteArray(bool bigEndian)
        {
            byte[] bytes = guid.ToByteArray();

            if (bigEndian)
            {
                ConvertByteOrder(bytes);
            }

            return bytes;
        }
    }
#endif

    extension(Guid)
    {
        /// <summary>
        /// Creates a <see cref="Guid"/> from bytes in little-endian or big-endian order.
        /// </summary>
        public static Guid Create(ReadOnlySpan<byte> b, bool bigEndian)
        {
#if NET8_0_OR_GREATER
            return new Guid(b, bigEndian);
#else
            if (b.Length != GuidLength)
            {
                throw new ArgumentException("Guid byte span must be exactly 16 bytes long.", nameof(b));
            }

            byte[] bytes = b.ToArray();

            if (bigEndian)
            {
                ConvertByteOrder(bytes);
            }

            return new Guid(bytes);
#endif
        }
    }

    private static void ConvertByteOrder(byte[] bytes)
    {
        Array.Reverse(bytes, 0, sizeof(uint));
        Array.Reverse(bytes, sizeof(uint), sizeof(ushort));
        Array.Reverse(bytes, sizeof(uint) + sizeof(ushort), sizeof(ushort));
    }
}
