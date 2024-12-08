using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class ToggleRepeatCommand(AnimationStore animationStore) : CommandBase
    {
        private AnimationStore _animationStore { get; } = animationStore;

        public override void Execute(object? parameter)
        {
            _animationStore.Repeating = !_animationStore.Repeating;
        }
    }
}