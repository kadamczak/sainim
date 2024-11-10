using sainim.WPF.Bases;
using sainim.WPF.Commands.MenuBarCommands;
using sainim.WPF.Stores;
using System.Windows.Input;

namespace sainim.WPF.ViewModels
{
    public class MenuBarViewModel(OriginalImageStore originalImageStore) : ViewModelBase
    {
        public ICommand ImportPsdImage { get; } = new ImportPsdImageCommand(originalImageStore);
    }
}