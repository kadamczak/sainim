using sainim.WPF.Bases;
using sainim.WPF.Commands.PlayBarCommands;
using sainim.WPF.Stores;
using System.Windows.Input;

namespace sainim.WPF.ViewModels
{
    public class PlayBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        public AnimationStore AnimationStore { get; }

        public ICommand PlayAnimation { get; } = new PlayAnimationCommand();

        public PlayBarViewModel(OriginalImageStore originalImageStore, AnimationStore animationStore)
        {
            _originalImageStore = originalImageStore;
            AnimationStore = animationStore;
        }
    }
}