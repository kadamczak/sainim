using sainim.Models;

namespace sainim.WPF.Stores
{
    public class OriginalImageStore
    {
        public OriginalImage? CurrentImage { get; private set; } = null;

        public event Action ImageLoaded;
        public void OnImageLoaded() => ImageLoaded?.Invoke();

        public event Action ImageReloaded;
        public void OnImageReloaded() => ImageReloaded?.Invoke();

        public void LoadImage(OriginalImage newImage)
        {
            CurrentImage = newImage;
            OnImageLoaded();
        }
    }
}