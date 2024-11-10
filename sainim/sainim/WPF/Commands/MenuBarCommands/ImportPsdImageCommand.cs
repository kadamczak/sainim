using Microsoft.Win32;
using sainim.Models;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.MenuBarCommands
{
    public class ImportPsdImageCommand(OriginalImageStore originalImageStore) : CommandBase
    {
        private readonly OriginalImageStore _originalImageStore = originalImageStore;

        public override void Execute(object? parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PSD Files (*.psd)|*.psd",
                Title = "ImportPSDImage".Resource()
            };

            if (openFileDialog.ShowDialog() == true)
            {
                OriginalImage newImage = new OriginalImage(openFileDialog.FileName);
                _originalImageStore.LoadNewImage(newImage);
            }
        }
    }
}