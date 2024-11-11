using sainim.Models.Extensions;
using System.Windows.Data;

namespace sainim.WPF.CustomUI.Converters
{
    /// <summary>
    /// The <c>StringToResourceConverter</c> class converts string key into a string taken from the loaded resource file.
    /// </summary>
    public class StringToResourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string valueStr = value.ToString();
            return (string.IsNullOrEmpty(valueStr)) ? "Unnamed".Resource() : valueStr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            => throw new NotImplementedException();
    }
}