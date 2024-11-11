using ImageMagick;

namespace sainim.Models
{
    public abstract class BaseLayer(IMagickImage<ushort> fullLayerData)
    {
        public string MainLabel { get; } = fullLayerData.Label!;
        public IMagickImage<ushort> FullLayerData { get; } = fullLayerData;

        protected const string FrameSeparator = "_";
        public static bool IsAnimationLayer(IMagickImage layer) => layer.Label!.Contains(FrameSeparator);
        public static bool IsStaticLayer(IMagickImage layer) => !IsAnimationLayer(layer);
        public static int GetFrameNumber(IMagickImage layer) => Int32.Parse(layer.Label!.Split(FrameSeparator)[0]);
    }
}