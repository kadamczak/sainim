using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class AnimationTimelineViewModel : ViewModelBase
    {
        private const int MaxFrameCount = 255;

        private readonly OriginalImageStore _originalImageStore;
        public ObservableCollection<string> TickLabels { get; } = [];
        public ObservableCollection<BitmapSource> FrameThumbnails { get; set; } = [];

        private int _currentFrameIndex;
        public int CurrentFrameIndex
        {
            get => _currentFrameIndex;
            set
            {
                if (_currentFrameIndex != value)
                {
                    _currentFrameIndex = value;
                    OnPropertyChanged();
                }
            }
        }

        public int MaxFrames { get; } = MaxFrameCount;
        public double TickWidth { get; } = 80;
        public double Width => MaxFrameCount * TickWidth;

        public AnimationTimelineViewModel(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += OnNewImageLoaded;

            var tickLabels = Enumerable.Range(1, MaxFrameCount).Select(n => n.ToString());
            foreach(var label in tickLabels)
            {
                TickLabels.Add(label);
            }
        }

        private void OnNewImageLoaded()
        {
            FrameThumbnails.Clear();

            for(int i = 0; i < 80; i++)
            {
                foreach (var frame in _originalImageStore.CurrentImage.Frames)
                {
                    FrameThumbnails.Add(frame.Thumbnail);
                }
            }
        }
    }
}