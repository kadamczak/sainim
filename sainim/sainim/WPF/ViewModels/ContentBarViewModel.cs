using ImageMagick;
using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.ViewModels
{
    public class ContentBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        private const string FRAME_SEPARATOR = "_";

        public ContentBarViewModel(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += OnNewImageLoaded;
        }

        private void OnNewImageLoaded()
        {
            var staticLayers = ImageData.Where(IsStaticLayer).ToList();

            var frames = ImageData.Where(IsAnimationLayer)
                                  .GroupBy(GetFrameNumber)
                                  .OrderBy(k => k.Key)
                                  .ToList();


            //OnPropertyChanged(nameof(ImagePath));
            //OnPropertyChanged(nameof(ImageData));
            //OnPropertyChanged(nameof(LastModified));
        }

        public string ImagePath => _originalImageStore.ImagePath;
        public MagickImageCollection ImageData => _originalImageStore.ImageData;
        public DateTime LastModified => _originalImageStore.LastModified;

        private bool IsAnimationLayer(IMagickImage layer) => layer.Label!.Contains(FRAME_SEPARATOR);
        private bool IsStaticLayer(IMagickImage layer) => !IsAnimationLayer(layer);
        private int GetFrameNumber(IMagickImage layer) => Int32.Parse(layer.Label!.Split(FRAME_SEPARATOR)[0]);
    }
}