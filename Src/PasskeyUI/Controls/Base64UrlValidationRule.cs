using System;
using System.Globalization;
using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class Base64UrlValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is not string text || string.IsNullOrWhiteSpace(text))
        {
            return ValidationResult.ValidResult;
        }

        try
        {
            Base64UrlConverter.FromBase64UrlString(text);
            return ValidationResult.ValidResult;
        }
        catch (FormatException)
        {
            return new ValidationResult(false, "Invalid Base64url value.");
        }
    }
}
