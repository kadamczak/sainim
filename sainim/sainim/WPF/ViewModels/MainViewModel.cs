using sainim.WPF.Bases;

namespace sainim.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ContentBarViewModel ContentBarViewModel { get; }
        public AnimationPreviewViewModel AnimationPreviewViewModel { get; }
        public TimelineBarViewModel TimelineBarViewModel { get; }
        public PlayBarViewModel PlayBarViewModel { get; }
        public MenuBarViewModel MenuBarViewModel { get; }


        public MainViewModel(ContentBarViewModel contentBarViewModel,
                             AnimationPreviewViewModel animationPreviewViewModel,
                             TimelineBarViewModel timelineBarViewModel,
                             PlayBarViewModel playBarViewModel,
                             MenuBarViewModel menuBarViewModel)
        {
            ContentBarViewModel = contentBarViewModel;
            AnimationPreviewViewModel = animationPreviewViewModel;
            TimelineBarViewModel = timelineBarViewModel;
            PlayBarViewModel = playBarViewModel;
            MenuBarViewModel = menuBarViewModel;
        }

    }
}
