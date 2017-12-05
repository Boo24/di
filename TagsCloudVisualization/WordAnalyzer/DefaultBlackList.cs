using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class DefaultBlackList : IWordsFilter
    {
        private string[] badWords;
        public string Name { get; } = "Boring words filter";
        public DefaultBlackList() =>badWords = File.ReadAllLines("BoringWords.txt");
        public IEnumerable<Word> Filter(IEnumerable<Word> words) => words.Where(w => !badWords.Contains(w.Text));
    }
}
