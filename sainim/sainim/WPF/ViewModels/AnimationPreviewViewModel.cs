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
            if (_originalImageStore.CurrentImage is null) // Do not update when image is not loaded
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
            var mergedImage = background;

            if (enabledLayerTypes.Contains("Background")) // Add background static elements
                mergedImage = MergeWithElements(mergedImage, Placement.Background);

            mergedImage = MergeWithFrameIfFrameHasVisibleLayers(mergedImage, currentFrame, enabledLayerTypes);

            if (enabledLayerTypes.Contains("Foreground")) // Add foreground static elements
                mergedImage = MergeWithElements(mergedImage, Placement.Foreground);

            var renderedBitmap = mergedImage.ToBitmapSource();
            currentFrame.RenderedImages[enabledLayerTypes] = renderedBitmap;
            return renderedBitmap;
        }

        private IMagickImage<ushort> MergeWithElements(IMagickImage<ushort> baseImage, Placement placementOfElements)
        {
            var elements = _originalImageStore.CurrentImage!.GetElementsInPlacement(placementOfElements);
            var elementsMerged = new MagickImageCollection(elements.Select(e => e.Data)).MergeWithTransparentBackground();
            return new MagickImageCollection { baseImage, elementsMerged! }.Merge();
        }

        private IMagickImage<ushort> MergeWithFrameIfFrameHasVisibleLayers(IMagickImage<ushort> baseImage, Frame frame, List<string> enabledLayerTypes)
        {
            var enabledSublayerTypes = enabledLayerTypes.Where(l => l != "Background" && l != "Foreground").ToArray();
            var mergedFrame = frame.GetMergedSublayers(enabledSublayerTypes);

            return (mergedFrame is null) ? baseImage : new MagickImageCollection { baseImage, mergedFrame }.Merge();
        }
    }
}