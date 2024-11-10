using ImageMagick;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class ContentBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;

        public ObservableCollection<BitmapSource> StaticElementThumbnails { get; set; } = [];
        public ObservableCollection<BitmapSource> FrameThumbnails { get; set; } = [];


        //public class ContentBarElement(int frameNumber, BitmapSource thumbnail)
        //{
        //    public int FrameNumber { get; } = frameNumber;
        //    public BitmapSource Thumbnail { get; } = thumbnail;
        //}

        public ContentBarViewModel(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += OnImageLoaded;
            _originalImageStore.ImageReloaded += OnImageLoaded;
        }

        private void OnImageLoaded()
        {
            //copy
            var staticElementThumbnails = _originalImageStore.CurrentImage?.StaticElements.Select(e => new MagickImage(e)).Select(s => s.ToBitmapSource());
            foreach(var element in staticElementThumbnails)
            {
                StaticElementThumbnails.Add(element);
            }

            var frames = _originalImageStore.CurrentImage?.Frames;
            var frameThumbnails = frames.Select(f => new MagickImageCollection(f).Merge().ToBitmapSource());
            foreach (var element in frameThumbnails)
            {
                FrameThumbnails.Add(element);
            }

        }
    }
}