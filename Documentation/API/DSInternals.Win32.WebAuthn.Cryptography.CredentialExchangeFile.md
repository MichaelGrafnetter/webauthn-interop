# <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile"></a> Class CredentialExchangeFile

Namespace: [DSInternals.Win32.WebAuthn.Cryptography](DSInternals.Win32.WebAuthn.Cryptography.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents the top-level Header of a FIDO Credential Exchange Format (CXF) payload.

```csharp
public sealed class CredentialExchangeFile
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[CredentialExchangeFile](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFile.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Remarks

Implements the cleartext data model defined in
<a href="https://fidoalliance.org/specs/cx/cxf-v1.0-ps-errata-20260309.html">FIDO Credential Exchange Format v1.0</a>.

## Fields

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_PasskeyCredentialType"></a> PasskeyCredentialType

The CXF credential type discriminator value used for passkeys.

```csharp
public const string PasskeyCredentialType = "passkey"
```

#### Field Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

## Properties

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_Accounts"></a> Accounts

The list of accounts contained in the export.

```csharp
[JsonPropertyName("accounts")]
public CredentialExchangeAccount[]? Accounts { get; set; }
```

#### Property Value

 [CredentialExchangeAccount](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeAccount.md)\[\]?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_ExporterDisplayName"></a> ExporterDisplayName

The display name of the exporting credential provider.

```csharp
[JsonPropertyName("exporterDisplayName")]
public string? ExporterDisplayName { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_ExporterRelyingPartyId"></a> ExporterRelyingPartyId

The relying party identifier of the exporting credential provider.

```csharp
[JsonPropertyName("exporterRpId")]
public string? ExporterRelyingPartyId { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)?

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_Timestamp"></a> Timestamp

The Unix timestamp at which the export was generated, in seconds.

```csharp
[JsonPropertyName("timestamp")]
public ulong Timestamp { get; set; }
```

#### Property Value

 [ulong](https://learn.microsoft.com/dotnet/api/system.uint64)

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_Version"></a> Version

The version of the CXF payload.

```csharp
[JsonPropertyName("version")]
public CredentialExchangeVersion? Version { get; set; }
```

#### Property Value

 [CredentialExchangeVersion](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeVersion.md)?

## Methods

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_GetPasskeys_System_Nullable_System_Guid__"></a> GetPasskeys\(Guid?\)

Returns all passkeys contained in this CXF payload.

```csharp
public IReadOnlyList<ExportedPasskey> GetPasskeys(Guid? aaGuid = null)
```

#### Parameters

`aaGuid` [Guid](https://learn.microsoft.com/dotnet/api/system.guid)?

Optional AAGUID to assign to the imported passkeys. Defaults to
<xref href="System.Guid.Empty" data-throw-if-not-resolved="false"></xref> as CXF does not carry an AAGUID.

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[ExportedPasskey](DSInternals.Win32.WebAuthn.Cryptography.ExportedPasskey.md)\>

The passkeys contained in the export.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_LoadFromFile_System_String_"></a> LoadFromFile\(string\)

Loads and parses a CXF JSON document from disk.

```csharp
public static CredentialExchangeFile LoadFromFile(string filePath)
```

#### Parameters

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string)

Path to the CXF JSON file.

#### Returns

 [CredentialExchangeFile](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFile.md)

The parsed CXF payload.

### <a id="DSInternals_Win32_WebAuthn_Cryptography_CredentialExchangeFile_LoadFromJson_System_String_"></a> LoadFromJson\(string\)

Parses a CXF JSON document.

```csharp
public static CredentialExchangeFile LoadFromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

The CXF JSON content.

#### Returns

 [CredentialExchangeFile](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFile.md)

The parsed CXF payload.

