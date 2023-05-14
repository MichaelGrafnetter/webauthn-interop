using System;
using System.IO;

namespace DSInternals.Win32.WebAuthn.ActiveDirectory
{
    internal static class ByteArrayExtensions
    {
        // TODO: Remove ByteArrayExtensions.ReadToEnd from DSInternals.Common
        public static byte[] ReadToEnd(this MemoryStream stream)
        {
            long remainingBytes = stream.Length - stream.Position;
            if (remainingBytes > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(stream));
            }

            byte[] buffer = new byte[remainingBytes];
            stream.Read(buffer, 0, (int)remainingBytes);
            return buffer;
        }
    }
}
