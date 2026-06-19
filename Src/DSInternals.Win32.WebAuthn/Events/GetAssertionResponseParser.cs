using System;
using System.Collections.Generic;
using System.Formats.Cbor;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Parses CTAP GetAssertion CBOR response payloads.
/// </summary>
internal static class GetAssertionResponseParser
{
    private const int CredentialKey = 1;
    private const int AuthenticatorDataKey = 2;
    private const int SignatureKey = 3;
    private const int UserKey = 4;

    /// <summary>
    /// Parses a CBOR-encoded GetAssertion response.
    /// </summary>
    /// <param name="response">The CBOR-encoded response payload from the event log.</param>
    /// <returns>The decoded response fields, or <see langword="null"/> when the payload is empty or invalid.</returns>
    public static GetAssertionResponseData? Parse(byte[]? response)
    {
        if (response == null || response.Length == 0)
        {
            return null;
        }

        try
        {
            var result = new GetAssertionResponseData();
            var reader = new CborReader(response, CborConformanceMode.Lax);

            switch (reader.PeekState())
            {
                case CborReaderState.StartMap:
                    ReadResponseWrapper(reader, result);
                    break;
                case CborReaderState.StartArray:
                    result.Credentials = ReadCredentials(reader, result);
                    break;
                case CborReaderState.ByteString:
                    ReadCommandResponse(reader.ReadByteString(), result);
                    break;
                default:
                    return null;
            }

            ApplyDeviceInfo(result);
            return result;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static void ReadResponseWrapper(CborReader reader, GetAssertionResponseData result)
    {
        int? mapLength = reader.ReadStartMap();
        int count = mapLength ?? int.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (reader.PeekState() == CborReaderState.EndMap)
            {
                break;
            }

            if (reader.PeekState() != CborReaderState.TextString)
            {
                reader.SkipValue();
                reader.SkipValue();
                continue;
            }

            string key = reader.ReadTextString();

            switch (key)
            {
                case "deviceInfo":
                    ReadDeviceInfo(reader, result);
                    break;
                case "status":
                    result.Status = ReadInt32(reader);
                    break;
                case "sharedAuthenticatorData":
                    ReadSharedAuthenticatorData(reader, result);
                    break;
                case "response":
                    ReadResponseValue(reader, result);
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();
    }

    private static void ReadDeviceInfo(CborReader reader, GetAssertionResponseData result)
    {
        if (reader.PeekState() != CborReaderState.StartMap)
        {
            reader.SkipValue();
            return;
        }

        int? mapLength = reader.ReadStartMap();
        int count = mapLength ?? int.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (reader.PeekState() == CborReaderState.EndMap)
            {
                break;
            }

            if (reader.PeekState() != CborReaderState.TextString)
            {
                reader.SkipValue();
                reader.SkipValue();
                continue;
            }

            string key = reader.ReadTextString();

            switch (key)
            {
                case "providerType":
                    result.ProviderType = ReadTextString(reader);
                    break;
                case "providerName":
                    result.ProviderName = ReadTextString(reader);
                    break;
                case "manufacturer":
                    result.Manufacturer = ReadTextString(reader);
                    break;
                case "product":
                    result.Product = ReadTextString(reader);
                    break;
                case "aaGuid":
                    result.AAGuid = ReadGuid(reader);
                    break;
                case "thirdPartyPayment":
                    result.ThirdPartyPayment = ReadBoolean(reader);
                    break;
                case "transports":
                    int? transports = ReadInt32(reader);
                    result.Transports = transports.HasValue ? (AuthenticatorTransport)(uint)transports.Value : null;
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();
    }

    private static void ReadResponseValue(CborReader reader, GetAssertionResponseData result)
    {
        switch (reader.PeekState())
        {
            case CborReaderState.ByteString:
                ReadCommandResponse(reader.ReadByteString(), result);
                break;
            case CborReaderState.StartArray:
                result.Credentials = ReadCredentials(reader, result);
                break;
            case CborReaderState.StartMap:
                CredentialDetails? credential = ReadCredential(reader, result);
                result.Credentials = credential != null ? [credential] : null;
                break;
            default:
                reader.SkipValue();
                break;
        }
    }

    private static void ReadCommandResponse(byte[] commandResponse, GetAssertionResponseData result)
    {
        if (commandResponse.Length == 0)
        {
            return;
        }

        result.CtapStatus = commandResponse[0];

        if (commandResponse.Length == 1)
        {
            return;
        }

        var reader = new CborReader(commandResponse.AsSpan(1).ToArray(), CborConformanceMode.Lax);

        switch (reader.PeekState())
        {
            case CborReaderState.StartArray:
                result.Credentials = ReadCredentials(reader, result);
                break;
            case CborReaderState.StartMap:
                CredentialDetails? credential = ReadCredential(reader, result);
                result.Credentials = credential != null ? [credential] : null;
                break;
        }
    }

    private static void ReadSharedAuthenticatorData(CborReader reader, GetAssertionResponseData result)
    {
        if (reader.PeekState() == CborReaderState.ByteString)
        {
            ApplySharedAuthenticatorData(result, ReadByteString(reader));
            return;
        }

        if (reader.PeekState() != CborReaderState.StartMap)
        {
            reader.SkipValue();
            return;
        }

        int? mapLength = reader.ReadStartMap();
        int count = mapLength ?? int.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (reader.PeekState() == CborReaderState.EndMap)
            {
                break;
            }

            if (reader.PeekState() != CborReaderState.TextString)
            {
                reader.SkipValue();
                reader.SkipValue();
                continue;
            }

            string key = reader.ReadTextString();

            switch (key)
            {
                case "rpIdHash":
                    result.RpIdHash = ReadByteString(reader);
                    break;
                case "flags":
                    result.AuthenticatorFlags = ReadAuthenticatorFlags(reader);
                    break;
                case "authData":
                case "authenticatorData":
                    ApplySharedAuthenticatorData(result, ReadByteString(reader));
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();
    }

    private static IReadOnlyList<CredentialDetails> ReadCredentials(CborReader reader, GetAssertionResponseData result)
    {
        var credentials = new List<CredentialDetails>();
        int? arrayLength = reader.ReadStartArray();
        int count = arrayLength ?? int.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (reader.PeekState() == CborReaderState.EndArray)
            {
                break;
            }

            CredentialDetails? credential = ReadCredential(reader, result);
            if (credential != null)
            {
                credentials.Add(credential);
            }
        }

        reader.ReadEndArray();
        return credentials;
    }

    private static CredentialDetails? ReadCredential(CborReader reader, GetAssertionResponseData result)
    {
        if (reader.PeekState() != CborReaderState.StartMap)
        {
            reader.SkipValue();
            return null;
        }

        var credential = new CredentialDetails();
        int? mapLength = reader.ReadStartMap();
        int count = mapLength ?? int.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (reader.PeekState() == CborReaderState.EndMap)
            {
                break;
            }

            int? key = ReadInt32(reader);
            if (!key.HasValue)
            {
                reader.SkipValue();
                continue;
            }

            switch (key.Value)
            {
                case CredentialKey:
                    credential.CredentialId = ReadCredentialId(reader);
                    break;
                case AuthenticatorDataKey:
                    ApplyAuthenticatorData(result, credential, ReadByteString(reader));
                    break;
                case SignatureKey:
                    reader.SkipValue();
                    break;
                case UserKey:
                    credential.UserInformation = ReadUserInformation(reader);
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();
        return credential.CredentialId != null || credential.UserInformation != null ? credential : null;
    }

    private static byte[]? ReadCredentialId(CborReader reader)
    {
        if (reader.PeekState() == CborReaderState.ByteString)
        {
            return reader.ReadByteString();
        }

        if (reader.PeekState() != CborReaderState.StartMap)
        {
            reader.SkipValue();
            return null;
        }

        byte[]? id = null;
        int? mapLength = reader.ReadStartMap();
        int count = mapLength ?? int.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (reader.PeekState() == CborReaderState.EndMap)
            {
                break;
            }

            if (reader.PeekState() != CborReaderState.TextString)
            {
                reader.SkipValue();
                reader.SkipValue();
                continue;
            }

            string key = reader.ReadTextString();

            switch (key)
            {
                case "id":
                    id = ReadByteString(reader);
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();
        return id;
    }

    private static UserInformation? ReadUserInformation(CborReader reader)
    {
        if (reader.PeekState() != CborReaderState.StartMap)
        {
            reader.SkipValue();
            return null;
        }

        byte[]? id = null;
        string? name = null;
        string? icon = null;
        string? displayName = null;
        int? mapLength = reader.ReadStartMap();
        int count = mapLength ?? int.MaxValue;

        for (int i = 0; i < count; i++)
        {
            if (reader.PeekState() == CborReaderState.EndMap)
            {
                break;
            }

            if (reader.PeekState() != CborReaderState.TextString)
            {
                reader.SkipValue();
                reader.SkipValue();
                continue;
            }

            string key = reader.ReadTextString();

            switch (key)
            {
                case "id":
                    id = ReadByteString(reader);
                    break;
                case "name":
                    name = ReadTextString(reader);
                    break;
                case "icon":
                    icon = ReadTextString(reader);
                    break;
                case "displayName":
                    displayName = ReadTextString(reader);
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();

        return id != null || name != null || icon != null || displayName != null
            ? new UserInformation { Id = id ?? [], Name = name, Icon = icon, DisplayName = displayName }
            : null;
    }

    private static void ApplyAuthenticatorData(GetAssertionResponseData result, CredentialDetails credential, byte[]? authenticatorData)
    {
        if (authenticatorData == null)
        {
            return;
        }

        ApplySharedAuthenticatorData(result, authenticatorData);

        try
        {
            var parsedData = FIDO.AuthenticatorData.Parse(authenticatorData);
            result.RpIdHash ??= parsedData.RelyingPartyIdHash;
            result.AuthenticatorFlags ??= parsedData.Flags;
            credential.BackedUp = parsedData.IsBackedUp;
        }
        catch (Exception)
        {
            // The credential details are still useful without decoded authenticator data.
        }
    }

    private static void ApplySharedAuthenticatorData(GetAssertionResponseData result, byte[]? authenticatorData)
    {
        result.RpIdHash ??= ReadRpIdHashFromAuthenticatorData(authenticatorData);
        result.AuthenticatorFlags ??= ReadAuthenticatorFlagsFromAuthenticatorData(authenticatorData);
    }

    private static byte[]? ReadRpIdHashFromAuthenticatorData(byte[]? authenticatorData)
    {
        return authenticatorData is { Length: >= 32 } ? authenticatorData.AsSpan(0, 32).ToArray() : null;
    }

    private static AuthenticatorFlags? ReadAuthenticatorFlagsFromAuthenticatorData(byte[]? authenticatorData)
    {
        return authenticatorData is { Length: >= 33 } ? (AuthenticatorFlags)authenticatorData[32] : null;
    }

    private static void ApplyDeviceInfo(GetAssertionResponseData result)
    {
        if (result.Credentials == null)
        {
            return;
        }

        foreach (var credential in result.Credentials)
        {
            credential.AuthenticatorName ??= result.Product ?? result.ProviderName;
            credential.ThirdPartyPayment = result.ThirdPartyPayment == true;
            credential.Transports = result.Transports ?? AuthenticatorTransport.NoRestrictions;
        }
    }

    private static int? ReadInt32(CborReader reader)
    {
        if (reader.PeekState() is CborReaderState.UnsignedInteger or CborReaderState.NegativeInteger)
        {
            return reader.ReadInt32();
        }

        reader.SkipValue();
        return null;
    }

    private static AuthenticatorFlags? ReadAuthenticatorFlags(CborReader reader)
    {
        int? flags = ReadInt32(reader);
        return flags.HasValue && flags.Value >= 0 && flags.Value <= byte.MaxValue ? (AuthenticatorFlags)flags.Value : null;
    }

    private static bool? ReadBoolean(CborReader reader)
    {
        if (reader.PeekState() == CborReaderState.Boolean)
        {
            return reader.ReadBoolean();
        }

        reader.SkipValue();
        return null;
    }

    private static string? ReadTextString(CborReader reader)
    {
        if (reader.PeekState() == CborReaderState.TextString)
        {
            return reader.ReadTextString();
        }

        reader.SkipValue();
        return null;
    }

    private static byte[]? ReadByteString(CborReader reader)
    {
        if (reader.PeekState() == CborReaderState.ByteString)
        {
            return reader.ReadByteString();
        }

        reader.SkipValue();
        return null;
    }

    private static Guid? ReadGuid(CborReader reader)
    {
        switch (reader.PeekState())
        {
            case CborReaderState.ByteString:
                byte[] bytes = reader.ReadByteString();
                return bytes.Length == 16 ? Guid.Create(bytes, bigEndian: true) : null;
            case CborReaderState.TextString:
                return Guid.TryParse(reader.ReadTextString(), out Guid guid) && guid != Guid.Empty ? guid : null;
            default:
                reader.SkipValue();
                return null;
        }
    }
}

/// <summary>
/// Decoded fields from a CTAP GetAssertion response.
/// </summary>
internal sealed class GetAssertionResponseData
{
    public int? Status { get; set; }

    public int? CtapStatus { get; set; }

    public byte[]? RpIdHash { get; set; }

    public AuthenticatorFlags? AuthenticatorFlags { get; set; }

    public string? ProviderType { get; set; }

    public string? ProviderName { get; set; }

    public string? Manufacturer { get; set; }

    public string? Product { get; set; }

    public Guid? AAGuid { get; set; }

    public bool? ThirdPartyPayment { get; set; }

    public AuthenticatorTransport? Transports { get; set; }

    public IReadOnlyList<CredentialDetails>? Credentials { get; set; }
}
