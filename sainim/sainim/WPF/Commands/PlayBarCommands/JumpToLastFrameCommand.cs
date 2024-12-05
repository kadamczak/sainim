using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class JumpToLastFrameCommand(AnimationStore animationStore) : CommandBase
    {
        private AnimationStore _animationStore = animationStore;

        public override void Execute(object? parameter)
        {
            // find last full frame in _animationStore.AnimationSequence
            _animationStore.CurrentFrameIndex = _animationStore.AnimationSequence.ToList()
                                                                                 .FindLastIndex(frame => frame != null);
        }
    }
}