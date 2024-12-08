using ImageMagick;
using Microsoft.Win32;
using sainim.Models.Extensions;
using sainim.WPF.Bases;
using sainim.WPF.Helpers;
using sainim.WPF.Stores;
using System.IO;
using System.Windows.Media.Imaging;

namespace sainim.WPF.Commands.MenuBarCommands
{
    public class ExportGifCommand : CommandBase
    {
        private readonly OriginalImageStore _originalImageStore;
        private readonly AnimationStore _animationStore;
        private readonly MessageBoxHelpers _messageBoxHelpers;

        public ExportGifCommand(OriginalImageStore originalImageStore, AnimationStore animationStore, MessageBoxHelpers messageBoxHelpers)
        {
            _originalImageStore = originalImageStore;
            _animationStore = animationStore;
            _messageBoxHelpers = messageBoxHelpers;

            _originalImageStore.NewImageLoaded += OnCanExecuteChanged;
        }

        public override void Execute(object? parameter)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "GIF Files (*.gif)|*.gif",
                Title = "ExportGif".Resource()
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    ExportGif(saveFileDialog.FileName);
                }
                catch (IOException e)
                {
                    _messageBoxHelpers.ShowGenericErrorMessageBox("IOError".Resource(), e.Message);
                }
                catch (Exception e)
                {
                    _messageBoxHelpers.ShowGenericErrorMessageBox("UnknownErrorOperationUnsuccessful".Resource(), e.Message);
                }
            }
        }

        public override bool CanExecute(object? parameter) => _originalImageStore.CurrentImage is not null && base.CanExecute(parameter);

        private void ExportGif(string fileName)
        {
            (int firstFrameIndex, int lastFrameIndex) = _animationStore.FindFirstAndLastFullFrameIndices();
            var enabledLayerTypes = _animationStore.SelectableLayerTypes.GetSelectedLayerTypes();

            if (firstFrameIndex < 0 || (firstFrameIndex == lastFrameIndex))
                return;

            // Image rendering
            _animationStore.RenderMissingFrames(firstFrameIndex, lastFrameIndex, enabledLayerTypes);
            
            // Create gif
            var gifImageCollection = CreateGifImageCollection(firstFrameIndex, lastFrameIndex, enabledLayerTypes);
            SetGifFrameRate(gifImageCollection);
            SetGifRepeat(gifImageCollection);

            // Write
            gifImageCollection.Write(fileName);
        }

        private MagickImageCollection CreateGifImageCollection(int firstFrameIndex, int lastFrameIndex, List<string> enabledLayerTypes)
        {
            var gifImageCollection = new MagickImageCollection();

            for (int i = firstFrameIndex; i <= lastFrameIndex; i++)
            {
                var frame = _animationStore.AnimationSequence[i];
                if (frame is null)
                    continue;

                BitmapSource bitmapLayer = frame.GetRenderedBitmap(enabledLayerTypes)!;
                MagickImage magickLayer = BitmapSourceToMagickImage(bitmapLayer);
                gifImageCollection.Add(magickLayer);
            }

            return gifImageCollection;
        }
        private static MagickImage BitmapSourceToMagickImage(BitmapSource bitmapSource)
        {
            byte[] byteArray = BitmapSourceToByteArray(bitmapSource);
            return new MagickImage(byteArray);
        }

        private static byte[] BitmapSourceToByteArray(BitmapSource bitmapSource)
        {
            using var stream = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
            encoder.Save(stream);
            return stream.ToArray();
        }

        private void SetGifFrameRate(MagickImageCollection gifImageCollection)
        {
            uint delay = (uint)_animationStore.GetMillisecondsBetweenFrames() / 10;
            foreach (var image in gifImageCollection)
                image.AnimationDelay = delay;
        }

        private void SetGifRepeat(MagickImageCollection gifImageCollection)
            => gifImageCollection[0].AnimationIterations = (uint)(_animationStore.Repeating ? 0 : 1);
    }
}