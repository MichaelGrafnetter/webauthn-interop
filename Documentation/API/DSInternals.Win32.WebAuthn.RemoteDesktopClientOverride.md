# <a id="DSInternals_Win32_WebAuthn_RemoteDesktopClientOverride"></a> Class RemoteDesktopClientOverride

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Inputs for Chromium's remoteDesktopClientOverride WebAuthn extension.

```csharp
public sealed class RemoteDesktopClientOverride
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[RemoteDesktopClientOverride](DSInternals.Win32.WebAuthn.RemoteDesktopClientOverride.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Properties

### <a id="DSInternals_Win32_WebAuthn_RemoteDesktopClientOverride_Origin"></a> Origin

Origin of the remote caller that issued the original WebAuthn request.

```csharp
[JsonPropertyName("origin")]
[JsonRequired]
public required string Origin { get; set; }
```

#### Property Value

 [string](https://learn.microsoft.com/dotnet/api/system.string)

### <a id="DSInternals_Win32_WebAuthn_RemoteDesktopClientOverride_SameOriginWithAncestor"></a> SameOriginWithAncestor

Accepts the singular spelling used in early explainer examples while serializing the Chromium spelling.

```csharp
[JsonPropertyName("sameOriginWithAncestor")]
[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
[Obsolete("This property is only for deserializing the early explainer version of the extension input. Use SameOriginWithAncestors for new code.", false)]
public bool? SameOriginWithAncestor { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)?

### <a id="DSInternals_Win32_WebAuthn_RemoteDesktopClientOverride_SameOriginWithAncestors"></a> SameOriginWithAncestors

Indicates whether the remote caller is same-origin with all of its ancestors.

```csharp
[JsonPropertyName("sameOriginWithAncestors")]
public bool SameOriginWithAncestors { get; set; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

## Methods

### <a id="DSInternals_Win32_WebAuthn_RemoteDesktopClientOverride_FromJson_System_String_"></a> FromJson\(string\)

Deserializes a JSON string into remote desktop client override inputs.

```csharp
public static RemoteDesktopClientOverride? FromJson(string json)
```

#### Parameters

`json` [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of remote desktop client override inputs.

#### Returns

 [RemoteDesktopClientOverride](DSInternals.Win32.WebAuthn.RemoteDesktopClientOverride.md)?

Remote desktop client override inputs if deserialization is successful; otherwise, null.

### <a id="DSInternals_Win32_WebAuthn_RemoteDesktopClientOverride_ToString"></a> ToString\(\)

Serializes the remote desktop client override inputs to JSON.

```csharp
public override string ToString()
```

#### Returns

 [string](https://learn.microsoft.com/dotnet/api/system.string)

JSON representation of these remote desktop client override inputs.

