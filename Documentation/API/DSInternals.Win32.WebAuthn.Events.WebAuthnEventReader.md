# <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEventReader"></a> Class WebAuthnEventReader

Namespace: [DSInternals.Win32.WebAuthn.Events](DSInternals.Win32.WebAuthn.Events.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Reads events from the Microsoft-Windows-WebAuthN/Operational event log.

```csharp
public static class WebAuthnEventReader
```

#### Inheritance

[object](https://learn.microsoft.com/dotnet/api/system.object) ← 
[WebAuthnEventReader](DSInternals.Win32.WebAuthn.Events.WebAuthnEventReader.md)

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.MemberwiseClone\(\)](https://learn.microsoft.com/dotnet/api/system.object.memberwiseclone), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Methods

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEventReader_ReadEvents_System_Nullable_System_Int32__"></a> ReadEvents\(int?\)

Reads all events from the local WebAuthN event log.

```csharp
public static IReadOnlyList<WebAuthnEvent> ReadEvents(int? maxEvents = null)
```

#### Parameters

`maxEvents` [int](https://learn.microsoft.com/dotnet/api/system.int32)?

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md)\>

### <a id="DSInternals_Win32_WebAuthn_Events_WebAuthnEventReader_ReadEventsFromFile_System_String_System_Nullable_System_Int32__"></a> ReadEventsFromFile\(string, int?\)

Reads events from a saved .evtx file.

```csharp
public static IReadOnlyList<WebAuthnEvent> ReadEventsFromFile(string filePath, int? maxEvents = null)
```

#### Parameters

`filePath` [string](https://learn.microsoft.com/dotnet/api/system.string)

`maxEvents` [int](https://learn.microsoft.com/dotnet/api/system.int32)?

#### Returns

 [IReadOnlyList](https://learn.microsoft.com/dotnet/api/system.collections.generic.ireadonlylist\-1)<[WebAuthnEvent](DSInternals.Win32.WebAuthn.Events.WebAuthnEvent.md)\>

