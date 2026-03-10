using System.Globalization;
using System.Windows.Controls;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

public class NotEmptyValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (string.IsNullOrWhiteSpace(value as string))
            return new ValidationResult(false, "This field is required.");

        return ValidationResult.ValidResult;
    }
}
