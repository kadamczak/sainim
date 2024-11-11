using ImageMagick;

namespace sainim.Models
{
    public abstract class BaseLayer(IMagickImage<ushort> fullLayerData)
    {
        public string MainLabel { get; } = fullLayerData.Label!;
        public IMagickImage<ushort> FullLayerData { get; } = fullLayerData;
        
        protected const string Separator = "_";
    }
}