using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.IO;

namespace sainim.WPF.Commands.MenuBarCommands
{
    public class ReloadPsdImageCommand : CommandBase
    {
        private readonly OriginalImageStore _originalImageStore;

        public ReloadPsdImageCommand(OriginalImageStore originalImageStore)
        {
            _originalImageStore = originalImageStore;
            _originalImageStore.NewImageLoaded += OnCanExecuteChanged;
        }

        public override void Execute(object? parameter)
        {
            string filePath = _originalImageStore.CurrentImage!.ImagePath;
            DateTime latestLoadedVersionTime = _originalImageStore.CurrentImage!.LastModified;
            DateTime currentVersionTime = File.GetLastWriteTime(filePath); //TODO catch

            // image hasn't been updated on drive since last load
            if (currentVersionTime <= latestLoadedVersionTime)
                return;

            //OriginalImage newImage = new OriginalImage(filePath);
            //_originalImageStore.ReloadImage(newImage);
        }

        public override bool CanExecute(object? parameter) => _originalImageStore.CurrentImage is not null && base.CanExecute(parameter); 
    }
}