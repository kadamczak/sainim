using sainim.Models;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Windows.Threading;

namespace sainim.WPF.Commands.PlayBarCommands
{
    public class PlayAnimationCommand : CommandBase
    {
        private OriginalImageStore _originalImageStore { get; }
        private AnimationStore _animationStore { get; }
        private FrameRenderer _frameRenderer { get; }

        public PlayAnimationCommand(OriginalImageStore originalImageStore, AnimationStore animationStore, FrameRenderer frameRenderer)
        {
            _originalImageStore = originalImageStore;
            _animationStore = animationStore;
            _frameRenderer = frameRenderer;
        }

        public override void Execute(object? parameter)
        {
            int firstFrameIndex = _animationStore.FindFirstFullFrameIndex();
            int lastFrameIndex = _animationStore.FindLastFullFrameIndex();

            // Safeguards
            if (!AnimationCanPlay(firstFrameIndex, lastFrameIndex))
                return;

            // Render frames that have not been rendered yet
            var enabledLayerTypes = _animationStore.SelectableLayerTypes.GetSelectedLayerTypes();
            RenderMissingFrames(enabledLayerTypes, firstFrameIndex, lastFrameIndex);

            // Set clocks to change frames
            float millisecondsBetweenFrames = 1000 / _animationStore.FrameRate;

            // Using millisecondsBetweenFrames intervals, framesLeftToPlay times, raise the CurrentFrameIndex by 1.
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(millisecondsBetweenFrames)
            };

            timer.Tick += (s, e) =>
            {
                if (_animationStore.CurrentFrameIndex < lastFrameIndex)
                    _animationStore.CurrentFrameIndex++;
                else
                    timer.Stop();
            };

            timer.Start();
        }

        private bool AnimationCanPlay(int firstFrameIndex, int lastFrameIndex)
            => (firstFrameIndex != -1) && (firstFrameIndex != lastFrameIndex) && (_animationStore.CurrentFrameIndex != lastFrameIndex);

        private void RenderMissingFrames(List<string> enabledLayerTypes, int firstFrameIndex, int lastFrameIndex)
        {
            var framesToRender = _animationStore.AnimationSequence.Skip(firstFrameIndex).Take(lastFrameIndex - firstFrameIndex + 1);

            foreach (var frame in framesToRender)
            {
                if (frame is null)
                    continue;
                if (frame!.GetRenderedBitmap(enabledLayerTypes) is not null)
                    continue;

                frame!.RenderedBitmaps[enabledLayerTypes] = _frameRenderer.RenderFrame(frame!, _originalImageStore.CurrentImage!, enabledLayerTypes);
            }
        }

    }
}