namespace GGM.GFMarcro.Core.Recognition
{
    public struct SearchResult
    {
        internal SearchResult(double minimumSimilarity, double maximumSimilarity, System.Drawing.Point minimumPoint, System.Drawing.Point maximumPoint)
        {
            MinimumSimilarity = minimumSimilarity;
            MaximumSimilarity = maximumSimilarity;
            MinimumPoint = minimumPoint;
            MaximumPoint = maximumPoint;
        }

        public double MinimumSimilarity { get; }
        public double MaximumSimilarity { get; }
        public System.Drawing.Point MinimumPoint { get; }
        public System.Drawing.Point MaximumPoint { get; }
    }
}