using Microsoft.Win32.SafeHandles;
using Windows.Win32.Foundation;

namespace DSInternals.Win32.WebAuthn.Interop;

internal sealed class SafeNCryptBufferHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private SafeNCryptBufferHandle() : base(true) { }

    protected override bool ReleaseHandle()
    {
        HRESULT result = NativeMethods.NCryptFreeBuffer(this.handle);
        return result == HRESULT.S_OK;
    }
}
