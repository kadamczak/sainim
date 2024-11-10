namespace sainim.Models.Extensions
{
    /// <summary>
    /// The <c>StringExtension</c> class's main purpose is to convert string keys into values taken from loaded resource file.
    /// </summary>
    public static class StringExtension
    {
        public static string Resource(this string key) => (string)System.Windows.Application.Current.Resources[key];
    }
}