# WindowHandle structure

Represents a window handle.

```csharp
public struct WindowHandle
```

## Public Members

| name | description |
| --- | --- |
| [WindowHandle](WindowHandle/WindowHandle.md)(…) |  |
| static [ConsoleWindow](WindowHandle/ConsoleWindow.md) { get; } | Retrieves the window handle used by the console associated with the calling process. |
| static [ForegroundWindow](WindowHandle/ForegroundWindow.md) { get; } | Gets the window handle of the foreground window associated with the calling process. |
| static [MainWindow](WindowHandle/MainWindow.md) { get; } | Gets the window handle of the main window of the calling process. |
| [IsValid](WindowHandle/IsValid.md) { get; } | Indicates whether the window handle is valid. |
| override [Equals](WindowHandle/Equals.md)(…) |  |
| override [GetHashCode](WindowHandle/GetHashCode.md)() |  |
| [operator ==](WindowHandle/op_Equality.md) |  |
| [operator !=](WindowHandle/op_Inequality.md) |  |

## See Also

* namespace [DSInternals.Win32.WebAuthn](../DSInternals.Win32.WebAuthn.md)
* [WindowHandle.cs](https://github.com/MichaelGrafnetter/webauthn-interop/tree/master/Src/DSInternals.Win32.WebAuthn/WindowHandle.cs)

<!-- DO NOT EDIT: generated by xmldocmd for DSInternals.Win32.WebAuthn.dll -->