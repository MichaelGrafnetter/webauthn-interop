using Windows.Win32;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Hybrid Storage Linked Data Version Information.
    /// </summary>
    internal enum HybridStorageLinkedDataVersion : uint
    {
        /// <remarks>
        /// Corresponds to CTAPCBOR_HYBRID_STORAGE_LINKED_DATA_VERSION_1.
        /// </remarks>
        Version1 = PInvoke.CTAPCBOR_HYBRID_STORAGE_LINKED_DATA_VERSION_1,

        /// <summary>
        /// Current version
        /// </summary>
        /// <remarks>
        /// Corresponds to CTAPCBOR_HYBRID_STORAGE_LINKED_DATA_CURRENT_VERSION.
        /// </remarks>
        Current = PInvoke.CTAPCBOR_HYBRID_STORAGE_LINKED_DATA_CURRENT_VERSION
    }
}
