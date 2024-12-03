using ImageMagick;
using System.IO;

namespace sainim.Models
{
    public class OriginalImageFactory
    {
        public OriginalImage Create(string path)
        {
            DateTime lastModified = File.GetLastWriteTime(path);

            var _imageData = new MagickImageCollection(path);

            // create transparent image with the width and height of the whole original image
            // to center the thumbnails of the rest of the layers which might be offseted
            var background = new MagickImage(MagickColors.White, _imageData[0].Width, _imageData[0].Height);
            _imageData.RemoveAt(0);

            var staticElements = _imageData.Where(StaticLayer.IsStaticLayer).Select(l => new StaticLayer(l, background)).ToList();

            var frames = _imageData.Where(FrameSublayer.IsAnimationSublayer)
                              .GroupBy(FrameSublayer.GetFrameNumber)
                              .OrderBy(k => k.Key)
                              .Select(g =>
                              {
                                  var frameSublayers = g.Select(l => new FrameSublayer(l)).ToList();
                                  return new Frame(g.Key, frameSublayers, background);
                              }).ToList();

            return new OriginalImage(path, lastModified, background, staticElements, frames);
        }
    }
}