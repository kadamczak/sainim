using ImageMagick;

namespace sainim.WPF.Stores
{
    public class OriginalImageStore
    {
        public string ImagePath { get; set; } = string.Empty;
        public MagickImageCollection ImageData { get; set; } = new MagickImageCollection();
        public DateTime LastModified { get; set; } = DateTime.MinValue;

        public event Action NewImageLoaded;
        public void OnNewImageLoaded() => NewImageLoaded?.Invoke();


        public event Action CurrentImageUpdated;
        public void OnCurrentImageUpdated() => CurrentImageUpdated?.Invoke();
    }
}