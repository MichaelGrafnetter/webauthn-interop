#pragma warning disable CA1028 // Enum Storage should be Int32

namespace DSInternals.Win32.WebAuthn.ADStore
{
    /// <summary>
    /// Key Credential Link Blob Structure Version
    /// </summary>
    /// <see>https://msdn.microsoft.com/en-us/library/mt220501.aspx</see>
    public enum KeyCredentialVersion : uint
    {
        /// <summary>
        /// Legacy Blob Structure
        /// </summary>
        Version0 = 0,

        /// <summary>
        /// Blob Structure Version 1
        /// </summary>
        Version1 = 0x00000100,

        /// <summary>
        /// Blob Structure Version 2
        /// </summary>
        /// <remarks>Corresponds to KEY_CREDENTIAL_LINK_VERSION_2.</remarks>
        Version2 = 0x00000200,
    }
}
