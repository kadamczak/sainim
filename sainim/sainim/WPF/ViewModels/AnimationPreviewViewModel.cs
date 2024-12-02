using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class AnimationPreviewViewModel : ViewModelBase
    {
        private readonly AnimationStore _animationStore;
        private readonly RenderedImagesStore _renderedImagesStore;

        private BitmapSource? _previewImage = null;
        public BitmapSource? PreviewImage
        {
            get => _previewImage;
            set
            {
                _previewImage = value;
                OnPropertyChanged(nameof(PreviewImage));
            }
        }

        public AnimationPreviewViewModel(AnimationStore animationStore, RenderedImagesStore renderedImagesStore)
        {
            _animationStore = animationStore;
            _renderedImagesStore = renderedImagesStore;

            _animationStore.CurrentFrameIndexChanged += ChangePreview;
        }

        private void ChangePreview()
        {
            var currentFrame = _animationStore.CurrentFrame;

            if (currentFrame is null)
            {
                PreviewImage = null;
                return;
            }

            PreviewImage = currentFrame.Thumbnail;
        }
    }
}