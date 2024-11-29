using ImageMagick;

namespace sainim.Models
{
    public abstract class BaseLayer(IMagickImage<ushort> fullLayerData)
    {
        public string Label { get; } = fullLayerData.Label!;
        public IMagickImage<ushort> Data { get; } = fullLayerData;
    }
}