using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;

namespace sainim.WPF.ViewModels
{
    public class TimelineBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore; // original image - readonly
        public AnimationStore AnimationStore { get; set; }       // animation sequence - editable through Timeline Bar
                                                                 // edits of animation sequence can come from Timeline Bar
                                                                 // and from other sources

        // Counts and widths
        public double TickWidth { get; } = 80;
        public double Width => (AnimationStore.FrameSpaceCount + 1) * TickWidth;

        // Observable collections
        public ObservableCollection<string> TickLabels { get; } = [];
        public ObservableCollection<string> TimeLabels { get; } = [];

        public TimelineBarViewModel(AnimationStore animationStore, OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            AnimationStore = animationStore;
            AnimationStore.PropertyChanged += (s, e) => { if (e.PropertyName == nameof(AnimationStore.FrameRate)) UpdateTimeLabels(); };

            CreateTickLabels();
            UpdateTimeLabels();
        }
        private void UpdateTimeLabels()
        {
            double millisecondsBetweenFrames = 1000 / AnimationStore.FrameRate;

            var timeLabels = Enumerable.Range(0, AnimationStore.FrameSpaceCount)
                .Select(n => TimeSpan.FromMilliseconds(n * millisecondsBetweenFrames).TotalSeconds.ToString("F2"));

            TimeLabels.Clear();
            TimeLabels.AddRange(timeLabels);
        }

        private void CreateTickLabels()
        {
            var tickLabels = Enumerable.Range(0, AnimationStore.FrameSpaceCount).Select(n => n.ToString());
            TickLabels.AddRange(tickLabels);
        }
    }
}