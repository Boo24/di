using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class WordsAnalyzer
    {
        private IBlackList blackList;
        private IEnumerable<IWordsFilter> filters;
        private IEnumerable<IWordConverter> converters;

        public WordsAnalyzer(IBlackList badWords, IEnumerable<IWordsFilter> filters,
            IEnumerable<IWordConverter> converters)
        {
            blackList = badWords;
            this.filters = filters;
            this.converters = converters;
        }

        public (IEnumerable<Word> sortedWords, int minCount, int maxCount) Analyze(IEnumerable<string> words,
            int wordsCount)
        {
            var groupWords = GroupWords(ApplyConverters(words));
            var result = OrderInDescending(ApplyFilters(FilterBadWords(groupWords))).Take(wordsCount);
            var borders = GetMinAndMaxCountOfOccurrences(result);
            return (result, borders.minCount, borders.maxCount);
        }

        private List<Word> FilterBadWords(IEnumerable<Word> allWords) =>
            allWords.Where(x => !blackList.Contains(x.Text)).ToList();

        private List<Word> ApplyFilters(IEnumerable<Word> allwords) =>
            allwords.Where(w => filters.All(f => f.CheckWord(w))).ToList();

        private IEnumerable<string> ApplyConverters(IEnumerable<string> words) =>
            converters.Aggregate(words, (current, converter) => converter.Convert(current));

        private IEnumerable<Word> OrderInDescending(IEnumerable<Word> allWords) =>
            allWords.OrderByDescending(x => x.CountOfOccurrences);

        private (int minCount, int maxCount) GetMinAndMaxCountOfOccurrences(IEnumerable<Word> words)
        {
            if (words.FirstOrDefault() is null) return (0, 0);
            return (words.Last().CountOfOccurrences, words.First().CountOfOccurrences);
        }

        private IEnumerable<Word> GroupWords(IEnumerable<string> words)
            => words.GroupBy(word => word, (word, eqWord) => new Word(word, eqWord.Count()),
                StringComparer.InvariantCultureIgnoreCase);
    }
}