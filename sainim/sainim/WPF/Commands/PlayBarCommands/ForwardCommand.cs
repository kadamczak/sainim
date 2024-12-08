using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class ForwardCommand(AnimationStore animationStore) : CommandBase
    {
        private AnimationStore AnimationStore { get; } = animationStore;

        public override void Execute(object? parameter)
        {
            if (AnimationStore.CurrentFrameIndex < AnimationStore.MaxFrameIndex)
                AnimationStore.CurrentFrameIndex++;
        }
    }
}