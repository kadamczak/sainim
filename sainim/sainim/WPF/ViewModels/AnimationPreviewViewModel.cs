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
            _animationStore.AnimationDataLoaded += ChangePreview;
            _animationStore.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_animationStore.CurrentFrameIndex))
                    ChangePreview();
            };
        }

        private void ChangePreview()
        {
            if (_originalImageStore.CurrentImage is null)              // Do not update when image is not loaded
                return;

            Frame? currentFrame = _animationStore.CurrentFrame;
            List<string> enabledLayerTypes = _animationStore.SelectableLayerTypes.GetSelectedLayerTypes();

            if (currentFrame is null || enabledLayerTypes.Count == 0)       // If empty frame, show background
            {
                PreviewImage = _originalImageStore.CurrentImage.BackgroundBitmap;
                return;
            }

            BitmapSource? renderedImage = currentFrame.GetRenderedImage(enabledLayerTypes);
            renderedImage ??= RenderImage(currentFrame, enabledLayerTypes, _originalImageStore.CurrentImage.BackgroundMagick); // If requested image has not been rendered yet, render and save it now
            PreviewImage = renderedImage;
        }

        private BitmapSource? RenderImage(Frame currentFrame, List<string> enabledLayerTypes, IMagickImage<ushort> background)
        {
            var enabledSublayerTypes = enabledLayerTypes.Where(l => l != "Static").ToArray();
            IMagickImage<ushort> mergedFrame = currentFrame.MergeLayers(enabledSublayerTypes);

            //var mergedLayersBeforeFrame = background;

            // try composite instead of merge

            //if (enabledLayerTypes.Contains("Static"))
            //{
            //    var backgroundElements = _originalImageStore.CurrentImage!.GetElementsInPlacement(Placement.Background);
            //    mergedLayersBeforeFrame = new MagickImageCollection(backgroundElements.Select(e => e.Data)).Merge();
            //    mergedFrame = new MagickImageCollection { background, mergedLayersBeforeFrame, mergedFrame }.Merge();
            //}

            //if (enabledLayerTypes.Contains("Static"))
            //{
            //    var foregroundElements = _originalImageStore.CurrentImage!.GetElementsInPlacement(Placement.Foreground);
            //    var mergedLayersAfterFrame = new MagickImageCollection(foregroundElements.Select(e => e.Data)).Merge();
            //    mergedFrame = new MagickImageCollection{ mergedFrame, mergedLayersAfterFrame }.Merge();
            //}

            var renderedBitmap = mergedFrame.ToBitmapSource();

            currentFrame.RenderedImages[enabledLayerTypes] = renderedBitmap;
            return renderedBitmap;
        }
    }
}