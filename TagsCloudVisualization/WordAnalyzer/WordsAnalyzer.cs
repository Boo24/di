using System;
using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class WordsAnalyzer
    {
        private Dictionary<FilterType, IWordsFilter> allFilters = new Dictionary<FilterType, IWordsFilter>();
        private Dictionary<WordsConverterType, IWordConverter> allConverters = new Dictionary<WordsConverterType, IWordConverter>();

        public WordsAnalyzer(IEnumerable<IWordsFilter> filters, IEnumerable<IWordConverter> converters)
        {
            foreach (var filter in filters)
                allFilters[filter.Type] = filter;
            foreach (var converter in  converters)
                allConverters[converter.Type] = converter;
        }

        public AnalyzeResult Analyze(IEnumerable<string> words, int wordsCount, IEnumerable<FilterType> useFilters, IEnumerable<WordsConverterType> useConverters)
        {
            var groupWords = GroupWords(ApplyConverters(words, useConverters));
            var result = OrderInDescending(ApplyFilters(groupWords, useFilters)).Take(wordsCount);
            var borders = GetMinAndMaxCountOfOccurrences(result);
            return new AnalyzeResult(borders.maxCount, borders.minCount, result);
        }

        private List<Word> ApplyFilters(IEnumerable<Word> allwords, IEnumerable<FilterType> useFilters) =>
            useFilters.Aggregate(allwords, (current, useFilter) => allFilters[useFilter].Filter(current)).ToList();

        private IEnumerable<string> ApplyConverters(IEnumerable<string> words, IEnumerable<WordsConverterType> useConverters) =>
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