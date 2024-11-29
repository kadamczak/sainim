using ImageMagick;

namespace sainim.Models
{
    public class FrameSublayer(IMagickImage<ushort> fullLayerData) : BaseLayer(fullLayerData)
    {
        public string SpecialLabel { get; } = GetSpecialLabel(fullLayerData);

        protected const string Separator = "_";

        public static bool IsAnimationSublayer(IMagickImage layer) => layer.Label!.Contains(Separator);
        public static int GetFrameNumber(IMagickImage layer) => Int32.Parse(layer.Label!.Split(Separator).ElementAt(0));
        public static string GetSpecialLabel(IMagickImage layer) => layer.Label!.Split(Separator).ElementAt(1);
    }
}