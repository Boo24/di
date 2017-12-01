using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.TextHandler
{
    public class TxtParser : ITextParser
    {
        private char[] delimeters = new[] {' ', '.', ',', '!', '—', '-', '?'};
        public IEnumerable<Word> Parse(string text) => GetAllWords(SplitTextByDelimeters(text));

        private IEnumerable<string> SplitTextByDelimeters(string text) =>
            text.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

        private IEnumerable<Word> GetAllWords(IEnumerable<string> words)
            => words.GroupBy(word => word, (word, eqWord) => new Word(word, eqWord.Count()),
                StringComparer.InvariantCultureIgnoreCase);
    }
}