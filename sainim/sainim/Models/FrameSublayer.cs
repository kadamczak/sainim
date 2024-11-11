using ImageMagick;

namespace sainim.Models
{
    public class FrameSublayer(IMagickImage<ushort> fullLayerData) : BaseLayer(fullLayerData)
    {
        public string SpecialLayerLabel { get; } = GetSpecialLabel(fullLayerData);
        public static string GetSpecialLabel(IMagickImage layer) => layer.Label!.Split(FrameSeparator)[1];
    }
}