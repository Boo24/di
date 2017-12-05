using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class ShortWordsFilter : IWordsFilter
    {
        private const int MinWordLength = 4;
        public string Name { get; } = "Filter short words";
        public IEnumerable<Word> Filter(IEnumerable<Word> words) => words.Where(w => w.Text.Length >= MinWordLength);
    }
}
