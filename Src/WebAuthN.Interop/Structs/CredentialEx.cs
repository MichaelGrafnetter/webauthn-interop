using System;
using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    /// <summary>
    /// Information about credential with extra information, such as, Transports.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class  CredentialExIn : IDisposable
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialExVersion Version = CredentialExVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private VariableByteArrayIn _id = new VariableByteArrayIn(null);

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>
        public string Type;

        /// <summary>
        /// Transports.
        /// </summary>
        public AuthenticatorTransport Transports;

        public byte[] Id
        {
            get => _id?.Data;
            set => _id = new VariableByteArrayIn(value);
        }

        public void Dispose()
        {
            if(_id != null)
            {
                _id.Dispose();
                _id = null;
            }
        }
    }

    /// <summary>
    /// Information about credential with extra information, such as, Transports.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_EX.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class CredentialExOut
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private protected CredentialExVersion Version = CredentialExVersion.Current;

        /// <summary>
        /// Unique ID for this particular credential.
        /// </summary>
        private VariableByteArrayOut _id;

        /// <summary>
        /// Well-known credential type specifying what this particular credential is.
        /// </summary>  
        public string Type;

        /// <summary>
        /// Transports.
        /// </summary>
        public AuthenticatorTransport Transports;

        public byte[] Id
        {
            get => _id?.Data;
        }
    }

    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_LIST.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialExListOut : VariableArray<CredentialExOut>
    {
        private CredentialExListOut() : base() { }

        public override CredentialExOut[] Data
        {
            get
            {
                if (_nativeArray == IntPtr.Zero)
                {
                    return null;
                }

                // Allocate a managed array
                var managedArray = new CredentialExOut[_length];

                // Marshal items one-by-one
                int itemSize = Marshal.SizeOf<CredentialExOut>();
                for (int i = 0; i < _length; i++)
                {
                    // The array contains pointers to structures.
                    IntPtr itemAddress = Marshal.ReadIntPtr(_nativeArray + IntPtr.Size * i);
                    managedArray[i] = Marshal.PtrToStructure<CredentialExOut>(itemAddress);
                }

                return managedArray;
            }
        }
    }

    // TODO: Make these structures not inherited.

    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIAL_LIST.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class CredentialExListIn : VariableArrayIn<CredentialExIn>
    {
        public CredentialExListIn(CredentialExIn[] credentials) : base(credentials) { }

        public override CredentialExIn[] Data
        {
            get
            {
                // We could get into some double deallocation situations.
                throw new NotSupportedException();
            }
        }

        protected override void InitializePointer(CredentialExIn[] data)
        {
            // This structure contains array of pointers to target structures.
            // We save pointers to the beginning of the array, followed by the items themselves.

            // Allocate memory
            int itemSize = Marshal.SizeOf<CredentialExIn>();
            int arraySize = data.Length * (itemSize + IntPtr.Size);
            _nativeArray = Marshal.AllocHGlobal(arraySize);

            // Copy items
            IntPtr itemsStart = _nativeArray + data.Length * IntPtr.Size;
            for (int i = 0; i < data.Length; i++)
            {
                // Marshal item
                IntPtr itemPosition = itemsStart + (itemSize * i);
                Marshal.StructureToPtr<CredentialExIn>(data[i], itemPosition, false);

                // Marshal item pointer
                IntPtr pointerPosition = _nativeArray + IntPtr.Size * i;
                Marshal.WriteIntPtr(pointerPosition, itemPosition);
            }
        }
    }
}
