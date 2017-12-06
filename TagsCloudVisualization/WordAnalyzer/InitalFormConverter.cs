﻿using System.Collections.Generic;
using NHunspell;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class InitalFormConverter : IWordConverter
    {
        public WordsConverterType Type { get; } = WordsConverterType.InitalFormConverter;

        public IEnumerable<string> Convert(IEnumerable<string> words)
        {
            using (var hunspell = new Hunspell("en_US.aff", "en_US.dic"))
                foreach (var word in words)
                {
                    var stem = hunspell.Stem(word);
                    yield return stem.Count == 0 ? word : stem[0];
                }

        }
    }
}
