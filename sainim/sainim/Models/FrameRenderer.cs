using ImageMagick;
using sainim.Models.Enums;
using sainim.Models.Extensions;
using System.Windows.Media.Imaging;

namespace sainim.Models
{
    public class FrameRenderer
    {
        public BitmapSource? RenderFrame(Frame selectedFrame, OriginalImage originalImage, List<string> enabledLayerTypes)
        {
            var mergedImage = originalImage.BackgroundMagick;

            if (enabledLayerTypes.Contains("Background")) // Add background static elements
                mergedImage = MergeWithElements(mergedImage, originalImage, Placement.Background);

            mergedImage = MergeWithFrameIfFrameHasVisibleLayers(mergedImage, selectedFrame, enabledLayerTypes);

            if (enabledLayerTypes.Contains("Foreground")) // Add foreground static elements
                mergedImage = MergeWithElements(mergedImage, originalImage, Placement.Foreground);

            var renderedBitmap = mergedImage.ToBitmapSource();
            selectedFrame.RenderedBitmaps[enabledLayerTypes] = renderedBitmap;
            return renderedBitmap;
        }

        private IMagickImage<ushort> MergeWithElements(IMagickImage<ushort> baseImage, OriginalImage originalImage, Placement placementOfElements)
        {
            var elements = originalImage.GetElementsInPlacement(placementOfElements);
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