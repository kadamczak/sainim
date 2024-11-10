using ImageMagick;
using Microsoft.Win32;
using sainim.WPF.Bases;
using sainim.WPF.Stores;
using System.IO;

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
                Title = "Import PSD Image"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                _originalImageStore.ImagePath = filePath;
                _originalImageStore.ImageData = new MagickImageCollection(filePath);
                _originalImageStore.LastModified = File.GetLastWriteTime(filePath);

                // remove combined image (it's not useful for animation)
                _originalImageStore.ImageData.RemoveAt(0);

                _originalImageStore.OnNewImageLoaded();
            }
        }
    }
}