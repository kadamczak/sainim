using Microsoft.Extensions.Configuration;
using sainim.WPF.Bases;
using sainim.WPF.Commands.MenuBarCommands;
using sainim.WPF.Commands.SettingsCommands;
using sainim.WPF.Stores;
using System.Windows.Input;

namespace sainim.WPF.ViewModels
{
    public class MenuBarViewModel(OriginalImageStore originalImageStore) : ViewModelBase
    {
        public ICommand ImportPsdImage { get; } = new ImportPsdImageCommand(originalImageStore);

        public ICommand ChangeLanguageToEnglish { get; } = new LoadStringResourcesCommand("en-US");
        public ICommand ChangeLanguageToPolish { get; } = new LoadStringResourcesCommand("pl");
    }
}