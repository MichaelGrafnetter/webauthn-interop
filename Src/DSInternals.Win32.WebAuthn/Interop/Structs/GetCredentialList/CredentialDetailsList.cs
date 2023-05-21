using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// List of platform credentials.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_DETAILS_LIST.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialDetailsList
    {
        private int _length;
        private IntPtr _nativeArray = IntPtr.Zero;

        private CredentialDetailsList() { }

        public CredentialDetailsOut[] Items
        {
            get
            {
                if (_nativeArray == IntPtr.Zero || _length <= 0)
                {
                    return null;
                }

                // Allocate a managed array
                CredentialDetailsOut[] managedArray = new CredentialDetailsOut[_length];

                // Marshal items one-by-one
                for (int i = 0; i < _length; i++)
                {
                    IntPtr currentItem = Marshal.ReadIntPtr(_nativeArray + i * Marshal.SizeOf<IntPtr>());

                    // Handle possible older structure versions
                    CredentialDetailsVersion version = (CredentialDetailsVersion) Marshal.ReadInt32(currentItem);
                    int sourceStructSize;

                    switch (version)
                    {
                        // HACK: Due to a bug in Windows 11 22H2, the version is returned as 0 instead of 1.
                        case 0:
                        case CredentialDetailsVersion.Version1:
                            sourceStructSize = Marshal.SizeOf<CredentialDetailsV1>();
                            break;
                        case CredentialDetailsVersion.Version2:
                        default:
                            sourceStructSize = Marshal.SizeOf<CredentialDetailsOut>();
                            break;
                    }

                    managedArray[i] = VersionedStructMarshaler.PtrToStructure<CredentialDetailsOut>(currentItem, sourceStructSize);
                }

                return managedArray;
            }
        }
    }
}
