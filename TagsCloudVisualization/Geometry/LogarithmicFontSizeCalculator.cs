using System;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public class LogarithmicFontSizeCalculator : IFontSizeСalculator
    {
        private const int FontSizeCoefficient = 5;
        public int Calculate(Word word, int minFontSize, int maxFontSize, int maxCountOfOccurrences = 0, int minCountOfOccurrences = 0)
        {
            return Math.Max(FontSizeCoefficient * (int)((Math.Log(word.CountOfOccurrences) - Math.Log(minFontSize)) /
                                        (Math.Log(maxFontSize) - Math.Log(minFontSize))), minFontSize);
        }
    }
}
