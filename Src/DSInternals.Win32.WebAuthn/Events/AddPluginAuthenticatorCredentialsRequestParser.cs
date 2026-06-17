using System;
using System.Collections.Generic;
using System.Formats.Cbor;
using System.Text;

namespace DSInternals.Win32.WebAuthn.Events;

/// <summary>
/// Parses AddPluginAuthenticatorCredentials CBOR request payloads.
/// </summary>
internal static class AddPluginAuthenticatorCredentialsRequestParser
{
    /// <summary>
    /// Credential record key for the credential ID byte string.
    /// </summary>
    private const int CredentialIdKey = 1;

    /// <summary>
    /// Credential record key for the relying party information map.
    /// </summary>
    private const int RelyingPartyKey = 2;

    /// <summary>
    /// Credential record key for the user information map.
    /// </summary>
    private const int UserKey = 3;

    /// <summary>
    /// Credential record key indicating whether the credential is removable.
    /// </summary>
    private const int RemovableKey = 4;

    /// <summary>
    /// Credential record key indicating whether the credential is backed up.
    /// </summary>
    private const int BackedUpKey = 5;

    /// <summary>
    /// Credential record key for the authenticator name.
    /// </summary>
    private const int AuthenticatorNameKey = 6;

    /// <summary>
    /// Credential record key for the authenticator logo.
    /// </summary>
    private const int AuthenticatorLogoKey = 7;

    /// <summary>
    /// Credential record key indicating whether the credential is a third-party payment credential.
    /// </summary>
    private const int ThirdPartyPaymentKey = 8;

    /// <summary>
    /// Credential record key for the credential transports bit field.
    /// </summary>
    private const int TransportsKey = 9;

    /// <summary>
    /// Parses a CBOR-encoded AddPluginAuthenticatorCredentials request.
    /// </summary>
    /// <param name="request">The CBOR-encoded request payload from the event log.</param>
    /// <returns>The decoded request, or <see langword="null"/> when the payload is empty or invalid.</returns>
    public static AddPluginAuthenticatorCredentialsRequest? Parse(byte[]? request)
    {
        if (request == null || request.Length == 0)
        {
            return null;
        }

        try
        {
            var reader = new CborReader(request, CborConformanceMode.Lax);

            return reader.PeekState() == CborReaderState.StartMap
                ? ReadRequest(reader)
                : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Parses a CBOR-encoded credential details array.
    /// </summary>
    /// <param name="credentialDetails">The CBOR-encoded credential details payload.</param>
    /// <returns>The decoded credential list, or <see langword="null"/> when the payload is empty or invalid.</returns>
    public static IReadOnlyList<CredentialDetails>? ParseCredentialDetails(byte[]? credentialDetails)
    {
        if (credentialDetails == null || credentialDetails.Length == 0)
        {
            return null;
        }

        try
        {
            return ReadCredentials(credentialDetails);
        }
        catch (Exception)
        {
            return null;
        }
    }

    /// <summary>
    /// Reads the top-level request map.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the start of the request map.</param>
    /// <returns>The decoded request fields.</returns>
    private static AddPluginAuthenticatorCredentialsRequest ReadRequest(CborReader reader)
    {
        var result = new AddPluginAuthenticatorCredentialsRequest();
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
                case "command":
                case "flags":
                case "timeout":
                case "transactionId":
                    reader.SkipValue();
                    break;
                case "filterHybridTransport":
                    result.FilterHybridTransport = ReadBoolean(reader);
                    break;
                case "credentialDetails":
                    result.Credentials = ReadCredentialsValue(reader);
                    break;
                case "pluginClsId":
                case "pluginClassId":
                    result.PluginClassId = ReadGuidFromTextString(reader);
                    break;
                case "authenticatorInfoLogoRequestType":
                case "pluginAutofillScenarioSupported":
                case "uvTransactionId":
                case "thirdPartyPayment":
                    reader.SkipValue();
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();
        return result;
    }

    /// <summary>
    /// Reads the credentialDetails value, which may be an array or a byte string containing a nested array.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the credentialDetails value.</param>
    /// <returns>The decoded credential list, or <see langword="null"/> when the value shape is unsupported.</returns>
    private static IReadOnlyList<CredentialDetails>? ReadCredentialsValue(CborReader reader)
    {
        return reader.PeekState() switch
        {
            CborReaderState.ByteString => ReadCredentials(reader.ReadByteString()),
            CborReaderState.StartArray => ReadCredentials(reader),
            _ => SkipAndReturn<IReadOnlyList<CredentialDetails>>(reader)
        };
    }

    /// <summary>
    /// Reads credentials from a nested CBOR byte string.
    /// </summary>
    /// <param name="credentialDetails">The CBOR byte string containing the credential array.</param>
    /// <returns>The decoded credential list, or <see langword="null"/> when the payload is not an array.</returns>
    private static IReadOnlyList<CredentialDetails>? ReadCredentials(byte[] credentialDetails)
    {
        if (credentialDetails.Length == 0)
        {
            return [];
        }

        var reader = new CborReader(credentialDetails, CborConformanceMode.Lax);

        return reader.PeekState() == CborReaderState.StartArray
            ? ReadCredentials(reader)
            : null;
    }

    /// <summary>
    /// Reads a credential array.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the start of the credential array.</param>
    /// <returns>The decoded credential list.</returns>
    private static IReadOnlyList<CredentialDetails> ReadCredentials(CborReader reader)
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

            var credential = ReadCredential(reader);
            if (credential != null)
            {
                credentials.Add(credential);
            }
        }

        reader.ReadEndArray();
        return credentials;
    }

    /// <summary>
    /// Reads a single credential record.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the credential record value.</param>
    /// <returns>The decoded credential, or <see langword="null"/> when the value is not a map.</returns>
    private static CredentialDetails? ReadCredential(CborReader reader)
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
                case CredentialIdKey:
                    credential.CredentialId = ReadByteString(reader);
                    break;
                case RelyingPartyKey:
                    credential.RelyingPartyInformation = ReadRelyingPartyInformation(reader);
                    break;
                case UserKey:
                    credential.UserInformation = ReadUserInformation(reader);
                    break;
                case RemovableKey:
                    credential.Removable = ReadBoolean(reader) == true;
                    break;
                case BackedUpKey:
                    credential.BackedUp = ReadBoolean(reader) == true;
                    break;
                case AuthenticatorNameKey:
                    credential.AuthenticatorName = ReadTextString(reader);
                    break;
                case AuthenticatorLogoKey:
                    credential.AuthenticatorLogo = ReadLogo(reader);
                    break;
                case ThirdPartyPaymentKey:
                    credential.ThirdPartyPayment = ReadBoolean(reader) == true;
                    break;
                case TransportsKey:
                    credential.Transports = (AuthenticatorTransport)(uint)(ReadInt32(reader) ?? 0);
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();
        return credential;
    }

    /// <summary>
    /// Reads a relying party information map.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the relying party value.</param>
    /// <returns>The decoded relying party information, or <see langword="null"/> when the value is not a map.</returns>
    private static RelyingPartyInformation? ReadRelyingPartyInformation(CborReader reader)
    {
        if (reader.PeekState() != CborReaderState.StartMap)
        {
            reader.SkipValue();
            return null;
        }

        string? id = null;
        string? name = null;
        string? icon = null;
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
                    id = ReadTextString(reader);
                    break;
                case "name":
                    name = ReadTextString(reader);
                    break;
                case "icon":
                    icon = ReadTextString(reader);
                    break;
                default:
                    reader.SkipValue();
                    break;
            }
        }

        reader.ReadEndMap();

        return id != null || name != null || icon != null
            ? new RelyingPartyInformation { Id = id, Name = name ?? string.Empty, Icon = icon }
            : null;
    }

    /// <summary>
    /// Reads a user information map.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the user value.</param>
    /// <returns>The decoded user information, or <see langword="null"/> when the value is not a map.</returns>
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

    /// <summary>
    /// Reads a signed 32-bit integer value.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the integer value.</param>
    /// <returns>The decoded integer, or <see langword="null"/> when the next value is not an integer.</returns>
    private static int? ReadInt32(CborReader reader)
    {
        if (reader.PeekState() is CborReaderState.UnsignedInteger or CborReaderState.NegativeInteger)
        {
            return reader.ReadInt32();
        }

        reader.SkipValue();
        return null;
    }

    /// <summary>
    /// Reads a Boolean value.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the Boolean value.</param>
    /// <returns>The decoded Boolean, or <see langword="null"/> when the next value is not a Boolean.</returns>
    private static bool? ReadBoolean(CborReader reader)
    {
        if (reader.PeekState() == CborReaderState.Boolean)
        {
            return reader.ReadBoolean();
        }

        reader.SkipValue();
        return null;
    }

    /// <summary>
    /// Reads a text string value.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the text string value.</param>
    /// <returns>The decoded string, or <see langword="null"/> when the next value is not a text string.</returns>
    private static string? ReadTextString(CborReader reader)
    {
        if (reader.PeekState() == CborReaderState.TextString)
        {
            return reader.ReadTextString();
        }

        reader.SkipValue();
        return null;
    }

    /// <summary>
    /// Reads a byte string value.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the byte string value.</param>
    /// <returns>The decoded bytes, or <see langword="null"/> when the next value is not a byte string.</returns>
    private static byte[]? ReadByteString(CborReader reader)
    {
        if (reader.PeekState() == CborReaderState.ByteString)
        {
            return reader.ReadByteString();
        }

        reader.SkipValue();
        return null;
    }

    /// <summary>
    /// Reads a GUID encoded as a text string.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the GUID text string value.</param>
    /// <returns>The decoded GUID, or <see langword="null"/> when the value is missing, invalid, or empty.</returns>
    private static Guid? ReadGuidFromTextString(CborReader reader)
    {
        string? value = ReadTextString(reader);

        return Guid.TryParse(value, out Guid guid) && guid != Guid.Empty
            ? guid
            : null;
    }

    /// <summary>
    /// Reads an authenticator logo value from either a byte string or text string.
    /// </summary>
    /// <param name="reader">The CBOR reader positioned at the logo value.</param>
    /// <returns>The decoded logo string, or <see langword="null"/> when the value is unsupported or empty.</returns>
    private static string? ReadLogo(CborReader reader)
    {
        return reader.PeekState() switch
        {
            CborReaderState.ByteString => DecodeBinaryLogo(reader.ReadByteString()),
            CborReaderState.TextString => reader.ReadTextString(),
            _ => SkipAndReturn<string>(reader)
        };
    }

    /// <summary>
    /// Decodes a UTF-8 authenticator logo byte string.
    /// </summary>
    /// <param name="logoBytes">The logo bytes, optionally terminated with a null byte.</param>
    /// <returns>The decoded logo string, or <see langword="null"/> when the input is empty.</returns>
    private static string? DecodeBinaryLogo(byte[]? logoBytes)
    {
        if (logoBytes == null || logoBytes.Length == 0)
        {
            return null;
        }

        int logoLength = logoBytes[^1] == 0 ? logoBytes.Length - 1 : logoBytes.Length;

        return logoLength > 0
            ? Encoding.UTF8.GetString(logoBytes, 0, logoLength)
            : null;
    }

    /// <summary>
    /// Skips the next CBOR value and returns the default value for the requested type.
    /// </summary>
    /// <typeparam name="T">The return type.</typeparam>
    /// <param name="reader">The CBOR reader positioned at the value to skip.</param>
    /// <returns>The default value for <typeparamref name="T"/>.</returns>
    private static T? SkipAndReturn<T>(CborReader reader)
    {
        reader.SkipValue();
        return default;
    }
}

/// <summary>
/// Decoded fields from an AddPluginAuthenticatorCredentials CBOR request.
/// </summary>
internal sealed class AddPluginAuthenticatorCredentialsRequest
{
    /// <summary>
    /// Whether hybrid transport credentials are filtered.
    /// </summary>
    public bool? FilterHybridTransport { get; set; }

    /// <summary>
    /// Credentials included in the request.
    /// </summary>
    public IReadOnlyList<CredentialDetails>? Credentials { get; set; }

    /// <summary>
    /// The plugin authenticator COM class ID.
    /// </summary>
    public Guid? PluginClassId { get; set; }
}
