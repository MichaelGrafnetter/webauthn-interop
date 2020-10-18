using System;
using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// Corresponds to WEBAUTHN_CREDENTIAL_LIST.
    /// Contain an array of pointers to target structures.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialList : IDisposable
    {
        private int _length;
        private IntPtr _nativeArray = IntPtr.Zero;

        public CredentialList(CredentialEx[] credentials) : base()
        {
            if ((credentials?.Length ?? 0) <= 0)
            {
                // Nothing to initialize
                return;
            }

            _length = credentials.Length;

            // Allocate memory (We save pointers to the beginning of the array, followed by the items themselves.)
            int itemSize = Marshal.SizeOf<CredentialEx>();
            int arraySize = _length * (itemSize + IntPtr.Size);
            _nativeArray = Marshal.AllocHGlobal(arraySize);

            // Copy items
            IntPtr itemsStart = _nativeArray + _length * IntPtr.Size;
            for (int i = 0; i < _length; i++)
            {
                // Marshal item
                IntPtr itemPosition = itemsStart + (itemSize * i);
                Marshal.StructureToPtr<CredentialEx>(credentials[i], itemPosition, false);

                // Marshal item pointer
                IntPtr pointerPosition = _nativeArray + IntPtr.Size * i;
                Marshal.WriteIntPtr(pointerPosition, itemPosition);
            }
        }

        public void Dispose()
        {
            _length = 0;

            if (_nativeArray != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_nativeArray);
                _nativeArray = IntPtr.Zero;
            }
        }
    }
}
