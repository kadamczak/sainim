using ImageMagick;
using sainim.Models;
using sainim.Models.Enums;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class AnimationPreviewViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        private readonly AnimationStore _animationStore;
        private readonly FrameRenderer _frameRenderer;

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

        public AnimationPreviewViewModel(OriginalImageStore originalImageStore, AnimationStore animationStore, FrameRenderer frameRenderer)
        {
            _originalImageStore = originalImageStore;
            _animationStore = animationStore;
            _frameRenderer = frameRenderer;

            _animationStore.AnimationDataLoaded += ChangePreview;
            _animationStore.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_animationStore.CurrentFrameIndex))
                    ChangePreview();
            };
            _animationStore.SelectableLayerTypes.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_animationStore.SelectableLayerTypes.LayerTypes))
                    ChangePreview();
            };
        }

        private void ChangePreview()
        {
            if (_originalImageStore.CurrentImage is null) // Do not update when image is not loaded
                return;

            Frame? currentFrame = _animationStore.CurrentFrame;
            List<string> enabledLayerTypes = _animationStore.SelectableLayerTypes.GetSelectedLayerTypes();

            if (currentFrame is null || enabledLayerTypes.Count == 0)       // If empty frame, show background
            {
                PreviewImage = _originalImageStore.CurrentImage.BackgroundBitmap;
                return;
            }

            BitmapSource? renderedFrame = currentFrame.GetRenderedBitmap(enabledLayerTypes);

            // If requested image has not been rendered yet, render and save it now
            renderedFrame ??= _frameRenderer.RenderFrame(currentFrame, _originalImageStore.CurrentImage, enabledLayerTypes);
            PreviewImage = renderedFrame;
        }
    }
}