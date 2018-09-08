namespace GGM.GFMarcro.Core.Recognition
{
    public struct SearchResult
    {
        internal SearchResult(double maximumSimilarity, System.Drawing.Point maximumPoint)
        {
            MaximumSimilarity = maximumSimilarity;
            MatchedPoint = maximumPoint;
        }

        public double MaximumSimilarity { get; }
        public System.Drawing.Point MatchedPoint { get; }
    }
}