namespace sainim.Models.Extensions
{
    public static class IEnumerableExtension
    {
        /// <summary>
        /// Enables foreach with iteration.
        /// </summary>
        /// <typeparam name="T">Type of objects inside IEnumerable <paramref name="self"/>.</typeparam>
        /// <param name="self">Collection.</param>
        /// <returns>Collections items with indexes.</returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self, int indexOffset = 0)
         => self.Select((item, index) => (item, index + indexOffset));
    }
}