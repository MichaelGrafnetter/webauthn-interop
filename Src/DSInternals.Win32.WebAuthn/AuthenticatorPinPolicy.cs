namespace DSInternals.Win32.WebAuthn
{
    /// <summary>Describes an authenticator's PIN requirements, user-verification settings, and configuration limits.</summary>
    /// <remarks>A null property value means that the corresponding setting or limit was not reported.</remarks>
    public sealed class AuthenticatorPinPolicy
    {
        /// <summary>Minimum PIN length in Unicode code points.</summary>
        public uint? MinimumPinLength { get; init; }

        /// <summary>Whether the PIN must be changed before obtaining a PIN authentication token.</summary>
        public bool? ForcePinChangeRequired { get; init; }

        /// <summary>Whether authenticator configuration support was reported.</summary>
        public bool? AuthenticatorConfigSupported { get; init; }

        /// <summary>Whether minimum PIN length configuration support was reported.</summary>
        public bool? SetMinPinLengthSupported { get; init; }

        /// <summary>Maximum number of additional RP IDs accepted when configuring minimum PIN length disclosure.</summary>
        /// <remarks>Zero means that no additional RP IDs can be configured; preconfigured RP IDs are not included.</remarks>
        public uint? MaximumRpIds { get; init; }

        /// <summary>Whether enterprise attestation is enabled.</summary>
        public bool? EnterpriseAttestationEnabled { get; init; }

        /// <summary>Whether user verification is always required.</summary>
        public bool? AlwaysUvEnabled { get; init; }

        /// <summary>Maximum serialized large-blob bytes, including checksum.</summary>
        public uint? MaximumLargeBlobBytes { get; init; }

        /// <summary>Returns a single-line summary of minimum PIN length, forced PIN change, and required user verification.</summary>
        /// <returns>Named policy values, with unreported settings shown as N/A.</returns>
        public override string ToString() =>
            $"MinPinLength={MinimumPinLength?.ToString(System.Globalization.CultureInfo.InvariantCulture) ?? "N/A"}, " +
            $"ForcePinChange={ForcePinChangeRequired?.ToString() ?? "N/A"}, " +
            $"AlwaysUV={AlwaysUvEnabled?.ToString() ?? "N/A"}";
    }
}
