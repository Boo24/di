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

        public Result<AnalyzeResult> Analyze(IEnumerable<string> words, int wordsCount, 
            IEnumerable<FilterType> useFilters, IEnumerable<WordsConverterType> useConverters)
        {
            var groupWords = Result.Ok(words)
                            .Then(w => ApplyConverters(w, useConverters))
                            .Then(GroupWords)
                            .Then(w => ApplyFilters(w, useFilters))
                            .Then(r => OrderInDescendingTopWords(r, wordsCount).AsResult());
            return groupWords
                .Then(GetMinAndMaxCountOfOccurrences)
                .Then( r => new AnalyzeResult(r.maxCount, r.minCount, groupWords.Value));
        }

        private Result<IEnumerable<Word>> ApplyFilters(IEnumerable<Word> allwords, IEnumerable<FilterType> useFilters)
        {
            var res = Result.Ok(allwords);
            return useFilters.Aggregate(res, (current, useFilter) => current.Then(w => allFilters[useFilter].Filter(w)));
        }

        private Result<IEnumerable<string>> ApplyConverters(IEnumerable<string> words,
            IEnumerable<WordsConverterType> useConverters)
        {
            var res = Result.Ok(words);
            return  useConverters.Aggregate(res, (current, useConverter) => current.Then(w => allConverters[useConverter].Convert(w)));
        }

        private IEnumerable<Word> OrderInDescendingTopWords(IEnumerable<Word> allWords, int wordsCount) =>
            allWords.OrderByDescending(x => x.CountOfOccurrences).Take(wordsCount);

        private (int minCount, int maxCount) GetMinAndMaxCountOfOccurrences(IEnumerable<Word> words)
        {
            if (words.FirstOrDefault() is null) return (0, 0);
            return (words.Last().CountOfOccurrences, words.First().CountOfOccurrences);
        }
        private IEnumerable<Word> GroupWords(IEnumerable<string> words)
            => words.GroupBy(word => word, (word, eqWord) => new Word(word, eqWord.Count()), StringComparer.InvariantCultureIgnoreCase);
    }
}