using ImageMagick;
using sainim.Models.Enums;
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

        public List<StaticLayer> GetElementsInPlacement(Placement placement) => StaticElements.Where(l => l.Placement == placement).ToList();

        public (List<StaticLayer> staticElements, List<Frame> frames) GetStaticElementsAndFrames()
            => (StaticElements, Frames);

        public (List<StaticLayer> backgroundElements, List<StaticLayer> foregroundElements, List<Frame> frames) GetBackgroundForegroundAndFrames()
            => (GetElementsInPlacement(Placement.Background), GetElementsInPlacement(Placement.Foreground), Frames);
    }
}