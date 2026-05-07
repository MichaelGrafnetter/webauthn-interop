using System;
using System.Globalization;
using System.Windows.Data;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

internal sealed class GuidConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is Guid guid ? guid.ToString() : string.Empty;

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => Guid.TryParse(value as string, out var guid) ? guid : Binding.DoNothing;
}
