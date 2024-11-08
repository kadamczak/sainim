using System.ComponentModel;

namespace sainim.WPF.Bases
{
    /// <summary>
    /// The <c>ViewModelBase</c> abstract class contains basic logic for view models in the MVVM pattern.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        protected virtual void Dispose() { }
    }
}