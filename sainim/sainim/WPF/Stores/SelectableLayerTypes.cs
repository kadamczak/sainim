using sainim.Models;
using sainim.WPF.ViewModels.Elements;
using System.Collections.ObjectModel;

namespace sainim.WPF.Stores
{
    public class SelectableLayerTypes
    {
        public ObservableCollection<SelectableOption> LayerTypes { get; } = [];
        public List<string> GetSelectedLayerTypes() => LayerTypes.Where(layerType => layerType.IsSelected)
                                                                 .Select(layerType => layerType.Name)
                                                                 .ToList();

        public void UpdateLayerTypeCollection(List<StaticLayer> backgroundElements, List<StaticLayer> foregroundElements, List<Frame> frames)
        {
            LayerTypes.Clear();
            List<string> layerNames = ExtractLayerNames(backgroundElements, foregroundElements, frames);

            foreach (var layerName in layerNames)
                LayerTypes.Add(new SelectableOption(layerName, true));
        }

        private List<string> ExtractLayerNames(List<StaticLayer> backgroundElements, List<StaticLayer> foregroundElements, List<Frame> frames)
        {
            List<string> extractedLayerTypes = [];

            if (backgroundElements.Count > 0)
                extractedLayerTypes.Add("Background");

            if (foregroundElements.Count > 0)
                extractedLayerTypes.Add("Foreground");

            extractedLayerTypes.AddRange(GetDistinctSublayerSpecialLabels(frames));
            return extractedLayerTypes;
        }

        private List<string> GetDistinctSublayerSpecialLabels(List<Frame> frames) => frames.SelectMany(f => f.Sublayers)
                                                                                           .Select(sl => sl.SpecialLabel)
                                                                                           .Distinct()
                                                                                           .ToList();
    }
}