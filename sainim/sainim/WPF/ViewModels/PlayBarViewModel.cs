using sainim.Models;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Commands.PlayBarCommands;
using sainim.WPF.Stores;
using sainim.WPF.ViewModels.Elements;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace sainim.WPF.ViewModels
{
    public class PlayBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        public ICommand PlayAnimation { get; } = new PlayAnimationCommand();
        public ObservableCollection<SelectableOption> EnabledLayerTypes { get; } = [];

        public PlayBarViewModel(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += UpdateLayerTypeCollection;
        }

        private void UpdateLayerTypeCollection()
        {
            EnabledLayerTypes.Clear();
            List<string> layerNames = ExtractLayerNames();

            foreach (var layerName in layerNames)
                EnabledLayerTypes.Add(new SelectableOption(layerName, true));
        }

        private List<string> ExtractLayerNames()
        {
            var staticElements = _originalImageStore.CurrentImage!.StaticElements;
            var frames = _originalImageStore.CurrentImage!.Frames;

            List<string> extractedLayerTypes = [];

            if (staticElements.Count != 0)
                extractedLayerTypes.Add("Static".Resource());

            extractedLayerTypes.AddRange(GetUniqueSublayerSpecialLabels(frames));
            return extractedLayerTypes;
        }

        private List<string> GetUniqueSublayerSpecialLabels(List<Frame> frames) => frames.SelectMany(f => f.Sublayers)
                                                                                         .Select(sl => sl.SpecialLabel)
                                                                                         .Distinct()
                                                                                         .ToList();
    }
}