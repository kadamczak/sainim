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

        public static BitmapSource CreateThumbnail(this IMagickImage<ushort> image, uint maxDimension, MagickImage? background = null)
        {
            var finalImage = MergeIfNotNull(image, background);
            var imageCopy = finalImage.ShrinkImageWithAspectRatio(maxDimension);
            return imageCopy.ToBitmapSource();
        }

        public static IMagickImage<ushort> MergeIfNotNull(this IMagickImage<ushort> image, IMagickImage<ushort>? background)
            => (background is null) ? image : new MagickImageCollection { background, image }.Merge();
    }
}