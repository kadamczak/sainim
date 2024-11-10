using ImageMagick;
using sainim.Models.Extensions;
using System.Windows.Media.Imaging;

namespace sainim.Models
{
    public class LayerModel
    {
        public IMagickImage<ushort> FullLayerData { get; }
        public BitmapSource LayerThumbnail { get; }
        public string MainLabel { get; }
        public string? SpecialLayerLabel { get; }

        public LayerModel(IMagickImage<ushort> fullLayerData, uint maxThumbnailDimension)
        {
            FullLayerData = fullLayerData;
            LayerThumbnail = fullLayerData.CreateThumbnail(maxThumbnailDimension);
            MainLabel = fullLayerData.Label!;
            SpecialLayerLabel = GetSpecialLabel(fullLayerData);
        }

        private const string SPECIAL_SEPARATOR = "*";
        public string? GetSpecialLabel(IMagickImage layer) => layer.Label!.Contains(SPECIAL_SEPARATOR) ? layer.Label!.Split(SPECIAL_SEPARATOR)[1] : null;
    }
}
