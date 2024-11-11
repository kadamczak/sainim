using ImageMagick;
using sainim.Models.Extensions;
using System.Windows.Media.Imaging;

namespace sainim.Models
{
    public class StaticLayer(IMagickImage<ushort> fullLayerData, MagickImage background, uint maxThumbnailDimension = 250) : BaseLayer(fullLayerData)
    {
        public BitmapSource Thumbnail { get; } = fullLayerData.CreateThumbnail(background, maxThumbnailDimension);
        public static bool IsStaticLayer(IMagickImage layer) => !layer.Label!.Contains(Separator);
    }
}