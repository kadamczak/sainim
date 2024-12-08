using sainim.Models;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;

namespace sainim.WPF.ViewModels
{
    public class ContentBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        private OriginalImage CurrentImage => _originalImageStore.CurrentImage!;

        public ObservableCollection<StaticLayer> StaticElements { get; set; } = [];
        public ObservableCollection<Frame> Frames { get; set; } = [];

        public ContentBarViewModel(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.ImageLoaded += OnImageLoaded;
            _originalImageStore.ImageReloaded += OnImageLoaded;
        }

        private void OnImageLoaded()
        {
            StaticElements.Clear();
            Frames.Clear();

            StaticElements.AddRange(CurrentImage.StaticElements);
            Frames.AddRange(CurrentImage.Frames);
        }
    }
}