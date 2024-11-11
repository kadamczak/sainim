using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class AnimationTimelineViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
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

        public int FrameCount { get; set; } = 20;

        public AnimationTimelineViewModel(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += OnNewImageLoaded;
        }

        private void OnNewImageLoaded()
        {
            foreach (var frame in _originalImageStore.CurrentImage.Frames)
            {
                FrameThumbnails.Add(frame.FrameThumbnail);
            }
        }
    }
}