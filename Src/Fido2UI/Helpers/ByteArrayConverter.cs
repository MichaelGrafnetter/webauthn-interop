using System;
using System.Globalization;
using System.Windows.Data;
using DSInternals.Win32.WebAuthn.FIDO;


namespace DSInternals.Win32.WebAuthn.Fido2UI
{
    [ValueConversion(typeof(byte[]), typeof(string))]
    public class ByteArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] bytes = value as byte[];

            return bytes != null ? Base64UrlConverter.ToBase64UrlString(bytes) : string.Empty;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string base64url = value as string;

            return !string.IsNullOrEmpty(base64url) ? Base64UrlConverter.FromBase64UrlString(base64url) : null;
        }
    }
}
