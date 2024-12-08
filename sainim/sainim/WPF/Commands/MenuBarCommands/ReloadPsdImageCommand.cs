using ImageMagick;
using sainim.Models;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Helpers;
using sainim.WPF.Stores;
using System.IO;

namespace sainim.WPF.Commands.MenuBarCommands
{
    public class ReloadPsdImageCommand : CommandBase
    {
        private readonly OriginalImageFactory _originalImageFactory;
        private readonly OriginalImageStore _originalImageStore;
        private readonly MessageBoxHelpers _messageBoxHelpers;

        public ReloadPsdImageCommand(OriginalImageFactory originalImageFactory, OriginalImageStore originalImageStore, MessageBoxHelpers messageBoxHelpers)
        {
            _originalImageFactory = originalImageFactory;
            _originalImageStore = originalImageStore;
            _messageBoxHelpers = messageBoxHelpers;
            _originalImageStore.ImageLoaded += OnCanExecuteChanged;
        }

        public override void Execute(object? parameter)
        {
            string filePath = _originalImageStore.CurrentImage!.ImagePath;
            DateTime latestLoadedVersionTime = _originalImageStore.CurrentImage!.LastModified;

            try
            {
                if (File.GetLastWriteTime(filePath) <= latestLoadedVersionTime)
                    return;

                OriginalImage newImage = _originalImageFactory.Create(filePath);
                _originalImageStore.LoadImage(newImage);
            }
            catch (IOException e)
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

        public override bool CanExecute(object? parameter) => _originalImageStore.CurrentImage is not null && base.CanExecute(parameter); 
    }
}