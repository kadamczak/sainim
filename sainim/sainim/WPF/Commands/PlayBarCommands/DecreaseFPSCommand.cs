using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class DecreaseFPSCommand(AnimationStore animationStore) : CommandBase
    {
        private AnimationStore _animationStore = animationStore;

        public override void Execute(object? parameter)
        {
            _animationStore.FrameRate -= 1;
        }
    }
}