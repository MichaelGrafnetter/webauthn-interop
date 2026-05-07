using System;
using System.Buffers.Text;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using DSInternals.Win32.WebAuthn;

namespace DSInternals.Win32.WebAuthn.PasskeyUI;

/// <summary>
/// Converts a byte array to a Base64Url encoded string and vice versa.
/// </summary>
internal sealed class ByteArrayToBase64UrlConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not byte[] bytes || bytes.Length == 0)
        {
            return null;
        }

        return Base64Url.EncodeToString(bytes);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string str || string.IsNullOrWhiteSpace(str))
        {
            return null;
        }

        try
        {
            return Base64UrlConverter.FromBase64UrlString(str);
        }
        catch (FormatException)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
