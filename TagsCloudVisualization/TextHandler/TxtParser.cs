using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.TextHandler
{
    public class TxtParser : ITextParser
    {
        private Regex wordPattern = new Regex("([a-z]+?)[^a-z]", RegexOptions.IgnoreCase);
        public IEnumerable<string> Parse(string text) => FindWords(text);

        private IEnumerable<string> FindWords(string text)
        {
            var matches = wordPattern.Matches(text);
            for (var i = 0; i < matches.Count; i++)
                yield return matches[i].Groups[1].Value.ToLower();
        }
    }
}