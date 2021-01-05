using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Information about credential list with extra information.
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_CREDENTIALS.</remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class Credentials : SafeStructArrayIn<CredentialIn>
    {
        public Credentials(CredentialIn[] credentials) : base(credentials) { }
    }
}
