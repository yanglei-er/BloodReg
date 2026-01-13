using System.Globalization;
using System.Windows.Data;

namespace BloodReg.Converters
{
    public sealed class StringToDonationVolumeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString()!.Replace("ml", string.Empty);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? s = value.ToString();
            if (!string.IsNullOrEmpty(s))
            {
                return s+"ml";
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
