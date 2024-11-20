using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class TimelineBarViewModel : ViewModelBase
    {
        public AnimationStore AnimationStore { get; set; }
        private readonly OriginalImageStore _originalImageStore;

        public ObservableCollection<string> TickLabels { get; } = [];
        public ObservableCollection<BitmapSource?> FrameThumbnails { get; set; } = [];

        public int MaxFrameCount => AnimationStore.MaxFrameCount;
        public double TickWidth { get; } = 80;
        public double Width => MaxFrameCount * TickWidth;

        public TimelineBarViewModel(AnimationStore animationStore, OriginalImageStore originalImageStore)
        {
            AnimationStore = animationStore;
            _originalImageStore = originalImageStore;
            AnimationStore.FramesModified += OnFramesModified;

            CreateTickLabels();
        }

        private void CreateTickLabels()
        {
            var tickLabels = Enumerable.Range(1, MaxFrameCount).Select(n => n.ToString());
            foreach (var label in tickLabels)
            {
                TickLabels.Add(label);
            }
        }

        private void OnFramesModified()
        {
            FrameThumbnails.Clear();
            
            foreach(var frame in AnimationStore.Frames)
            {
                FrameThumbnails.Add(frame.Thumbnail);
            }
        }
    }
}