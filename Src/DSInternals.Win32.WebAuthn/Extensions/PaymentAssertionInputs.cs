using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Inputs for the Secure Payment Confirmation (SPC) <c>payment</c> WebAuthn extension during assertion.
/// </summary>
/// <see href="https://w3c.github.io/secure-payment-confirmation/#sctn-payment-extension"/>
public sealed class PaymentAssertionInputs
{
    /// <summary>
    /// Indicates that the extension is active.
    /// </summary>
    [JsonPropertyName("isPayment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsPayment { get; set; }

    /// <summary>
    /// Relying Party identifier of the credential being asserted.
    /// </summary>
    [JsonPropertyName("rpId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? RpId { get; set; }

    /// <summary>
    /// Origin of the top-level frame initiating the payment.
    /// </summary>
    [JsonPropertyName("topOrigin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TopOrigin { get; set; }

    /// <summary>
    /// Display name of the payee shown to the user.
    /// </summary>
    [JsonPropertyName("payeeName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PayeeName { get; set; }

    /// <summary>
    /// Origin of the payee shown to the user.
    /// </summary>
    [JsonPropertyName("payeeOrigin")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PayeeOrigin { get; set; }

    /// <summary>
    /// Total amount of the transaction shown to the user.
    /// </summary>
    [JsonPropertyName("total")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaymentCurrencyAmount? Total { get; set; }

    /// <summary>
    /// Instrument descriptor shown to the user.
    /// </summary>
    [JsonPropertyName("instrument")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaymentCredentialInstrument? Instrument { get; set; }

    /// <summary>
    /// Deserializes a JSON string into a <see cref="PaymentAssertionInputs"/>.
    /// </summary>
    public static PaymentAssertionInputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PaymentAssertionInputs);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PaymentAssertionInputs);
    }
}
