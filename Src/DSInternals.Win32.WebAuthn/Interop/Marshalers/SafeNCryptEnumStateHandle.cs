using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Interop;

/// <summary>
/// Holds the opaque enumeration state produced by <c>NCryptEnumKeys</c> between calls.
/// </summary>
/// <remarks>
/// Requires a public parameterless constructor, since the interop marshaler replaces this handle
/// in place whenever the native function returns a new state value through the <c>ref</c> parameter.
/// </remarks>
internal sealed class SafeNCryptEnumStateHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    public SafeNCryptEnumStateHandle() : base(true) { }

    protected override bool ReleaseHandle()
    {
        HRESULT result = NativeMethods.NCryptFreeBuffer(this.handle);
        return result == HRESULT.S_OK;
    }
}
