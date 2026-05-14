using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop;

/// <summary>
/// List of authenticator details.
/// </summary>
/// <remarks>Corresponds to WEBAUTHN_AUTHENTICATOR_DETAILS_LIST.</remarks>
[StructLayout(LayoutKind.Sequential)]
internal sealed class AuthenticatorDetailsList
{
    private int _length;
    private IntPtr _nativeArray;

    private AuthenticatorDetailsList() { }

    public AuthenticatorDetailsOut[]? Items
    {
        get
        {
            if (_nativeArray == IntPtr.Zero || _length <= 0)
            {
                return null;
            }

            // Allocate a managed array
            AuthenticatorDetailsOut[] managedArray = new AuthenticatorDetailsOut[_length];

            // Marshal items one-by-one
            for (int i = 0; i < _length; i++)
            {
                IntPtr currentItem = Marshal.ReadIntPtr(_nativeArray + i * Marshal.SizeOf<IntPtr>());

                // Handle possible older structure versions
                AuthenticatorDetailsVersion version = (AuthenticatorDetailsVersion)Marshal.ReadInt32(currentItem);
                int sourceStructSize;

                switch (version)
                {
                    case AuthenticatorDetailsVersion.Version1:
                    default:
                        sourceStructSize = Marshal.SizeOf<AuthenticatorDetailsOut>();
                        break;
                }

                managedArray[i] = VersionedStructMarshaler.PtrToStructure<AuthenticatorDetailsOut>(currentItem, sourceStructSize);
            }

            return managedArray;
        }
    }
}
