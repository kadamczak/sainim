using sainim.Models;
using sainim.WPF.ViewModels.Elements;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace sainim.WPF.Stores
{
    public class SelectableLayerTypes : INotifyPropertyChanged
    {
        public ObservableCollection<SelectableOption> LayerTypes { get; } = [];


        public List<string> GetSelectedLayerTypes() => LayerTypes.Where(layerType => layerType.IsSelected)
                                                                 .Select(layerType => layerType.Name)
                                                                 .ToList();

        //Events
        private bool _enableEvents = true;
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (_enableEvents)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Methods
        public void UpdateLayerTypeCollection(List<StaticLayer> backgroundElements, List<StaticLayer> foregroundElements, List<Frame> frames)
        {
            _enableEvents = false;
            LayerTypes.Clear();
            List<string> layerNames = ExtractLayerNames(backgroundElements, foregroundElements, frames);

            foreach (var layerName in layerNames)
            {
                var option = new SelectableOption(layerName, true);
                option.PropertyChanged += (s, e) => OnPropertyChanged(nameof(LayerTypes));
                LayerTypes.Add(option);
            }
            _enableEvents = true;
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