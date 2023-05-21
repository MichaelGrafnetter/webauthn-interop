
/* Unmerged change from project 'DSInternals.Win32.WebAuthn (net6.0)'
Before:
using System;
After:
using System;
using DSInternals;
using DSInternals.Win32;
using DSInternals.Win32.WebAuthn;
using DSInternals.Win32.WebAuthn;
using DSInternals.Win32.WebAuthn.FIDO;
*/
using System;

namespace DSInternals.Win32.WebAuthn.Interop
{
    internal static class EndianBitConverter
    {
        public static uint ToUInt32BigEndian(this byte[] bytes, int startIndex = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt32(bytes, startIndex);
        }

        public static ushort ToUInt16BigEndian(this byte[] bytes, int startIndex = 0)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return BitConverter.ToUInt16(bytes, startIndex);
        }

        public static Guid ToGuidBigEndian(this byte[] bytes)
        {
            if (BitConverter.IsLittleEndian)
            {
                bytes.SwapBytes(0, 3);
                bytes.SwapBytes(1, 2);
                bytes.SwapBytes(4, 5);
                bytes.SwapBytes(6, 7);
            }

            return new Guid(bytes);
        }

        public static void SwapBytes(this byte[] bytes, int index1, int index2)
        {
            var temp = bytes[index1];
            bytes[index1] = bytes[index2];
            bytes[index2] = temp;
        }
    }
}
