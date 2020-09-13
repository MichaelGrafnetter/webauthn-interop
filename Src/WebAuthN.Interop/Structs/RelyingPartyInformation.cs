using System.Runtime.InteropServices;

namespace WebAuthN.Interop
{
    // TODO: Create a base class with Version, Id, Name and Icon.

    /// <summary>
    /// Information about an RP Entity
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_RP_ENTITY_INFORMATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public class RelyingPartyInformation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>Corresponds to WEBAUTHN_RP_ENTITY_INFORMATION_CURRENT_VERSION.</remarks>
        private const int CurrentVersion = 1;

        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        public int  Version;

        /// <summary>
        /// Identifier for the RP.
        /// </summary>
        public string Id;

        /// <summary>
        /// Contains the friendly name of the Relying Party, such as "Acme Corporation", "Widgets Inc" or "Awesome Site".
        /// </summary>
        public string Name;

        /// <summary>
        /// Optional URL pointing to RP's logo.
        /// </summary>
        public string Icon;
    }
}
