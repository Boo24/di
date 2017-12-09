using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class DefaultBlackList : IWordsFilter
    {
        private string[] badWords;
        public FilterType Type { get; } = FilterType.BoringWordsFilter;
        public DefaultBlackList(string[] badWords) => this.badWords = badWords;
        public IEnumerable<Word> Filter(IEnumerable<Word> words) => words.Where(w => !badWords.Contains(w.Text));
    }
}
