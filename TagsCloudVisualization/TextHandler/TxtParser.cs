using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.TextHandler
{
    public class TxtParser : ITextParser
    {
        private Regex wordPattern = new Regex("([a-z]+?|[а-я]+?)[^a-z,а-я]", RegexOptions.IgnoreCase);
        public IEnumerable<Word> Parse(string text) => GetAllWords(SplitTextByDelimeters(text));

        private IEnumerable<string> SplitTextByDelimeters(string text)
        {
            var matches = wordPattern.Matches(text);
            for (var i = 0; i < matches.Count; i++)
                yield return matches[i].Groups[1].Value.ToLower();
        }

        private IEnumerable<Word> GetAllWords(IEnumerable<string> words)
            => words.GroupBy(word => word, (word, eqWord) => new Word(word, eqWord.Count()),
                StringComparer.InvariantCultureIgnoreCase);
    }
}