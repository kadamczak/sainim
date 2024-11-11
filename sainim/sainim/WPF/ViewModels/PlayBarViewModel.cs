using sainim.WPF.Bases;
using sainim.WPF.Commands.PlayBarCommands;
using System.Windows.Input;

namespace sainim.WPF.ViewModels
{
    public class PlayBarViewModel : ViewModelBase
    {
        public ICommand PlayAnimation { get; } = new PlayAnimationCommand();
    }
}