using sainim.WPF.Bases;
using sainim.WPF.Commands.MenuBarCommands;
using sainim.WPF.Stores;
using System.Windows.Input;

namespace sainim.WPF.ViewModels
{
    public class MainViewModel(ContentBarViewModel contentBarViewModel,
                         AnimationPreviewViewModel animationPreviewViewModel,
                         TimelineBarViewModel timelineBarViewModel,
                         PlayBarViewModel playBarViewModel,
                         MenuBarViewModel menuBarViewModel) : ViewModelBase
    {
        public ContentBarViewModel ContentBarViewModel { get; } = contentBarViewModel;
        public AnimationPreviewViewModel AnimationPreviewViewModel { get; } = animationPreviewViewModel;
        public TimelineBarViewModel TimelineBarViewModel { get; } = timelineBarViewModel;
        public PlayBarViewModel PlayBarViewModel { get; } = playBarViewModel;
        public MenuBarViewModel MenuBarViewModel { get; } = menuBarViewModel;
    }
}