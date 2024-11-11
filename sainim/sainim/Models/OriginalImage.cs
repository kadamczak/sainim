using ImageMagick;
using System.IO;

namespace sainim.Models
{
    //16-bit psds for now
    public class OriginalImage
    {
        public string ImagePath { get; }
        public DateTime LastModified { get; }

        public List<StaticLayer> StaticElements { get; }
        public List<Frame> Frames { get; }

        public OriginalImage(string filePath)
        {
            ImagePath = filePath;
            LastModified = File.GetLastWriteTime(filePath); //TODO catch

            var imageData = new MagickImageCollection(filePath);

            // create transparent image with the width and height of the whole original image
            // to center the thumbnails of the rest of the layers which might be offseted
            var background = new MagickImage(MagickColors.White, imageData[0].Width, imageData[0].Height);
            imageData.RemoveAt(0);

            StaticElements = imageData.Where(StaticLayer.IsStaticLayer).Select(l => new StaticLayer(l, background)).ToList();

            Frames = imageData.Where(FrameSublayer.IsAnimationSublayer)
                              .GroupBy(FrameSublayer.GetFrameNumber)
                              .OrderBy(k => k.Key)
                              .Select(g =>
                              {
                                  var frameSublayers = g.Select(l => new FrameSublayer(l)).ToList();
                                  return new Frame(g.Key, frameSublayers, background);
                              }).ToList();

            imageData.Dispose();
        }
    }
}