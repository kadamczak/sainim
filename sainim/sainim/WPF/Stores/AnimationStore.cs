using sainim.Models;
using sainim.Models.Extensions;

namespace sainim.WPF.Stores
{
    public class AnimationStore
    {
        private readonly OriginalImageStore _originalImageStore;

        // Frame numbering
        public int MinFrameIndex { get; } = 1;
        public int MaxFrameIndex { get; } = 255;
        public int AvailableFrameSpaces => MaxFrameIndex - MinFrameIndex + 1;
        public int CurrentFrameIndex { get; set; } = 1;

        // Play settings
        public int FrameRate { get; set; } = 12;
        public bool Repeating { get; set; } = false;
        public List<string> ActiveSpecialLayerTypes { get; } = [];

        // References to data
        public SortedDictionary<int, Frame> FrameSequence = [];

        // Events
        public event Action FrameSequenceModified;
        public void OnFrameSequenceModified() => FrameSequenceModified?.Invoke();

        public AnimationStore(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += LoadDefaultAnimation;
        }

        public void LoadDefaultAnimation()
        {
            FrameSequence.Clear();

            foreach(var (frame, i) in _originalImageStore.CurrentImage!.Frames.WithIndex(indexOffset: MinFrameIndex))
                FrameSequence.Add(i, frame);

            OnFrameSequenceModified();
        }
    }
}