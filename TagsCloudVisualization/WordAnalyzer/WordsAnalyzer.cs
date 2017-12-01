using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class WordsAnalyzer
    {
        private IBlackList blackList;
        private IEnumerable<IWordsFilter> filters;
        public WordsAnalyzer(IBlackList badWords, IEnumerable<IWordsFilter> filters)
        {
            blackList = badWords;
            this.filters = filters;
        }

        private IEnumerable<Word> FilterBadWords(IEnumerable<Word> allWords) => allWords.Where(x => !blackList.Contains(x.Text));

        private IEnumerable<Word> ApplyFilters(IEnumerable<Word> allwords) =>
            allwords.Where(w => filters.All(f => f.CheckWord(w)));

        private  IEnumerable<Word> OrderInDescending(IEnumerable<Word> allWords) => allWords.OrderByDescending(x => x.CountOfOccurrences);

        public (IEnumerable<Word> sortedWords, int minCount, int maxCount) Analyze(IEnumerable<Word> words, int wordsCount)
        { 
            var result = OrderInDescending(ApplyFilters(FilterBadWords(words))).Take(wordsCount);
            var borders = GetMinAndMaxCountOfOccurrences(result);
            return (result, borders.minCount, borders.maxCount);
        }

        private (int minCount, int maxCount) GetMinAndMaxCountOfOccurrences(IEnumerable<Word> words)
        {
            if (words.FirstOrDefault() is null) return (0, 0);
            return (words.Last().CountOfOccurrences, words.First().CountOfOccurrences);
        }

    }
}
