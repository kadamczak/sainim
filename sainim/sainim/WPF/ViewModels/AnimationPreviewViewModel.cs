using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class AnimationPreviewViewModel : ViewModelBase
    {
        private readonly AnimationStore _animationStore;

        private BitmapSource? _previewFrame = null;
        public BitmapSource? PreviewFrame
        {
            get => _previewFrame;
            set
            {
                _previewFrame = value;
                OnPropertyChanged(nameof(PreviewFrame));
            }
        }

        public AnimationPreviewViewModel(AnimationStore animationStore)
        {
            _animationStore = animationStore;
            _animationStore.CurrentFrameIndexChanged += ChangePreview;
        }

        private void ChangePreview()
        {
            var currentFrame = _animationStore.CurrentFrame;

            if (currentFrame is null)
            {
                PreviewFrame = null;
                return;
            }

            PreviewFrame = currentFrame.Thumbnail;
        }
    }
}