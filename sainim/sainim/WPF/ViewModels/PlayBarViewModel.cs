using sainim.Models;
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

        public ICommand ToggleRepeat { get; }
        public ICommand JumpToFirstFrame { get; }
        public ICommand PlayAnimation { get; }
        public ICommand JumpToLastFrame { get; }
        public ICommand Forward { get; }
        public ICommand Back { get; }
        public ICommand IncreaseFPS { get; }
        public ICommand DecreaseFPS { get; }

        public PlayBarViewModel(OriginalImageStore originalImageStore, AnimationStore animationStore, FrameRenderer frameRenderer)
        {
            _originalImageStore = originalImageStore;
            AnimationStore = animationStore;

            ToggleRepeat = new ToggleRepeatCommand(animationStore);

            JumpToFirstFrame = new JumpToFirstFrameCommand(animationStore);
            PlayAnimation = new PlayAnimationCommand(originalImageStore, animationStore, frameRenderer);
            JumpToLastFrame = new JumpToLastFrameCommand(animationStore);
            Back = new BackCommand(animationStore);
            Forward = new ForwardCommand(animationStore);

            IncreaseFPS = new IncreaseFPSCommand(animationStore);
            DecreaseFPS = new DecreaseFPSCommand(animationStore);
        }
    }
}