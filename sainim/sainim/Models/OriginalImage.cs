using ImageMagick;
using sainim.Models.Extensions;
using System.IO;

namespace sainim.Models
{
    //16-bit psds for now
    public class OriginalImage
    {
        public string ImagePath { get; }
        public DateTime LastModified { get; }

        public List<LayerModel> StaticElements { get; }
        public List<FrameModel> Frames { get; }

        public OriginalImage(string filePath)
        {
            ImagePath = filePath;
            LastModified = File.GetLastWriteTime(filePath); //TODO catch

            var imageData = new MagickImageCollection(filePath);

            // create transparent image with the width and height of the whole original image
            // to center the thumbnails of the rest of the layers which might be offseted
            var background = new MagickImage(MagickColors.White, imageData[0].Width, imageData[0].Height);
            imageData.RemoveAt(0);

            StaticElements = imageData.Where(IsStaticLayer).Select(l => new LayerModel(l, background)).ToList();

            Frames = imageData.Where(IsAnimationLayer)
                              .GroupBy(GetFrameNumber)
                              .OrderBy(k => k.Key)
                              .Select(g =>
                              {
                                  var frameLayers = g.Select(l => new LayerModel(l, background)).ToList();
                                  return new FrameModel(g.Key, frameLayers, background);
                              }).ToList();

            imageData.Dispose();
        }

        private const string FRAME_SEPARATOR = "_";
        private bool IsAnimationLayer(IMagickImage layer) => layer.Label!.Contains(FRAME_SEPARATOR);
        private bool IsStaticLayer(IMagickImage layer) => !IsAnimationLayer(layer);
        private int GetFrameNumber(IMagickImage layer) => Int32.Parse(layer.Label!.Split(FRAME_SEPARATOR)[0]);
    }
}