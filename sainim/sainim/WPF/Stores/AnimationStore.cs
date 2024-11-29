using sainim.Models;
using sainim.Models.Extensions;

namespace sainim.WPF.Stores
{
    public class AnimationStore
    {
        private readonly OriginalImageStore _originalImageStore;

        // Frame numbering
        public int MaxFrameCount { get; } = 255;
        public int CurrentFrameIndex { get; set; } = 0;

        // Play settings
        public int FrameRate { get; set; } = 24;
        public bool Repeating { get; set; } = false;
        public List<string> ActiveAnimationLayerTypes { get; } = [];

        // References to data
        public SortedDictionary<int, Frame> SortedFrames = [];

        // Events
        public event Action FramesModified;
        public void OnFramesModified() => FramesModified?.Invoke();

        public AnimationStore(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += LoadDefaultAnimation;
        }

        public void LoadDefaultAnimation()
        {
            SortedFrames.Clear();

            foreach(var (frame, i) in _originalImageStore.CurrentImage!.Frames.WithIndex())
            {
                SortedFrames.Add(i, frame);
            }

            OnFramesModified();
        }
    }
}