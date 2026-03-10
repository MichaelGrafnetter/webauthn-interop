using System;
using System.Globalization;
using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

public class GuidValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (string.IsNullOrWhiteSpace(value as string))
            return new ValidationResult(false, "This field is required.");

        if (!Guid.TryParse((string)value, out _))
            return new ValidationResult(false, "Invalid GUID format (e.g. 00000000-0000-0000-0000-000000000000).");

        return ValidationResult.ValidResult;
    }
}
