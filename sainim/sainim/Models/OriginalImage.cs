namespace sainim.Models
{
    //16-bit psds for now
    public class OriginalImage(string imagePath, DateTime lastModified, List<StaticLayer> staticElements, List<Frame> frames)
    {
        public string ImagePath { get; } = imagePath;
        public DateTime LastModified { get; } = lastModified;
        public List<StaticLayer> StaticElements { get; } = staticElements;
        public List<Frame> Frames { get; } = frames;
    }
}