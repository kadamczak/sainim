using System.Windows.Media.Imaging;

namespace sainim.WPF.Stores
{
    public class RenderedImagesStore
    {
        public Dictionary<int, List<RenderedImage>> RenderedImages { get; } = [];

        public RenderedImagesStore()
        {

        }

    }

    public class RenderedImage
    {
        public List<string> ActiveSpecialLayers { get; } = [];
        public BitmapSource Image { get; }
    }
}