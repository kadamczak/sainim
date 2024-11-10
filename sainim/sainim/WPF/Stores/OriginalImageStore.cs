using sainim.Models;

namespace sainim.WPF.Stores
{
    public class OriginalImageStore
    {
        public OriginalImage? CurrentImage { get; private set; } = null;

        public void LoadNewImage(OriginalImage newImage)
        {
            CurrentImage = newImage;
            OnNewImageLoaded();
        }

        public void ReloadImage(OriginalImage newImage)
        {
            CurrentImage = newImage;
            OnImageReloaded();
        }

        public event Action NewImageLoaded;
        public void OnNewImageLoaded() => NewImageLoaded?.Invoke();


        public event Action ImageReloaded;
        public void OnImageReloaded() => ImageReloaded?.Invoke();
    }
}