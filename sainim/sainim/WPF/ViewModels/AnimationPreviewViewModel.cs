using ImageMagick;
using sainim.Models;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class AnimationPreviewViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        private readonly AnimationStore _animationStore;

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

        public AnimationPreviewViewModel(OriginalImageStore originalImageStore, AnimationStore animationStore)
        {
            _originalImageStore = originalImageStore;
            _animationStore = animationStore;
            _animationStore.CurrentFrameIndexChanged += ChangePreview;
            _originalImageStore.NewImageLoaded += ChangePreview;
        }

        private void ChangePreview()
        {
            if (_originalImageStore.CurrentImage is null)              // Do not update when image is not loaded
                return;

            Frame? currentFrame = _animationStore.CurrentFrame;
            List<string> enabledLayerTypes = _animationStore.GetSelectedLayerTypes();

            if (currentFrame is null || enabledLayerTypes.Count == 0)       // If empty frame, show background
            {
                PreviewImage = _originalImageStore.CurrentImage.BackgroundBitmap;
                return;
            }

            BitmapSource? renderedImage = currentFrame.GetRenderedImage(enabledLayerTypes);
            renderedImage ??= RenderImage(currentFrame, enabledLayerTypes, _originalImageStore.CurrentImage.BackgroundMagick); // If requested image has not been rendered yet, render and save it now
            PreviewImage = renderedImage;
        }

        private BitmapSource? RenderImage(Frame currentFrame, List<string> enabledLayerTypes, MagickImage background)
        {
            //if (enabledLayerTypes.Contains("Static"))
            //{

            //}
            //enabledLayerTypes.Remove("Static");

            var enabledSublayerTypes = enabledLayerTypes.Where(l => l != "Static").ToArray();

            BitmapSource renderedImage = currentFrame.MergeLayers(background, enabledSublayerTypes).ToBitmapSource();
            currentFrame.RenderedImages[enabledLayerTypes] = renderedImage;
            return renderedImage;
        }
    }
}