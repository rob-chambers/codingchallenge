using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;

            // TODO: data preparation and initialisation of additional data structures to improve performance goes here.

        }


        public SearchResults Search(SearchOptions options)
        {
            // TODO: search logic goes here.
            var shirts = new List<Shirt>();

            var sizes = _shirts.Where(x => options.Sizes.Contains(x.Size));
            foreach (var item in sizes)
            {
                if (options.Colors.Contains(item.Color))
                {
                    shirts.Add(item);
                    break;
                }
            }

            var colors = shirts.Select(x => x.Color).Distinct().Count();

            var sizeGroups = shirts.GroupBy(x => x.Size)
                .Select(x => new
                {
                    Size = x.Key,
                    Count = x.Count()
                });

            var sizeCounts = new List<SizeCount>();
            foreach (var item in sizeGroups)
            {
                sizeCounts.Add(new SizeCount
                {
                    Size = item.Size,
                    Count = item.Count
                });
            }

            AddMissingSizeCounts(sizeCounts);

            var colorGroups = shirts.GroupBy(x => x.Color)
                .Select(x => new
                {
                    Color = x.Key,
                    Count = x.Count()
                });

            var colorCounts = new List<ColorCount>();
            foreach (var item in colorGroups)
            {
                colorCounts.Add(new ColorCount
                {
                    Color = item.Color,
                    Count = item.Count
                });
            }

            AddMissingColorCounts(colorCounts);

            return new SearchResults(shirts, sizeCounts, colorCounts);
        }

        private static void AddMissingSizeCounts(List<SizeCount> sizeCounts)
        {
            var distinctSizes = sizeCounts.Select(x => x.Size).Distinct();
            var remaining = Size.All.Except(distinctSizes);

            foreach (var item in remaining)
            {
                sizeCounts.Add(new SizeCount
                {
                    Size = item,
                    Count = 0
                });
            }
        }

        private static void AddMissingColorCounts(List<ColorCount> colorCounts)
        {
            var distinctColors = colorCounts.Select(x => x.Color).Distinct();
            var remaining = Color.All.Except(distinctColors);

            foreach (var item in remaining)
            {
                colorCounts.Add(new ColorCount
                {
                    Color = item,
                    Count = 0
                });
            }
        }
    }
}