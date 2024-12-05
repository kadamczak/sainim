using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class PlayAnimationCommand : CommandBase
    {
        private AnimationStore _animationStore { get; }

        public PlayAnimationCommand(AnimationStore animationStore)
        {
            this._animationStore = animationStore;
        }

        public override void Execute(object? parameter)
        {
            int lastFrameIndex = _animationStore.AnimationSequence.ToList().FindLastIndex(frame => frame != null);

            if (_animationStore.CurrentFrameIndex == lastFrameIndex)
                return;

            float milisecondsBetweenFrames = 1000 / _animationStore.FrameRate;
        }
    }
}