using System;
using System.Collections.Generic;
using DSInternals.Win32.WebAuthn.COSE;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

public record AuthenticatorPreset(string Name, Guid AaGuid, Algorithm DefaultAlgorithm)
{
    public override string ToString() => Name;

    public static IReadOnlyList<AuthenticatorPreset> KnownPresets { get; } =
    [
        new("YubiKey 5 NFC", Guid.Parse("cb69481e-8ff7-4039-93ec-0a2729a154a8"), Algorithm.ES256),
        new("Windows Hello", Guid.Parse("08987058-cadc-4b81-b6e1-30de50dcbe96"), Algorithm.RS256),
        new("Microsoft Authenticator (iOS)", Guid.Parse("90a3ccdf-635c-4729-a248-9b709135078f"), Algorithm.ES256),
        new("Microsoft Authenticator (Android)", Guid.Parse("de1e552d-db1d-4423-a619-566b625cdc84"), Algorithm.ES256),
        new("1Password", Guid.Parse("bada5566-a7aa-401f-bd96-45619a55120d"), Algorithm.ES256),
        new("Bitwarden", Guid.Parse("d548826e-79b4-db40-a3d8-11116f7e8349"), Algorithm.ES256),
    ];

    public static IList<KeyValuePair<Algorithm, string>> AlgorithmItems { get; } =
    [
        new(Algorithm.ES256, nameof(Algorithm.ES256)),
        new(Algorithm.ES384, nameof(Algorithm.ES384)),
        new(Algorithm.ES512, nameof(Algorithm.ES512)),
        new(Algorithm.RS256, nameof(Algorithm.RS256)),
        new(Algorithm.RS384, nameof(Algorithm.RS384)),
        new(Algorithm.RS512, nameof(Algorithm.RS512)),
        new(Algorithm.PS256, nameof(Algorithm.PS256)),
        new(Algorithm.PS384, nameof(Algorithm.PS384)),
        new(Algorithm.PS512, nameof(Algorithm.PS512)),
    ];
}
