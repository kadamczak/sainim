using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class JumpToFirstFrameCommand(AnimationStore animationStore) : CommandBase
    {
        private AnimationStore _animationStore = animationStore;

        public override void Execute(object? parameter)
        {
            int firstIndex = _animationStore.FindFirstFullFrameIndex();

            if (firstIndex == -1)
                return;

            _animationStore.CurrentFrameIndex = firstIndex;
        }
    }
}