using Microsoft.Extensions.Configuration;
using sainim.WPF.Bases;
using System.Text.RegularExpressions;
using System.Windows;

namespace sainim.WPF.Commands.SettingsCommands
{
    /// <summary>
    /// The <c>LoadStringResourcesCommand</c> class changes used string resource file.
    /// </summary>
    public class LoadStringResourcesCommand : CommandBase
    {
        private readonly ResourceDictionary _applicationResources = Application.Current.Resources;
        private const string StringFolderPath = @".\Resources\Strings";
        private readonly Regex _stringResourceRegex = new(@$"StringResources\..+\.xaml$");
        private readonly string _chosenLanguage;

        public LoadStringResourcesCommand(string chosenLanguage)
        {
            _chosenLanguage = chosenLanguage;
        }

        public override void Execute(object? parameter = null)
        {
            RemovePreviousStringResourceDictionary();
            ResourceDictionary stringResourceDictionary = SelectStringResourceDictionary();
            _applicationResources.MergedDictionaries.Add(stringResourceDictionary);
        }

        private void RemovePreviousStringResourceDictionary()
        {
            var stringResourceDictionary = _applicationResources.MergedDictionaries.FirstOrDefault(d => _stringResourceRegex.IsMatch(d.Source.OriginalString));

            if (stringResourceDictionary is null)
                return;

            _applicationResources.Remove(stringResourceDictionary);
        }

        private ResourceDictionary SelectStringResourceDictionary()
        {
            string fileName = @$"{StringFolderPath}\StringResources.{_chosenLanguage}.xaml";
            return new ResourceDictionary() { Source = new Uri(fileName, UriKind.Relative) };
        }
    }
}
