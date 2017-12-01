using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class WordsAnalyzer
    {
        private IBlackList blackList;
        public WordsAnalyzer(IBlackList badWords) => blackList = badWords;

        private IEnumerable<Word> FilterBadWords(IEnumerable<Word> allWords) =>
            allWords.Where(x => !blackList.Contains(x.Text));
        
        private  IEnumerable<Word> OrderInDescending(IEnumerable<Word> allWords) =>
            allWords.OrderByDescending(x => x.CountOfOccurrences);

        public (IEnumerable<Word> sortedWords, int minCount, int maxCount) Analyze(IEnumerable<Word> words, int wordsCount)
        {
            var h = new WordConverter();
            h.Do();   
            var r = words.Where(w => w.Text.Length > 0 && w.CountOfOccurrences>0);
            var result = OrderInDescending(FilterBadWords(r)).Take(wordsCount);
            return (result, result.Last().CountOfOccurrences, result.First().CountOfOccurrences);
        }
    }
}
