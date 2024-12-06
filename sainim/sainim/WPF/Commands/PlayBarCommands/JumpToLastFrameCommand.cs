using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class JumpToLastFrameCommand(AnimationStore animationStore) : CommandBase
    {
        private AnimationStore _animationStore = animationStore;

        public override void Execute(object? parameter)
        {
            int lastIndex = _animationStore.FindLastFullFrameIndex();

            if (lastIndex == -1)
                return;
               
            _animationStore.CurrentFrameIndex = lastIndex;
        }
    }
}