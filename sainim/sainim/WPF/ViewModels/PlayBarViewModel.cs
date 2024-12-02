using sainim.Models;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Commands.PlayBarCommands;
using sainim.WPF.Stores;
using sainim.WPF.ViewModels.Elements;
using System.Windows.Input;

namespace sainim.WPF.ViewModels
{
    public class PlayBarViewModel : ViewModelBase
    {
        private readonly OriginalImageStore _originalImageStore;
        public AnimationStore AnimationStore { get; }

        public ICommand PlayAnimation { get; } = new PlayAnimationCommand();

        public PlayBarViewModel(OriginalImageStore originalImageStore, AnimationStore animationStore)
        {
            _originalImageStore = originalImageStore;
            AnimationStore = animationStore;

            _originalImageStore.NewImageLoaded += UpdateLayerTypeCollection;
        }

        private void UpdateLayerTypeCollection()
        {
            AnimationStore.SelectableLayerTypes.Clear();
            List<string> layerNames = ExtractLayerNames();

            foreach (var layerName in layerNames)
                AnimationStore.SelectableLayerTypes.Add(new SelectableOption(layerName, true));
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