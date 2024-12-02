using System.ComponentModel;

namespace sainim.WPF.ViewModels.Elements
{
    public class SelectableOption(string name, bool isSelected) : INotifyPropertyChanged
    {
        public string Name { get; } = name;

        private bool _isSelected = isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}