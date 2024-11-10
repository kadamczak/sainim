using ImageMagick;
using System.IO;

namespace sainim.Models
{
    //16-bit psds for now
    public class OriginalImage
    {
        public string ImagePath { get; }
        public DateTime LastModified { get; }

        public List<IMagickImage<ushort>> StaticElements { get; }
        public List<IGrouping<int, IMagickImage<ushort>>> Frames { get; }

        public OriginalImage(string filePath)
        {
            ImagePath = filePath;
            LastModified = File.GetLastWriteTime(filePath); //TODO catch

            var imageData = new MagickImageCollection(filePath);
            // remove combined image (it's not useful for animation)
            imageData.RemoveAt(0);

            StaticElements = imageData.Where(IsStaticLayer).ToList();
            Frames = imageData.Where(IsAnimationLayer)
                              .GroupBy(GetFrameNumber)
                              .OrderBy(k => k.Key)
                              .ToList();
        }


        private const string FRAME_SEPARATOR = "_";
        private bool IsAnimationLayer(IMagickImage layer) => layer.Label!.Contains(FRAME_SEPARATOR);
        private bool IsStaticLayer(IMagickImage layer) => !IsAnimationLayer(layer);
        private int GetFrameNumber(IMagickImage layer) => Int32.Parse(layer.Label!.Split(FRAME_SEPARATOR)[0]);
    }
}