namespace DSInternals.Win32.WebAuthn
{
    /// <summary>Describes a discoverable credential's identifier, relying party, and user account.</summary>
    /// <remarks>The private display API does not supply a user handle, RP display name, or backup flags.</remarks>
    public sealed class DiscoverableCredentialDetails
    {
        /// <summary>The opaque credential identifier.</summary>
        public required byte[] CredentialId { get; init; }

        /// <summary>The relying party identifier, if supplied.</summary>
        public string? RelyingPartyId { get; init; }

        /// <summary>The account name, if supplied.</summary>
        public string? UserName { get; init; }

        /// <summary>The account display name, if supplied.</summary>
        public string? UserDisplayName { get; init; }
    }
}
