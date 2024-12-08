using sainim.Models;
using sainim.WPF.Bases;
using sainim.WPF.Helpers;
using sainim.WPF.Stores;

namespace sainim.WPF.Commands.MenuBarCommands
{
    public class ExportGifCommand : CommandBase
    {
        private readonly OriginalImageFactory _originalImageFactory;
        private readonly OriginalImageStore _originalImageStore;
        private readonly MessageBoxHelpers _messageBoxHelpers;

        public ExportGifCommand(OriginalImageFactory originalImageFactory, OriginalImageStore originalImageStore, MessageBoxHelpers messageBoxHelpers)
        {
            _originalImageFactory = originalImageFactory;
            _originalImageStore = originalImageStore;
            _messageBoxHelpers = messageBoxHelpers;

            _originalImageStore.NewImageLoaded += OnCanExecuteChanged;
        }

        public override void Execute(object? parameter)
        {
            //
        }

        public override bool CanExecute(object? parameter) => _originalImageStore.CurrentImage is not null && base.CanExecute(parameter);
    }
}