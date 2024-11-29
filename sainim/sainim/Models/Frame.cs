using ImageMagick;
using sainim.Models.Extensions;
using System.Windows.Media.Imaging;

namespace sainim.Models
{
    public class Frame
    {
        public int Index { get; }
        public BitmapSource Thumbnail { get; }
        public List<FrameSublayer> Sublayers { get; }

        public Frame(int index, List<FrameSublayer> sublayers, MagickImage background, uint maxThumbnailDimension = 250)
        {
            Index = index;
            Sublayers = sublayers;
            Thumbnail = this.MergeLayers().CreateThumbnail(maxThumbnailDimension, background);
        }

        public IMagickImage<ushort> MergeLayers() => new MagickImageCollection(Sublayers.Select(l => l.Data)).Merge();

        // merge only layers with special labels that belong to a specified subset
        public IMagickImage<ushort> MergeLayers(params string[] specialLabels)
            => new MagickImageCollection(Sublayers.Where(l => specialLabels.Contains(l.SpecialLabel)).Select(l => l.Data)).Merge();
    }
}