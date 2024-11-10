using ImageMagick;
using sainim.Models;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace sainim.WPF.ViewModels
{
    public class ContentBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        private OriginalImage CurrentImage => _originalImageStore.CurrentImage!;

        public ObservableCollection<ContentBarElement> StaticElementThumbnails { get; set; } = [];
        public ObservableCollection<ContentBarElement> FrameThumbnails { get; set; } = [];


        public class ContentBarElement(string label, BitmapSource thumbnail)
        {
            public string Label { get; } = label;
            public BitmapSource Thumbnail { get; } = thumbnail;
        }

        public ContentBarViewModel(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += OnImageLoaded;
            _originalImageStore.ImageReloaded += OnImageLoaded;
        }

        private void OnImageLoaded()
        {
            //copy
            var staticElementThumbnails = CurrentImage.StaticElements.Select(e => new MagickImage(e)).Select(s => new ContentBarElement(s.Label, s.ToBitmapSource()));
            foreach(var element in staticElementThumbnails)
            {
                StaticElementThumbnails.Add(element);
            }

            var frames = CurrentImage.Frames;
            var frameThumbnails = frames.Select(f => new ContentBarElement(f.Key.ToString(), new MagickImageCollection(f).Merge().ToBitmapSource()));
            foreach (var element in frameThumbnails)
            {
                FrameThumbnails.Add(element);
            }

            

        }
    }
}