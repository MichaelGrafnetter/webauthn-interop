﻿using System.Runtime.InteropServices;
using System.Text.Json.Serialization;
using DSInternals.Win32.WebAuthn.Interop;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>
    /// Information about an RP Entity
    /// </summary>
    /// <remarks>Corresponds to WEBAUTHN_RP_ENTITY_INFORMATION.</remarks>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public sealed class RelyingPartyInformation
    {
        /// <summary>
        /// Version of this structure, to allow for modifications in the future.
        /// </summary>
        internal RelyingPartyInformationVersion Version { get; set; } = RelyingPartyInformationVersion.Current;

        /// <summary>
        /// Identifier for the RP.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Contains the friendly name of the Relying Party, such as "Acme Corporation", "Widgets Inc" or "Awesome Site".
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Optional URL pointing to RP's logo.
        /// </summary>
        [JsonPropertyName("icon")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Icon { get; set; }
    }
}
