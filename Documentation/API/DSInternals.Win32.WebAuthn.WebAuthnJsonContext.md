# <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext"></a> Class WebAuthnJsonContext

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Source-generated JSON serialization metadata for WebAuthn models.

```csharp
[JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull)]
[JsonSerializable(typeof(CollectedClientData))]
[JsonSerializable(typeof(AttestationPublicKeyCredential))]
[JsonSerializable(typeof(AssertionPublicKeyCredential))]
[JsonSerializable(typeof(AuthenticatorAttestationResponse))]
[JsonSerializable(typeof(AuthenticatorAssertionResponse))]
[JsonSerializable(typeof(AuthenticationExtensionsClientAttestationInputs))]
[JsonSerializable(typeof(AuthenticationExtensionsClientAttestationOutputs))]
[JsonSerializable(typeof(AuthenticationExtensionsClientAssertionInputs))]
[JsonSerializable(typeof(AuthenticationExtensionsClientAssertionOutputs))]
[JsonSerializable(typeof(HMACGetSecretInput))]
[JsonSerializable(typeof(HMACGetSecretOutput))]
[JsonSerializable(typeof(CredentialPropertiesOutputs))]
[JsonSerializable(typeof(LargeBlobAttestationInputs))]
[JsonSerializable(typeof(LargeBlobAttestationOutputs))]
[JsonSerializable(typeof(LargeBlobAssertionInputs))]
[JsonSerializable(typeof(LargeBlobAssertionOutputs))]
[JsonSerializable(typeof(PRFAttestationInputs))]
[JsonSerializable(typeof(PRFAttestationOutputs))]
[JsonSerializable(typeof(PRFAssertionInputs))]
[JsonSerializable(typeof(PRFAssertionOutputs))]
[JsonSerializable(typeof(PRFValues))]
[JsonSerializable(typeof(RemoteDesktopClientOverride))]
[JsonSerializable(typeof(UvmEntry))]
[JsonSerializable(typeof(PaymentAttestationInputs))]
[JsonSerializable(typeof(PaymentAssertionInputs))]
[JsonSerializable(typeof(PaymentCurrencyAmount))]
[JsonSerializable(typeof(PaymentCredentialInstrument))]
[JsonSerializable(typeof(PublicKeyCredentialCreationOptions))]
[JsonSerializable(typeof(MicrosoftGraphWebauthnCredentialCreationOptions))]
[JsonSerializable(typeof(MicrosoftGraphWebauthnAttestationResponse))]
[JsonSerializable(typeof(MicrosoftGraphAttestationPublicKeyCredential))]
[JsonSerializable(typeof(OktaWebauthnCredentialCreationOptions))]
[JsonSerializable(typeof(OktaFido2AuthenticationMethod))]
[JsonSerializable(typeof(OktaWebauthnAttestationResponse))]
[JsonSerializable(typeof(KeePassXCPasskey))]
[JsonSerializable(typeof(BitwardenVaultExportHeader))]
[JsonSerializable(typeof(BitwardenCleartextVaultExport))]
[JsonSerializable(typeof(BitwardenEncryptedVaultExport))]
[JsonSerializable(typeof(CredentialExchangeFile))]
[JsonSerializable(typeof(CredentialExchangeCredential))]
[JsonSerializable(typeof(CredentialExchangePasskey))]
[JsonSerializable(typeof(PublicKeyCredentialRequestOptions))]
public class WebAuthnJsonContext : JsonSerializerContext, IJsonTypeInfoResolver
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[JsonSerializerContext](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonserializercontext) ← 
[WebAuthnJsonContext](DSInternals.Win32.WebAuthn.WebAuthnJsonContext.md)

#### Implements

[IJsonTypeInfoResolver](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.ijsontypeinforesolver)

#### Inherited Members

[JsonSerializerContext.GetTypeInfo\(Type\)](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonserializercontext.gettypeinfo), 
[JsonSerializerContext.GeneratedSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonserializercontext.generatedserializeroptions), 
[JsonSerializerContext.Options](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.jsonserializercontext.options), 
[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext__ctor"></a> WebAuthnJsonContext\(\)

```csharp
public WebAuthnJsonContext()
```

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext__ctor_System_Text_Json_JsonSerializerOptions_"></a> WebAuthnJsonContext\(JsonSerializerOptions\)

Creates an instance of <xref href="System.Text.Json.Serialization.JsonSerializerContext" data-throw-if-not-resolved="false"></xref> and binds it with the indicated <xref href="System.Text.Json.JsonSerializerOptions" data-throw-if-not-resolved="false"></xref>.

```csharp
public WebAuthnJsonContext(JsonSerializerOptions options)
```

#### Parameters

`options` [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)

The run time provided options for the context instance.

## Properties

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_Algorithm"></a> Algorithm

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<Algorithm> Algorithm { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[Algorithm](DSInternals.Win32.WebAuthn.COSE.Algorithm.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AssertionPublicKeyCredential"></a> AssertionPublicKeyCredential

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AssertionPublicKeyCredential> AssertionPublicKeyCredential { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AssertionPublicKeyCredential](DSInternals.Win32.WebAuthn.AssertionPublicKeyCredential.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AttestationConveyancePreference"></a> AttestationConveyancePreference

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AttestationConveyancePreference> AttestationConveyancePreference { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AttestationConveyancePreference](DSInternals.Win32.WebAuthn.AttestationConveyancePreference.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AttestationPublicKeyCredential"></a> AttestationPublicKeyCredential

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AttestationPublicKeyCredential> AttestationPublicKeyCredential { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.AttestationPublicKeyCredential.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticationExtensionsClientAssertionInputs"></a> AuthenticationExtensionsClientAssertionInputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticationExtensionsClientAssertionInputs> AuthenticationExtensionsClientAssertionInputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticationExtensionsClientAssertionInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionInputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticationExtensionsClientAssertionOutputs"></a> AuthenticationExtensionsClientAssertionOutputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticationExtensionsClientAssertionOutputs> AuthenticationExtensionsClientAssertionOutputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticationExtensionsClientAssertionOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAssertionOutputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticationExtensionsClientAttestationInputs"></a> AuthenticationExtensionsClientAttestationInputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticationExtensionsClientAttestationInputs> AuthenticationExtensionsClientAttestationInputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticationExtensionsClientAttestationInputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationInputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticationExtensionsClientAttestationOutputs"></a> AuthenticationExtensionsClientAttestationOutputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticationExtensionsClientAttestationOutputs> AuthenticationExtensionsClientAttestationOutputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticationExtensionsClientAttestationOutputs](DSInternals.Win32.WebAuthn.AuthenticationExtensionsClientAttestationOutputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticatorAssertionResponse"></a> AuthenticatorAssertionResponse

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticatorAssertionResponse> AuthenticatorAssertionResponse { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticatorAssertionResponse](DSInternals.Win32.WebAuthn.AuthenticatorAssertionResponse.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticatorAttachment"></a> AuthenticatorAttachment

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticatorAttachment> AuthenticatorAttachment { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticatorAttachment](DSInternals.Win32.WebAuthn.AuthenticatorAttachment.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticatorAttestationResponse"></a> AuthenticatorAttestationResponse

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticatorAttestationResponse> AuthenticatorAttestationResponse { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticatorAttestationResponse](DSInternals.Win32.WebAuthn.AuthenticatorAttestationResponse.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticatorSelectionCriteria"></a> AuthenticatorSelectionCriteria

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticatorSelectionCriteria> AuthenticatorSelectionCriteria { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticatorSelectionCriteria](DSInternals.Win32.WebAuthn.AuthenticatorSelectionCriteria.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_AuthenticatorTransport"></a> AuthenticatorTransport

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<AuthenticatorTransport> AuthenticatorTransport { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[AuthenticatorTransport](DSInternals.Win32.WebAuthn.AuthenticatorTransport.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_BitwardenCleartextVaultExport"></a> BitwardenCleartextVaultExport

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<BitwardenCleartextVaultExport> BitwardenCleartextVaultExport { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[BitwardenCleartextVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenCleartextVaultExport.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_BitwardenEncryptedVaultExport"></a> BitwardenEncryptedVaultExport

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<BitwardenEncryptedVaultExport> BitwardenEncryptedVaultExport { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[BitwardenEncryptedVaultExport](DSInternals.Win32.WebAuthn.Cryptography.BitwardenEncryptedVaultExport.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_BitwardenFido2Credential"></a> BitwardenFido2Credential

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<BitwardenFido2Credential> BitwardenFido2Credential { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[BitwardenFido2Credential](DSInternals.Win32.WebAuthn.Cryptography.BitwardenFido2Credential.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_BitwardenFido2CredentialArray"></a> BitwardenFido2CredentialArray

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<BitwardenFido2Credential[]> BitwardenFido2CredentialArray { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[BitwardenFido2Credential](DSInternals.Win32.WebAuthn.Cryptography.BitwardenFido2Credential.md)\[\]\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_BitwardenLogin"></a> BitwardenLogin

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<BitwardenLogin> BitwardenLogin { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[BitwardenLogin](DSInternals.Win32.WebAuthn.Cryptography.BitwardenLogin.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_BitwardenVaultExportHeader"></a> BitwardenVaultExportHeader

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<BitwardenVaultExportHeader> BitwardenVaultExportHeader { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[BitwardenVaultExportHeader](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultExportHeader.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_BitwardenVaultItem"></a> BitwardenVaultItem

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<BitwardenVaultItem> BitwardenVaultItem { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[BitwardenVaultItem](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultItem.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_BitwardenVaultItemArray"></a> BitwardenVaultItemArray

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<BitwardenVaultItem[]> BitwardenVaultItemArray { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[BitwardenVaultItem](DSInternals.Win32.WebAuthn.Cryptography.BitwardenVaultItem.md)\[\]\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_Boolean"></a> Boolean

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<bool> Boolean { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_Byte"></a> Byte

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<byte> Byte { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_ByteArray"></a> ByteArray

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<byte[]> ByteArray { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[byte](https://learn.microsoft.com/dotnet/api/system.byte)\[\]\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CollectedClientData"></a> CollectedClientData

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CollectedClientData> CollectedClientData { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CollectedClientData](DSInternals.Win32.WebAuthn.FIDO.CollectedClientData.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeAccount"></a> CredentialExchangeAccount

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeAccount> CredentialExchangeAccount { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeAccount](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeAccount.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeAccountArray"></a> CredentialExchangeAccountArray

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeAccount[]> CredentialExchangeAccountArray { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeAccount](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeAccount.md)\[\]\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeCredential"></a> CredentialExchangeCredential

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeCredential> CredentialExchangeCredential { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeCredential](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeCredential.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeCredentialArray"></a> CredentialExchangeCredentialArray

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeCredential[]> CredentialExchangeCredentialArray { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeCredential](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeCredential.md)\[\]\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeFido2Extensions"></a> CredentialExchangeFido2Extensions

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeFido2Extensions> CredentialExchangeFido2Extensions { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeFido2Extensions](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2Extensions.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeFido2HmacCredentials"></a> CredentialExchangeFido2HmacCredentials

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeFido2HmacCredentials> CredentialExchangeFido2HmacCredentials { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeFido2HmacCredentials](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2HmacCredentials.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeFido2LargeBlob"></a> CredentialExchangeFido2LargeBlob

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeFido2LargeBlob> CredentialExchangeFido2LargeBlob { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeFido2LargeBlob](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFido2LargeBlob.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeFile"></a> CredentialExchangeFile

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeFile> CredentialExchangeFile { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeFile](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeFile.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeItem"></a> CredentialExchangeItem

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeItem> CredentialExchangeItem { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeItem](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeItem.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeItemArray"></a> CredentialExchangeItemArray

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeItem[]> CredentialExchangeItemArray { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeItem](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeItem.md)\[\]\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangePasskey"></a> CredentialExchangePasskey

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangePasskey> CredentialExchangePasskey { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangePasskey](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangePasskey.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialExchangeVersion"></a> CredentialExchangeVersion

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialExchangeVersion> CredentialExchangeVersion { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialExchangeVersion](DSInternals.Win32.WebAuthn.Cryptography.CredentialExchangeVersion.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_CredentialPropertiesOutputs"></a> CredentialPropertiesOutputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<CredentialPropertiesOutputs> CredentialPropertiesOutputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[CredentialPropertiesOutputs](DSInternals.Win32.WebAuthn.CredentialPropertiesOutputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_DateTime"></a> DateTime

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<DateTime> DateTime { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_Default"></a> Default

The default <xref href="System.Text.Json.Serialization.JsonSerializerContext" data-throw-if-not-resolved="false"></xref> associated with a default <xref href="System.Text.Json.JsonSerializerOptions" data-throw-if-not-resolved="false"></xref> instance.

```csharp
public static WebAuthnJsonContext Default { get; }
```

#### Property Value

 [WebAuthnJsonContext](DSInternals.Win32.WebAuthn.WebAuthnJsonContext.md)

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_DictionaryStringPRFValues"></a> DictionaryStringPRFValues

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<Dictionary<string, PRFValues>> DictionaryStringPRFValues { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[Dictionary](https://learn.microsoft.com/dotnet/api/system.collections.generic.dictionary\-2)<[string](https://learn.microsoft.com/dotnet/api/system.string), [PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)\>\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_Embedded"></a> Embedded

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<Embedded> Embedded { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[Embedded](DSInternals.Win32.WebAuthn.Okta.Embedded.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_GeneratedSerializerOptions"></a> GeneratedSerializerOptions

The source-generated options associated with this context.

```csharp
protected override JsonSerializerOptions? GeneratedSerializerOptions { get; }
```

#### Property Value

 [JsonSerializerOptions](https://learn.microsoft.com/dotnet/api/system.text.json.jsonserializeroptions)?

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_HMACGetSecretInput"></a> HMACGetSecretInput

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<HMACGetSecretInput> HMACGetSecretInput { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[HMACGetSecretInput](DSInternals.Win32.WebAuthn.HMACGetSecretInput.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_HMACGetSecretOutput"></a> HMACGetSecretOutput

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<HMACGetSecretOutput> HMACGetSecretOutput { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[HMACGetSecretOutput](DSInternals.Win32.WebAuthn.HMACGetSecretOutput.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_IListUvmEntry"></a> IListUvmEntry

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<IList<UvmEntry>> IListUvmEntry { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[IList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ilist\-1)<[UvmEntry](DSInternals.Win32.WebAuthn.UvmEntry.md)\>\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_IReadOnlyListPublicKeyCredentialDescriptor"></a> IReadOnlyListPublicKeyCredentialDescriptor

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<IReadOnlyList<PublicKeyCredentialDescriptor>> IReadOnlyListPublicKeyCredentialDescriptor { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_IReadOnlyListPublicKeyCredentialParameter"></a> IReadOnlyListPublicKeyCredentialParameter

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<IReadOnlyList<PublicKeyCredentialParameter>> IReadOnlyListPublicKeyCredentialParameter { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[PublicKeyCredentialParameter](DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter.md)\>\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_IReadOnlyListString"></a> IReadOnlyListString

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<IReadOnlyList<string>> IReadOnlyListString { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_Int32"></a> Int32

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<int> Int32 { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[int](https://learn.microsoft.com/dotnet/api/system.int32)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_KeePassXCPasskey"></a> KeePassXCPasskey

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<KeePassXCPasskey> KeePassXCPasskey { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[KeePassXCPasskey](DSInternals.Win32.WebAuthn.Cryptography.KeePassXCPasskey.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_LargeBlobAssertionInputs"></a> LargeBlobAssertionInputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<LargeBlobAssertionInputs> LargeBlobAssertionInputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[LargeBlobAssertionInputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionInputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_LargeBlobAssertionOutputs"></a> LargeBlobAssertionOutputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<LargeBlobAssertionOutputs> LargeBlobAssertionOutputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[LargeBlobAssertionOutputs](DSInternals.Win32.WebAuthn.LargeBlobAssertionOutputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_LargeBlobAttestationInputs"></a> LargeBlobAttestationInputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<LargeBlobAttestationInputs> LargeBlobAttestationInputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[LargeBlobAttestationInputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationInputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_LargeBlobAttestationOutputs"></a> LargeBlobAttestationOutputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<LargeBlobAttestationOutputs> LargeBlobAttestationOutputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[LargeBlobAttestationOutputs](DSInternals.Win32.WebAuthn.LargeBlobAttestationOutputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_LargeBlobSupport"></a> LargeBlobSupport

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<LargeBlobSupport> LargeBlobSupport { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[LargeBlobSupport](DSInternals.Win32.WebAuthn.LargeBlobSupport.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_MicrosoftGraphAttestationPublicKeyCredential"></a> MicrosoftGraphAttestationPublicKeyCredential

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<MicrosoftGraphAttestationPublicKeyCredential> MicrosoftGraphAttestationPublicKeyCredential { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[MicrosoftGraphAttestationPublicKeyCredential](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphAttestationPublicKeyCredential.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_MicrosoftGraphWebauthnAttestationResponse"></a> MicrosoftGraphWebauthnAttestationResponse

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<MicrosoftGraphWebauthnAttestationResponse> MicrosoftGraphWebauthnAttestationResponse { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[MicrosoftGraphWebauthnAttestationResponse](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphWebauthnAttestationResponse.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_MicrosoftGraphWebauthnCredentialCreationOptions"></a> MicrosoftGraphWebauthnCredentialCreationOptions

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<MicrosoftGraphWebauthnCredentialCreationOptions> MicrosoftGraphWebauthnCredentialCreationOptions { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[MicrosoftGraphWebauthnCredentialCreationOptions](DSInternals.Win32.WebAuthn.Entra.MicrosoftGraphWebauthnCredentialCreationOptions.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_NullableBoolean"></a> NullableBoolean

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<bool?> NullableBoolean { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[bool](https://learn.microsoft.com/dotnet/api/system.boolean)?\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_NullableDateTime"></a> NullableDateTime

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<DateTime?> NullableDateTime { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[DateTime](https://learn.microsoft.com/dotnet/api/system.datetime)?\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_NullableInt32"></a> NullableInt32

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<int?> NullableInt32 { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[int](https://learn.microsoft.com/dotnet/api/system.int32)?\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_NullableResidentKeyRequirement"></a> NullableResidentKeyRequirement

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<ResidentKeyRequirement?> NullableResidentKeyRequirement { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)?\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_NullableUInt32"></a> NullableUInt32

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<uint?> NullableUInt32 { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[uint](https://learn.microsoft.com/dotnet/api/system.uint32)?\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_NullableUInt64"></a> NullableUInt64

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<ulong?> NullableUInt64 { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)?\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_Object"></a> Object

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<object> Object { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[object](https://learn.microsoft.com/dotnet/api/system.object)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_OktaFido2AuthenticationMethod"></a> OktaFido2AuthenticationMethod

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<OktaFido2AuthenticationMethod> OktaFido2AuthenticationMethod { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[OktaFido2AuthenticationMethod](DSInternals.Win32.WebAuthn.Okta.OktaFido2AuthenticationMethod.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_OktaFido2Profile"></a> OktaFido2Profile

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<OktaFido2Profile> OktaFido2Profile { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[OktaFido2Profile](DSInternals.Win32.WebAuthn.Okta.OktaFido2Profile.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_OktaWebauthnAttestationResponse"></a> OktaWebauthnAttestationResponse

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<OktaWebauthnAttestationResponse> OktaWebauthnAttestationResponse { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[OktaWebauthnAttestationResponse](DSInternals.Win32.WebAuthn.Okta.OktaWebauthnAttestationResponse.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_OktaWebauthnCredentialCreationOptions"></a> OktaWebauthnCredentialCreationOptions

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<OktaWebauthnCredentialCreationOptions> OktaWebauthnCredentialCreationOptions { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[OktaWebauthnCredentialCreationOptions](DSInternals.Win32.WebAuthn.Okta.OktaWebauthnCredentialCreationOptions.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PRFAssertionInputs"></a> PRFAssertionInputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PRFAssertionInputs> PRFAssertionInputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PRFAssertionInputs](DSInternals.Win32.WebAuthn.PRFAssertionInputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PRFAssertionOutputs"></a> PRFAssertionOutputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PRFAssertionOutputs> PRFAssertionOutputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PRFAssertionOutputs](DSInternals.Win32.WebAuthn.PRFAssertionOutputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PRFAttestationInputs"></a> PRFAttestationInputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PRFAttestationInputs> PRFAttestationInputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PRFAttestationInputs](DSInternals.Win32.WebAuthn.PRFAttestationInputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PRFAttestationOutputs"></a> PRFAttestationOutputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PRFAttestationOutputs> PRFAttestationOutputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PRFAttestationOutputs](DSInternals.Win32.WebAuthn.PRFAttestationOutputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PRFValues"></a> PRFValues

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PRFValues> PRFValues { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PRFValues](DSInternals.Win32.WebAuthn.PRFValues.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PaymentAssertionInputs"></a> PaymentAssertionInputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PaymentAssertionInputs> PaymentAssertionInputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PaymentAssertionInputs](DSInternals.Win32.WebAuthn.PaymentAssertionInputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PaymentAttestationInputs"></a> PaymentAttestationInputs

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PaymentAttestationInputs> PaymentAttestationInputs { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PaymentAttestationInputs](DSInternals.Win32.WebAuthn.PaymentAttestationInputs.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PaymentCredentialInstrument"></a> PaymentCredentialInstrument

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PaymentCredentialInstrument> PaymentCredentialInstrument { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PaymentCredentialInstrument](DSInternals.Win32.WebAuthn.PaymentCredentialInstrument.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PaymentCurrencyAmount"></a> PaymentCurrencyAmount

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PaymentCurrencyAmount> PaymentCurrencyAmount { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PaymentCurrencyAmount](DSInternals.Win32.WebAuthn.PaymentCurrencyAmount.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PublicKeyCredentialCreationOptions"></a> PublicKeyCredentialCreationOptions

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PublicKeyCredentialCreationOptions> PublicKeyCredentialCreationOptions { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PublicKeyCredentialCreationOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialCreationOptions.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PublicKeyCredentialDescriptor"></a> PublicKeyCredentialDescriptor

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PublicKeyCredentialDescriptor> PublicKeyCredentialDescriptor { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PublicKeyCredentialDescriptor](DSInternals.Win32.WebAuthn.PublicKeyCredentialDescriptor.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PublicKeyCredentialParameter"></a> PublicKeyCredentialParameter

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PublicKeyCredentialParameter> PublicKeyCredentialParameter { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PublicKeyCredentialParameter](DSInternals.Win32.WebAuthn.PublicKeyCredentialParameter.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_PublicKeyCredentialRequestOptions"></a> PublicKeyCredentialRequestOptions

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<PublicKeyCredentialRequestOptions> PublicKeyCredentialRequestOptions { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[PublicKeyCredentialRequestOptions](DSInternals.Win32.WebAuthn.PublicKeyCredentialRequestOptions.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_RelyingPartyInformation"></a> RelyingPartyInformation

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<RelyingPartyInformation> RelyingPartyInformation { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[RelyingPartyInformation](DSInternals.Win32.WebAuthn.RelyingPartyInformation.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_RemoteDesktopClientOverride"></a> RemoteDesktopClientOverride

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<RemoteDesktopClientOverride> RemoteDesktopClientOverride { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[RemoteDesktopClientOverride](DSInternals.Win32.WebAuthn.RemoteDesktopClientOverride.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_ResidentKeyRequirement"></a> ResidentKeyRequirement

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<ResidentKeyRequirement> ResidentKeyRequirement { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[ResidentKeyRequirement](DSInternals.Win32.WebAuthn.ResidentKeyRequirement.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_String"></a> String

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<string> String { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_StringArray"></a> StringArray

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<string[]> StringArray { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[string](https://learn.microsoft.com/dotnet/api/system.string)\[\]\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_UInt32"></a> UInt32

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<uint> UInt32 { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[uint](https://learn.microsoft.com/dotnet/api/system.uint32)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_UInt64"></a> UInt64

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<ulong> UInt64 { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[ulong](https://learn.microsoft.com/dotnet/api/system.uint64)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_UserInformation"></a> UserInformation

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<UserInformation> UserInformation { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[UserInformation](DSInternals.Win32.WebAuthn.UserInformation.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_UserVerification"></a> UserVerification

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<UserVerification> UserVerification { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[UserVerification](DSInternals.Win32.WebAuthn.UserVerification.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_UserVerificationRequirement"></a> UserVerificationRequirement

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<UserVerificationRequirement> UserVerificationRequirement { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[UserVerificationRequirement](DSInternals.Win32.WebAuthn.UserVerificationRequirement.md)\>

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_UvmEntry"></a> UvmEntry

Defines the source generated JSON serialization contract metadata for a given type.

```csharp
public JsonTypeInfo<UvmEntry> UvmEntry { get; }
```

#### Property Value

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo\-1)<[UvmEntry](DSInternals.Win32.WebAuthn.UvmEntry.md)\>

## Methods

### <a id="DSInternals_Win32_WebAuthn_WebAuthnJsonContext_GetTypeInfo_System_Type_"></a> GetTypeInfo\(Type\)

Gets metadata for the specified type.

```csharp
public override JsonTypeInfo? GetTypeInfo(Type type)
```

#### Parameters

`type` [Type](https://learn.microsoft.com/dotnet/api/system.type)

The type to fetch metadata for.

#### Returns

 [JsonTypeInfo](https://learn.microsoft.com/dotnet/api/system.text.json.serialization.metadata.jsontypeinfo)?

The metadata for the specified type, or <a href="https://learn.microsoft.com/dotnet/csharp/language-reference/keywords/null">null</a> if the context has no metadata for the type.

