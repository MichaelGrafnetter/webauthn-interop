using System;
using System.Formats.Cbor;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Parses CTAP2 authenticatorMakeCredential request CBOR to extract user information.
/// </summary>
internal static class CtapMakeCredentialRequestParser
{
    // CTAP2 authenticatorMakeCredential parameter keys
    private const int UserKey = 3;

    /// <summary>
    /// Extracts the user name and display name from a CTAP2 MakeCredential request CBOR blob.
    /// </summary>
    public static void ParseUserInfo(byte[]? request, out string? userName, out string? userDisplayName)
    {
        userName = null;
        userDisplayName = null;

        if (request == null || request.Length < 2)
            return;

        try
        {
            // Skip the first byte which is the CTAP2 command byte (0x01 for MakeCredential)
            var cborData = new ReadOnlyMemory<byte>(request, 1, request.Length - 1);
            var reader = new CborReader(cborData, CborConformanceMode.Lax);

            int? mapLength = reader.ReadStartMap();

            int count = mapLength ?? int.MaxValue;
            for (int i = 0; i < count; i++)
            {
                if (reader.PeekState() == CborReaderState.EndMap)
                    break;

                int key = reader.ReadInt32();

                if (key == UserKey)
                {
                    ReadUserMap(reader, out userName, out userDisplayName);
                    return;
                }

                reader.SkipValue();
            }
        }
        catch (Exception)
        {
            // Silently ignore malformed CBOR
        }
    }

    private static void ReadUserMap(CborReader reader, out string? userName, out string? userDisplayName)
    {
        userName = null;
        userDisplayName = null;

        int? mapLength = reader.ReadStartMap();
        int count = mapLength ?? int.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (reader.PeekState() == CborReaderState.EndMap)
                break;

            string fieldName = reader.ReadTextString();

            switch (fieldName)
            {
                case "name":
                    userName = reader.ReadTextString();
                    break;
                case "displayName":
                    userDisplayName = reader.ReadTextString();
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }
    }
}
