using System;

namespace DSInternals.Win32.WebAuthn.COSE;

/// <summary>
/// Provides parsing helpers for COSE elliptic curves.
/// </summary>
public static class EllipticCurveExtensions
{
    extension(EllipticCurve)
    {
        /// <summary>
        /// Parses a curve name into a COSE elliptic curve.
        /// </summary>
        public static EllipticCurve? TryParse(string? value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

#if NET5_0_OR_GREATER
            string normalizedValue = value.Trim().Replace("-", string.Empty, StringComparison.Ordinal).ToUpperInvariant();
#else
            string normalizedValue = value.Trim().Replace("-", string.Empty).ToUpperInvariant();
#endif
            return normalizedValue switch
            {
                "P256" => EllipticCurve.P256,
                "P384" => EllipticCurve.P384,
                "P521" => EllipticCurve.P521,
                "P256K" or "SECP256K1" => EllipticCurve.P256K,
                "ED25519" => EllipticCurve.Ed25519,
                "ED448" => EllipticCurve.Ed448,
                "X25519" => EllipticCurve.X25519,
                "X448" => EllipticCurve.X448,
                _ => null
            };
        }
    }
}
