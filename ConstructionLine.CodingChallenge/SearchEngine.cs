using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
        }

        public SearchResults Search(SearchOptions options)
        {
            var selectedSizes = options.Sizes;
            if (!selectedSizes.Any())
            {
                selectedSizes = Size.All;
            }

            var selectedColors = options.Colors;
            if (!selectedColors.Any())
            {
                selectedColors = Color.All;
            }

            var shirts = _shirts.Where(x => selectedSizes.Contains(x.Size) &&
                    selectedColors.Contains(x.Color))
                    .ToList();

            return new SearchResults(shirts, CalculateSizeCounts(shirts), CalculateColorCounts(selectedSizes, shirts));
        }

        private List<ColorCount> CalculateColorCounts(List<Size> selectedSizes, List<Shirt> shirts)
        {
            var colorGroups = shirts
                .Where(x => selectedSizes.Contains(x.Size))
                .GroupBy(x => x.Color)
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

            AddMissingColorCounts(colorCounts, selectedSizes);
            return colorCounts;
        }

        private List<SizeCount> CalculateSizeCounts(List<Shirt> shirts)
        {
            var sizeGroups = shirts
                .GroupBy(x => x.Size)
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
            return sizeCounts;
        }

        private void AddMissingSizeCounts(List<SizeCount> sizeCounts)
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

        private void AddMissingColorCounts(List<ColorCount> colorCounts, List<Size> selectedSizes)
        {
            var distinctColors = colorCounts.Select(x => x.Color).Distinct();
            var remaining = Color.All.Except(distinctColors);

            foreach (var item in remaining)
            {
                colorCounts.Add(new ColorCount
                {
                    Color = item,
                    Count = _shirts.Count(x => x.Color == item && selectedSizes.Contains(x.Size))
                });
            }
        }
    }
}