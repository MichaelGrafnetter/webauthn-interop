#if !NET9_0_OR_GREATER
using System;

namespace System.Security.Cryptography.X509Certificates;

/// <summary>
/// Provides compatibility members for APIs added to newer .NET versions.
/// </summary>
internal static class X509CertificateLoader
{
    /// <summary>
    /// Loads an X.509 certificate from raw DER/BER encoded bytes.
    /// </summary>
    /// <param name="data">The raw certificate bytes.</param>
    /// <returns>The loaded certificate.</returns>
    public static X509Certificate2 LoadCertificate(byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        return new X509Certificate2(data);
    }
}
#endif
