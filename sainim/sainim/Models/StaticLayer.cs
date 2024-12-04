using ImageMagick;
using sainim.Models.Enums;
using sainim.Models.Extensions;
using System.Windows.Media.Imaging;

namespace sainim.Models
{
    public class StaticLayer(IMagickImage<ushort> fullLayerData, Placement placement, MagickImage background, uint maxThumbnailDimension = 250) : BaseLayer(fullLayerData)
    {
        public Placement Placement { get; } = placement;
        public BitmapSource Thumbnail { get; } = fullLayerData.CreateThumbnail(maxThumbnailDimension, background);

        protected const string Separator = "_";
        public static bool IsStaticLayer(IMagickImage layer) => !layer.Label!.Contains(Separator);
    }
}