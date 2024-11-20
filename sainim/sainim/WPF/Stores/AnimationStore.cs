using sainim.Models;

namespace sainim.WPF.Stores
{
    public class AnimationStore(OriginalImageStore originalImageStore)
    {
        private readonly OriginalImageStore _originalImageStore = originalImageStore;

        public int MaxFrameCount { get; } = 255;
        public int CurrentFrameIndex { get; set; } = 1;

        public int FrameRate { get; set; } = 24;
        public bool Repeating { get; set; } = false;

        public List<string> ActiveAnimationLayerTypes { get; } = [];
        public List<Frame?> Frames { get; set; } = [];

        public event Action FramesModified;
        public void OnFramesModified() => FramesModified?.Invoke();

        public void LoadDefaultAnimation()
        {
            // reference
            Frames = _originalImageStore.CurrentImage.Frames;
            Frames.Add(null);
            Frames.Add(_originalImageStore.CurrentImage.Frames.ElementAt(0));

            OnFramesModified();
        }
    }
}