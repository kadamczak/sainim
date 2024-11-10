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

        private const uint MAX_THUMBNAIL_DIMENSION = 300;

        public OriginalImage(string filePath)
        {
            ImagePath = filePath;
            LastModified = File.GetLastWriteTime(filePath); //TODO catch

            var imageData = new MagickImageCollection(filePath);

            // remove combined image (it's not useful for animation)
            imageData.RemoveAt(0);

            StaticElements = imageData.Where(IsStaticLayer).Select(l => new LayerModel(l, MAX_THUMBNAIL_DIMENSION)).ToList();

            Frames = imageData.Where(IsAnimationLayer)
                              .GroupBy(GetFrameNumber)
                              .OrderBy(k => k.Key)
                              .Select(g =>
                              {
                                  var frameLayers = g.Select(l => new LayerModel(l, MAX_THUMBNAIL_DIMENSION)).ToList();
                                  return new FrameModel(g.Key, frameLayers, MAX_THUMBNAIL_DIMENSION);
                              }).ToList();

            imageData.Dispose();
        }

        private const string FRAME_SEPARATOR = "_";
        private bool IsAnimationLayer(IMagickImage layer) => layer.Label!.Contains(FRAME_SEPARATOR);
        private bool IsStaticLayer(IMagickImage layer) => !IsAnimationLayer(layer);
        private int GetFrameNumber(IMagickImage layer) => Int32.Parse(layer.Label!.Split(FRAME_SEPARATOR)[0]);
    }
}