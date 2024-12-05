using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class JumpToFirstFrameCommand(AnimationStore animationStore) : CommandBase
    {
        private AnimationStore _animationStore = animationStore;

        public override void Execute(object? parameter)
        {
            // find first full frame in _animationStore.AnimationSequence
            _animationStore.CurrentFrameIndex = _animationStore.AnimationSequence.ToList()
                                                                                 .FindIndex(frame => frame != null);
        }
    }
}