# <a id="DSInternals_Win32_WebAuthn_WindowHandle"></a> Struct WindowHandle

Namespace: [DSInternals.Win32.WebAuthn](DSInternals.Win32.WebAuthn.md)  
Assembly: DSInternals.Win32.WebAuthn.dll  

Represents a window handle.

```csharp
public struct WindowHandle
```

#### Inherited Members

[object.Equals\(object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\)), 
[object.Equals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.equals\#system\-object\-equals\(system\-object\-system\-object\)), 
[object.GetHashCode\(\)](https://learn.microsoft.com/dotnet/api/system.object.gethashcode), 
[object.GetType\(\)](https://learn.microsoft.com/dotnet/api/system.object.gettype), 
[object.ReferenceEquals\(object?, object?\)](https://learn.microsoft.com/dotnet/api/system.object.referenceequals), 
[object.ToString\(\)](https://learn.microsoft.com/dotnet/api/system.object.tostring)

## Constructors

### <a id="DSInternals_Win32_WebAuthn_WindowHandle__ctor_System_IntPtr_"></a> WindowHandle\(nint\)

```csharp
public WindowHandle(nint handle)
```

#### Parameters

`handle` [nint](https://learn.microsoft.com/dotnet/api/system.intptr)

## Properties

### <a id="DSInternals_Win32_WebAuthn_WindowHandle_ConsoleWindow"></a> ConsoleWindow

Retrieves the window handle used by the console associated with the calling process.

```csharp
public static WindowHandle ConsoleWindow { get; }
```

#### Property Value

 [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

### <a id="DSInternals_Win32_WebAuthn_WindowHandle_ForegroundWindow"></a> ForegroundWindow

Gets the window handle of the foreground window associated with the calling process.

```csharp
public static WindowHandle ForegroundWindow { get; }
```

#### Property Value

 [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

### <a id="DSInternals_Win32_WebAuthn_WindowHandle_IsValid"></a> IsValid

Indicates whether the window handle is valid.

```csharp
public bool IsValid { get; }
```

#### Property Value

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_WindowHandle_MainWindow"></a> MainWindow

Gets the window handle of the main window of the calling process.

```csharp
public static WindowHandle MainWindow { get; }
```

#### Property Value

 [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

## Methods

### <a id="DSInternals_Win32_WebAuthn_WindowHandle_Equals_System_Object_"></a> Equals\(object\)

```csharp
public override bool Equals(object obj)
```

#### Parameters

`obj` [object](https://learn.microsoft.com/dotnet/api/system.object)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_WindowHandle_GetHashCode"></a> GetHashCode\(\)

```csharp
public override int GetHashCode()
```

#### Returns

 [int](https://learn.microsoft.com/dotnet/api/system.int32)

## Operators

### <a id="DSInternals_Win32_WebAuthn_WindowHandle_op_Equality_DSInternals_Win32_WebAuthn_WindowHandle_DSInternals_Win32_WebAuthn_WindowHandle_"></a> operator ==\(WindowHandle, WindowHandle\)

```csharp
public static bool operator ==(WindowHandle a, WindowHandle b)
```

#### Parameters

`a` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

`b` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

### <a id="DSInternals_Win32_WebAuthn_WindowHandle_op_Inequality_DSInternals_Win32_WebAuthn_WindowHandle_DSInternals_Win32_WebAuthn_WindowHandle_"></a> operator \!=\(WindowHandle, WindowHandle\)

```csharp
public static bool operator !=(WindowHandle a, WindowHandle b)
```

#### Parameters

`a` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

`b` [WindowHandle](DSInternals.Win32.WebAuthn.WindowHandle.md)

#### Returns

 [bool](https://learn.microsoft.com/dotnet/api/system.boolean)

