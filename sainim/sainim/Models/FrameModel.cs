using ImageMagick;
using sainim.Models.Extensions;
using System.Windows.Media.Imaging;

namespace sainim.Models
{
    public class FrameModel
    {
        public int FrameNumber { get; }
        public BitmapSource FrameThumbnail { get; }
        public List<LayerModel> Layers { get; }

        public FrameModel(int frameNumber, List<LayerModel> layers, MagickImage background, uint maxThumbnailDimension = 250)
        {
            FrameNumber = frameNumber;
            Layers = layers;
            FrameThumbnail = this.MergeLayers().CreateThumbnail(background, maxThumbnailDimension);
        }

        public IMagickImage<ushort> MergeLayers() => new MagickImageCollection(Layers.Select(l => l.FullLayerData)).Merge();
    }
}