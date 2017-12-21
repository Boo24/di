using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class ShortWordsFilter : IWordsFilter
    {
        private const int MinWordLength = 4;
        public FilterType Type { get; } = FilterType.FilterShortWords;
        public Result<IEnumerable<Word>> Filter(IEnumerable<Word> words)
            => Result.Of(() =>words.Where(w => w?.Text != null && w.Text.Length >= MinWordLength));
    }
}
