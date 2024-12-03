using ImageMagick;
using System.Windows.Media.Imaging;

namespace sainim.Models
{
    //16-bit psds for now
    public class OriginalImage(string imagePath, DateTime lastModified, MagickImage background, List<StaticLayer> staticElements, List<Frame> frames)
    {
        public string ImagePath { get; } = imagePath;
        public DateTime LastModified { get; } = lastModified;
        public MagickImage BackgroundMagick { get; } = background;
        public BitmapSource BackgroundBitmap { get; } = background.ToBitmapSource();
        public List<StaticLayer> StaticElements { get; } = staticElements;
        public List<Frame> Frames { get; } = frames;
    }
}