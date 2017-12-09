using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class ShortWordsFilter : IWordsFilter
    {
        private const int MinWordLength = 4;
        public FilterType Type { get; } = FilterType.FilterShortWords;
        public IEnumerable<Word> Filter(IEnumerable<Word> words) => words.Where(w => w?.Text != null && w.Text.Length >= MinWordLength);
    }
}
