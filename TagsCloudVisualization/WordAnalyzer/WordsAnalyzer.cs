using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class WordsAnalyzer
    {
        private Dictionary<string, IWordsFilter> allFilters = new Dictionary<string, IWordsFilter>();
        private Dictionary<string, IWordConverter> allConverters = new Dictionary<string, IWordConverter>();

        public WordsAnalyzer(IEnumerable<IWordsFilter> filters, IEnumerable<IWordConverter> converters)
        {
            foreach (var filter in filters)
                allFilters[filter.Name] = filter;
            foreach (var converter in  converters)
                allConverters[converter.Name] = converter;
        }

        public AnalyzeResult Analyze(IEnumerable<string> words, int wordsCount, HashSet<string> useFilters, HashSet<string> useConverters)
        {
            var groupWords = GroupWords(ApplyConverters(words, useConverters));
            var result = OrderInDescending(ApplyFilters(groupWords, useFilters)).Take(wordsCount);
            var borders = GetMinAndMaxCountOfOccurrences(result);
            return new AnalyzeResult(borders.maxCount, borders.minCount, result);
        }

        private List<Word> ApplyFilters(IEnumerable<Word> allwords, HashSet<string> useFilters)
        {
            return useFilters.Aggregate(allwords, (current, useFilter) => allFilters[useFilter].Filter(current)).ToList();
        }

        private IEnumerable<string> ApplyConverters(IEnumerable<string> words, HashSet<string> useConverters) =>
            useConverters.Aggregate(words, (current, useConverter) => allConverters[useConverter].Convert(current));

        private IEnumerable<Word> OrderInDescending(IEnumerable<Word> allWords) =>
            allWords.OrderByDescending(x => x.CountOfOccurrences);

        private (int minCount, int maxCount) GetMinAndMaxCountOfOccurrences(IEnumerable<Word> words)
        {
            if (words.FirstOrDefault() is null) return (0, 0);
            return (words.Last().CountOfOccurrences, words.First().CountOfOccurrences);
        }
        private IEnumerable<Word> GroupWords(IEnumerable<string> words)
            => words.GroupBy(word => word, (word, eqWord) => new Word(word, eqWord.Count()), StringComparer.InvariantCultureIgnoreCase);
    }
}