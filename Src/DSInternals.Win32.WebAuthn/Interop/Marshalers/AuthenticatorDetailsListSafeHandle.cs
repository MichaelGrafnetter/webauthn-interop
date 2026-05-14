using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace DSInternals.Win32.WebAuthn.Interop;

internal sealed class AuthenticatorDetailsListSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
{
    private AuthenticatorDetailsListSafeHandle() : base(true) { }

    protected override bool ReleaseHandle()
    {
        NativeMethods.FreeAuthenticatorList(this.handle);
        return true;
    }

    internal AuthenticatorDetailsOut[]? ToManaged()
    {
        if (this.IsInvalid)
        {
            return null;
        }

        return Marshal.PtrToStructure<AuthenticatorDetailsList>(handle)?.Items;
    }
}
