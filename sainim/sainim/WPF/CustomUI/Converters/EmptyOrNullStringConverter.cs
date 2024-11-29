using sainim.Models.Extensions;
using System.Windows.Data;

namespace sainim.WPF.CustomUI.Converters
{
    public class EmptyOrNullStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string valueStr = value.ToString();
            string replacement = parameter.ToString();
            return (string.IsNullOrEmpty(valueStr)) ? replacement.Resource() : valueStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}