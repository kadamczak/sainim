using ImageMagick;

namespace sainim.Models.Extensions
{
    public static class MagickImageCollectionExtension
    {
        public static IMagickImage<ushort> MergeWithTransparentBackground(this MagickImageCollection collection)
        {
            if (collection.Count > 0)
                collection[0].BackgroundColor = MagickColors.Transparent;

            return collection.Merge();
        }
    }
}