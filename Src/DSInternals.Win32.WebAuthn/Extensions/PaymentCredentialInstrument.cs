using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Payment instrument descriptor displayed during a Secure Payment Confirmation ceremony.
/// </summary>
/// <see href="https://w3c.github.io/secure-payment-confirmation/#dictdef-paymentcredentialinstrument"/>
public sealed class PaymentCredentialInstrument
{
    /// <summary>
    /// Human-readable instrument label.
    /// </summary>
    [JsonPropertyName("displayName")]
    [JsonRequired]
    public required string DisplayName { get; set; }

    /// <summary>
    /// URL of the instrument icon. May be a data: URL.
    /// </summary>
    [JsonPropertyName("icon")]
    [JsonRequired]
    public required string Icon { get; set; }

    /// <summary>
    /// Optional additional details for the instrument.
    /// </summary>
    [JsonPropertyName("details")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Details { get; set; }

    /// <summary>
    /// Whether the icon must be successfully fetched and shown for the ceremony to continue.
    /// </summary>
    [JsonPropertyName("iconMustBeShown")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? IconMustBeShown { get; set; }

    /// <summary>
    /// Deserializes a JSON string into a <see cref="PaymentCredentialInstrument"/>.
    /// </summary>
    public static PaymentCredentialInstrument? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PaymentCredentialInstrument);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PaymentCredentialInstrument);
    }
}
