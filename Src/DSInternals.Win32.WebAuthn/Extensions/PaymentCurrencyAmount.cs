using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Currency and amount displayed during a Secure Payment Confirmation ceremony.
/// </summary>
/// <see href="https://www.w3.org/TR/payment-request/#paymentcurrencyamount-dictionary"/>
public sealed class PaymentCurrencyAmount
{
    /// <summary>
    /// ISO 4217 currency code.
    /// </summary>
    [JsonPropertyName("currency")]
    [JsonRequired]
    public required string Currency { get; set; }

    /// <summary>
    /// Decimal amount string (e.g. "5.00").
    /// </summary>
    [JsonPropertyName("value")]
    [JsonRequired]
    public required string Value { get; set; }

    /// <summary>
    /// Deserializes a JSON string into a <see cref="PaymentCurrencyAmount"/>.
    /// </summary>
    public static PaymentCurrencyAmount? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PaymentCurrencyAmount);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PaymentCurrencyAmount);
    }
}
