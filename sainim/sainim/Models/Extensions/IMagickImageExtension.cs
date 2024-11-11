using ImageMagick;
using System.Windows.Media.Imaging;

namespace sainim.Models.Extensions
{
    public static class IMagickImageExtension
    {
        public static IMagickImage<ushort> ShrinkImageWithAspectRatio(this IMagickImage<ushort> image,
                                                                      uint maxDimension)
        {
            var imageCopy = image.Clone();

            if (image.Width <= maxDimension && image.Height <= maxDimension)
                return imageCopy;

            var size = new MagickGeometry(maxDimension, maxDimension) { IgnoreAspectRatio = false };

            imageCopy.Resize(size);
            return imageCopy;
        }

        public static BitmapSource CreateThumbnail(this IMagickImage<ushort> image, MagickImage background, uint maxDimension)
        {
            var imageMergedWithBackground = new MagickImageCollection { background, image }.Merge();
            var imageCopy = imageMergedWithBackground.ShrinkImageWithAspectRatio(maxDimension);
            return imageCopy.ToBitmapSource();
        }
    }
}