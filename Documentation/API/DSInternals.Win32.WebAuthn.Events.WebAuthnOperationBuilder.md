# <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperationBuilder"></a> Class WebAuthnOperationBuilder

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Aggregates raw WebAuthN events into <xref href="DSInternals.Win32.WebAuthn.Events.WebAuthnAttestationOperation" data-throw-if-not-resolved="false"></xref> and <xref href="DSInternals.Win32.WebAuthn.Events.WebAuthnAssertionOperation" data-throw-if-not-resolved="false"></xref> operations
grouped by transaction ID.

```csharp
public static class WebAuthnOperationBuilder
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnOperationBuilder](DSInternals.Win32.WebAuthn.Events.WebAuthnOperationBuilder.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnOperationBuilder_Build_System_Collections_Generic_IEnumerable_DSInternals_Win32_WebAuthn_Events_WebAuthnEvent__"></a> Build\(IEnumerable<WebAuthnEvent\>\)

Builds aggregated WebAuthn operations from a list of raw WebAuthN events.

```csharp
public static IReadOnlyList<WebAuthnOperation> Build(IEnumerable<WebAuthnEvent> events)
```

#### Parameters

`events` [IEnumerable](https://learn.microsoft.com/dotnet/api/system.collections.generic.ienumerable\-1)<[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md)\>

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[WebAuthnOperation](DSInternals.Win32.WebAuthn.Events.WebAuthnOperation.md)\>

