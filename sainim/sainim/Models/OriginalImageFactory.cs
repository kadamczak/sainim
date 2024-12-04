using ImageMagick;
using sainim.Models.Enums;
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

            //get index of first frame sublayer in _imageData.
            int firstFrameSublayerIndex = FindIndexOfFirstFrameSublayer(_imageData);

            var staticElements = ExtractStaticElements(_imageData, background, firstFrameSublayerIndex);
            var frames = ExtractFrames(_imageData, background);

            return new OriginalImage(path, lastModified, background, staticElements, frames);
        }

        private int FindIndexOfFirstFrameSublayer(MagickImageCollection image)
            => image.Select((layer, index) => new { layer, index })
                    .FirstOrDefault(pair => FrameSublayer.IsAnimationSublayer(pair.layer))?.index ?? -1;

        private List<StaticLayer> ExtractStaticElements(MagickImageCollection image, MagickImage background, int foregroundThreshold)
            => image.Where(StaticLayer.IsStaticLayer)
                    .Select((layer, index) => new StaticLayer(layer, DecidePlacement(index, foregroundThreshold), background))
                    .ToList();

        private List<Frame> ExtractFrames(MagickImageCollection image, MagickImage background)
            => image.Where(FrameSublayer.IsAnimationSublayer)
                    .GroupBy(FrameSublayer.GetFrameNumber)
                    .OrderBy(sublayerGroup => sublayerGroup.Key)
                    .Select(sublayerGroup =>
                    {
                        var sublayers = sublayerGroup.Select(sublayer => new FrameSublayer(sublayer)).ToList();
                        return new Frame(sublayerGroup.Key, sublayers, background);
                    }).ToList();

        public Placement DecidePlacement(int index, int foregroundThreshold)
            => (index < foregroundThreshold) ? Placement.Background : Placement.Foreground;
    }
}