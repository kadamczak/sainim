using ImageMagick;
using Microsoft.Win32;
using sainim.Models;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Helpers;
using sainim.WPF.Stores;
using System.IO;

namespace sainim.WPF.Commands.MenuBarCommands
{
    public class ImportPsdImageCommand(OriginalImageFactory originalImageFactory,
                                       OriginalImageStore originalImageStore,
                                       MessageBoxHelpers messageBoxHelpers) : CommandBase
    {
        private readonly OriginalImageFactory _originalImageFactory = originalImageFactory;
        private readonly OriginalImageStore _originalImageStore = originalImageStore;
        private readonly MessageBoxHelpers _messageBoxHelpers = messageBoxHelpers;

        public override void Execute(object? parameter)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PSD Files (*.psd)|*.psd",
                Title = "ImportPSDImage".Resource()
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    OriginalImage newImage = _originalImageFactory.Create(openFileDialog.FileName);
                    _originalImageStore.LoadNewImage(newImage);
                }
                catch(IOException e)
                {
                    _messageBoxHelpers.ShowGenericErrorMessageBox("IOError".Resource(), e.Message);
                }
                catch (MagickException e)
                {
                    _messageBoxHelpers.ShowGenericErrorMessageBox("ImageParsingError".Resource(), e.Message);
                }
                catch (Exception e)
                {
                    _messageBoxHelpers.ShowGenericErrorMessageBox("UnknownErrorOperationUnsuccessful".Resource(), e.Message);
                }
            }
        }
    }
}