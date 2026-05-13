# <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphWebauthnAttestationResponse"></a> Class MicrosoftGraphWebauthnAttestationResponse

Namespace: [DSInternals.Win32.WebAuthn.Entra](DSInternals.Win32.WebAuthn.Entra.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Microsoft Graph payload used to submit a WebAuthn attestation result.

```csharp
public class MicrosoftGraphWebauthnAttestationResponse
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[MicrosoftGraphWebauthnAttestationResponse](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphWebauthnAttestationResponse.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphWebauthnAttestationResponse__ctor_DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphAttestationPublicKeyCredential_System_String_"></a> MicrosoftGraphWebauthnAttestationResponse\(MicrosoftGraphAttestationPublicKeyCredential, string\)

Initializes a new Microsoft Graph attestation response payload from a Graph-shaped credential.

```csharp
[SetsRequiredMembers]
[JsonConstructor]
public MicrosoftGraphWebauthnAttestationResponse(MicrosoftGraphAttestationPublicKeyCredential publicKeyCredential, string displayName)
```

#### Parameters

`publicKeyCredential` [MicrosoftGraphAttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphAttestationPublicKeyCredential.md)

Graph-shaped WebAuthn credential.

`displayName` [string](https://learn.microsoft.com/dotnet/api/system.string)

User-provided passkey display name.

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphWebauthnAttestationResponse__ctor_DSInternals_Win32_WebAuthn_AttestationPublicKeyCredential_System_String_"></a> MicrosoftGraphWebauthnAttestationResponse\(AttestationPublicKeyCredential, string\)

Initializes a new Microsoft Graph attestation response payload by adapting a standard
<xref href="DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential" data-throw-if-not-resolved="false"></xref> returned by the WebAuthn API.

```csharp
[SetsRequiredMembers]
public MicrosoftGraphWebauthnAttestationResponse(AttestationPublicKeyCredential publicKeyCredential, string displayName)
```

#### Parameters

`publicKeyCredential` [AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)

WebAuthn credential returned by the authenticator.

`displayName` [string](https://learn.microsoft.com/dotnet/api/system.string)

User-provided passkey display name.

## Properties

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphWebauthnAttestationResponse_DisplayName"></a> DisplayName

The display name of the key as given by the user.

```csharp
[JsonPropertyName("displayName")]
[JsonRequired]
public required string DisplayName { get; init; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphWebauthnAttestationResponse_PublicKeyCred"></a> PublicKeyCred

Graph-shaped credential emitted in the serialized payload.

```csharp
[JsonPropertyName("publicKeyCredential")]
[JsonRequired]
public required MicrosoftGraphAttestationPublicKeyCredential PublicKeyCred { get; init; }
```

#### Property Value

 [MicrosoftGraphAttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphAttestationPublicKeyCredential.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Entra_MicrosoftGraphWebauthnAttestationResponse_ToString"></a> ToString\(\)

Serializes this payload to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation expected by Microsoft Graph APIs.

