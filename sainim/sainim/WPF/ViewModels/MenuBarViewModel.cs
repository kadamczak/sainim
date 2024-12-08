using sainim.Models;
using sainim.WPF.Bases;
using sainim.WPF.Commands.MenuBarCommands;
using sainim.WPF.Commands.SettingsCommands;
using sainim.WPF.Helpers;
using sainim.WPF.Stores;
using System.Windows.Input;

namespace sainim.WPF.ViewModels
{
    public class MenuBarViewModel(OriginalImageFactory originalImageFactory,
                                  OriginalImageStore originalImageStore,
                                  AnimationStore animationStore,
                                  MessageBoxHelpers messageBoxHelpers) : ViewModelBase
    {
        public ICommand ImportPsdImage { get; } = new ImportPsdImageCommand(originalImageFactory, originalImageStore, messageBoxHelpers);
        public ICommand ExportGif { get; } = new ExportGifCommand(originalImageStore, animationStore, messageBoxHelpers);
        public ICommand ReloadPsdImage { get; } = new ReloadPsdImageCommand(originalImageFactory, originalImageStore, messageBoxHelpers);

        public ICommand ChangeLanguageToEnglish { get; } = new LoadStringResourcesCommand("en-US");
        public ICommand ChangeLanguageToPolish { get; } = new LoadStringResourcesCommand("pl");

        public ICommand OpenCredits { get; } = new OpenCreditsCommand();
    }
}