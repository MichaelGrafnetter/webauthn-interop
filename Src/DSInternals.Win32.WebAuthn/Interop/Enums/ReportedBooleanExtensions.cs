using System;

namespace DSInternals.Win32.WebAuthn.Interop
{
    /// <summary>Converts between reported setting states and nullable booleans.</summary>
    internal static class ReportedBooleanExtensions
    {
        /// <summary>Converts a nullable boolean to its native reported setting state.</summary>
        /// <param name="value">The setting, or null if unreported.</param>
        /// <returns>The corresponding native setting state.</returns>
        internal static ReportedBoolean ToReportedBoolean(this bool? value) => value switch
        {
            true => ReportedBoolean.True,
            false => ReportedBoolean.False,
            null => ReportedBoolean.Unreported
        };

        /// <summary>Converts a reported setting state to a nullable boolean.</summary>
        /// <param name="value">The native setting state.</param>
        /// <returns>The reported boolean, or null if unreported.</returns>
        /// <exception cref="NotSupportedException">The native setting state is unrecognized.</exception>
        internal static bool? ToNullableBoolean(this ReportedBoolean value) => value switch
        {
            ReportedBoolean.False => false,
            ReportedBoolean.Unreported => null,
            ReportedBoolean.True => true,
            _ => throw new NotSupportedException($"Unknown signed management boolean {value}.")
        };
    }
}
