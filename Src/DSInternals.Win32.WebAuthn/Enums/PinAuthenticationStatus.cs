using System.Diagnostics.CodeAnalysis;

namespace DSInternals.Win32.WebAuthn
{
    /// <summary>Describes the PIN authentication or policy condition reported by an authenticator management operation.</summary>
    /// <remarks>These Windows management diagnostics supplement the operation result and remaining PIN retry count; they are not CTAP2 status codes.</remarks>
    [SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "Matches the native DWORD PIN diagnostic.")]
    public enum PinAuthenticationStatus : uint
    {
        /// <summary>No diagnostic was supplied.</summary>
        Unspecified = 0,

        /// <summary>A PIN is requested while attempts remain.</summary>
        PinRequired = 1,

        /// <summary>No PIN is configured.</summary>
        PinNotSet = 2,

        /// <summary>PIN verification failed.</summary>
        /// <remarks>The provider may also report this status for related user-verification failures.</remarks>
        PinVerificationFailed = 3,

        /// <summary>The PIN violates policy.</summary>
        PinPolicyViolation = 4,

        /// <summary>PIN use is blocked.</summary>
        PinBlocked = 5,

        /// <summary>A power cycle is required.</summary>
        PowerCycleRequired = 6
    }
}
