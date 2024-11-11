using sainim.Models;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.Collections.ObjectModel;

namespace sainim.WPF.ViewModels
{
    public class ContentBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        private OriginalImage CurrentImage => _originalImageStore.CurrentImage!;

        public ObservableCollection<LayerModel> StaticElements { get; set; } = [];
        public ObservableCollection<FrameModel> Frames { get; set; } = [];


        public ContentBarViewModel(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += OnImageLoaded;
            _originalImageStore.ImageReloaded += OnImageLoaded;
        }

        private void OnImageLoaded()
        {
            StaticElements.Clear();
            Frames.Clear();

            foreach (var element in CurrentImage.StaticElements)
                StaticElements.Add(element);

            foreach (var element in CurrentImage.Frames)
                Frames.Add(element);
        }
    }
}