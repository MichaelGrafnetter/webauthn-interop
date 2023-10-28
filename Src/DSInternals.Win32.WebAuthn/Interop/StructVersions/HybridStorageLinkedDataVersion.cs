namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>
    /// Hybrid Storage Linked Data Version Information.
    /// </summary>
    internal enum HybridStorageLinkedDataVersion : int
    {
        /// <remarks>
        /// Corresponds to CTAPCBOR_HYBRID_STORAGE_LINKED_DATA_VERSION_1.
        /// </remarks>
        Version1 = ApiConstants.HybridStorageLinkedDataVersion1,

        /// <summary>
        /// Current version
        /// </summary>
        /// <remarks>
        /// Corresponds to CTAPCBOR_HYBRID_STORAGE_LINKED_DATA_CURRENT_VERSION.
        /// </remarks>
        Current = ApiConstants.HybridStorageLinkedDataCurrentVersion
    }
}
