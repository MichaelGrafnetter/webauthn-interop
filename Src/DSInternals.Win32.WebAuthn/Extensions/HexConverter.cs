using System;
using System.Text;

namespace DSInternals.Win32.WebAuthn
{
    internal static class HexConverter
    {
        private const string HexDigitsUpper = "0123456789ABCDEF";
        private const string HexDigitsLower = "0123456789abcdef";

        public static string ToHex(this byte[] bytes, bool caps = false)
        {
            if (bytes == null)
            {
                return null;
            }

            string hexDigits = caps ? HexDigitsUpper : HexDigitsLower;

            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte currentByte in bytes)
            {
                hex.Append(hexDigits[(int)(currentByte >> 4)]);
                hex.Append(hexDigits[(int)(currentByte & 0xF)]);
            }

            return hex.ToString();
        }

        public static byte[] HexToBinary(this string hex)
        {
            // Trivial case
            if (String.IsNullOrEmpty(hex))
            {
                return null;
            }

            return hex.HexToBinary(0, hex.Length);
        }

        public static byte[] HexToBinary(this string hex, int startIndex, int length)
        {
            // Input validation
            if (hex == null)
            {
                throw new ArgumentNullException(nameof(hex));
            }

            if (length % 2 != 0)
            {
                // Each byte in a HEX string must be encoded using 2 characters.
                var exception = new ArgumentException("The input is not a hexadecimal string.", nameof(hex));
                exception.Data.Add("Value", hex);
                throw exception;
            }

            if (startIndex < 0 || startIndex >= hex.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex));
            }

            if (length < 0 || startIndex + length > hex.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }

            // Prepare the result
            byte[] bytes = new byte[length / 2];

            // Perform the conversion
            for (int nibbleIndex = 0, byteIndex = 0; nibbleIndex < length; byteIndex = ++nibbleIndex / 2)
            {
                char nibble = hex[startIndex + nibbleIndex];

                if ('0' <= nibble && nibble <= '9')
                {
                    bytes[byteIndex] = (byte)((bytes[byteIndex] << 4) | (nibble - '0'));
                }
                else if ('a' <= nibble && nibble <= 'f')
                {
                    bytes[byteIndex] = (byte)((bytes[byteIndex] << 4) | (nibble - 'a' + 0xA));
                }
                else if ('A' <= nibble && nibble <= 'F')
                {
                    bytes[byteIndex] = (byte)((bytes[byteIndex] << 4) | (nibble - 'A' + 0xA));
                }
                else
                {
                    // Invalid digit
                    var exception = new ArgumentException("The input is not a hexadecimal string.", nameof(hex));
                    exception.Data.Add("Value", hex);
                    throw exception;
                }
            }

            return bytes;
        }
    }
}
