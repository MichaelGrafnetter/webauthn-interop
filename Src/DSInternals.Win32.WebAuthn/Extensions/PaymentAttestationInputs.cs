using System.Text.Json;
using System.Text.Json.Serialization;

namespace DSInternals.Win32.WebAuthn;

/// <summary>
/// Inputs for the Secure Payment Confirmation (SPC) <c>payment</c> WebAuthn extension during credential creation.
/// </summary>
/// <remarks>
/// During registration only <see cref="IsPayment"/> is meaningful. The other authentication-only fields documented in the
/// W3C Secure Payment Confirmation specification are exposed by <see cref="PaymentAssertionInputs"/>.
/// </remarks>
/// <see href="https://w3c.github.io/secure-payment-confirmation/#sctn-payment-extension"/>
public sealed class PaymentAttestationInputs
{
    /// <summary>
    /// Indicates that the extension is active. When the credential is created with this flag,
    /// the resulting credential is eligible to be used in a Secure Payment Confirmation flow by other origins.
    /// </summary>
    [JsonPropertyName("isPayment")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public bool IsPayment { get; set; }

    /// <summary>
    /// Deserializes a JSON string into a <see cref="PaymentAttestationInputs"/>.
    /// </summary>
    public static PaymentAttestationInputs? FromJson(string json)
    {
        return WebAuthnJsonModel.FromJson(json, WebAuthnJsonContext.Default.PaymentAttestationInputs);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return JsonSerializer.Serialize(this, WebAuthnJsonContext.Default.PaymentAttestationInputs);
    }
}
