using System.Collections.Generic;

namespace ConstructionLine.CodingChallenge
{
    public class SearchResults
    {
        public SearchResults(
            List<Shirt> shirts,
            List<SizeCount> sizeCounts,
            List<ColorCount> colorCounts)
        {
            Shirts = shirts;
            SizeCounts = sizeCounts;
            ColorCounts = colorCounts;
        }

        public List<Shirt> Shirts { get; private set; } = new List<Shirt>();

        public List<SizeCount> SizeCounts { get; private set; } = new List<SizeCount>();

        public List<ColorCount> ColorCounts { get; private set; } = new List<ColorCount>();
    }
}