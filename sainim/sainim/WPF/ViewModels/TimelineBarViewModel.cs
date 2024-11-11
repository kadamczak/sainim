using sainim.WPF.Bases;

namespace sainim.WPF.ViewModels
{
    public class TimelineBarViewModel(AnimationTimelineViewModel animationTimelineViewModel) : ViewModelBase
    {
        public AnimationTimelineViewModel AnimationTimelineViewModel { get; } = animationTimelineViewModel;
    }
}