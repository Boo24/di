using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class DefaultBlackList : IBlackList
    {
        private readonly string[] badWords = new[] {"the", "a", "in"};
        public IEnumerable<string> GetBadWords() => badWords;
        public bool Contains(string word) => badWords.Contains(word);
    }
}
