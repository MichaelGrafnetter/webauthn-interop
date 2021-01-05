using System.Runtime.InteropServices;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Information about an RP Entity
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_RP_ENTITY_INFORMATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    internal class RelyingPartyInformation
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        private RelyingPartyInformationVersion Version { get; set; } = RelyingPartyInformationVersion.Current;

        /// <summary>
        /// Identifier for the RP.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Contains the friendly name of the Relying Party, such as "Acme Corporation", "Widgets Inc" or "Awesome Site".
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Optional URL pointing to RP's logo.
        /// </summary>
        public string Icon { get; set; }
    }
}
