using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public class CloudCreator
    {
        private int maxFontSize;
        private int minFontSize;
        private int maxCountOfOccurrences;
        private int minCountOfOccurrences;
        public IRectanglesCloud RectanglesCloud;
        private IFontColorSelector colorSelector;
        private IFontSizeСalculator fontSizeСalculator;
        private WordsAnalyzer analizer { get; }

        public CloudCreator(WordsAnalyzer analizer, IRectanglesCloud rectanglesCloud, IFontColorSelector colorSelector,
            IFontSizeСalculator fontSizeСalculator)
        {
            RectanglesCloud = rectanglesCloud;
            this.colorSelector = colorSelector;
            this.analizer = analizer;
            this.fontSizeСalculator = fontSizeСalculator;
        }

        public void Create(IEnumerable<string> wordsFlow, int maxFontSize, int minFontSize, int wordsCount,
            string fontName, IEnumerable<FilterType> useFilters, IEnumerable<WordsConverterType> useConverters)
        {
            var analyzeResult = analizer.Analyze(wordsFlow, wordsCount, useFilters, useConverters);
            this.minFontSize = minFontSize;
            this.maxFontSize = maxFontSize;
            this.minCountOfOccurrences = analyzeResult.MinCountOfOccurrences;
            this.maxCountOfOccurrences = analyzeResult.MaxCountOfOccurrences;
            foreach (var word in analyzeResult.SortedWordsTop)
            {
                var fontSize = CalculateWordSize(word);
                var rectangleSize = CalculateRectangleSize(word, fontSize, fontName);
                var wordColor = colorSelector.GetColorFor(word);
                RectanglesCloud.PutNextWord(word, rectangleSize, fontSize, wordColor, fontName);
            }
        }

        private int CalculateWordSize(Word word) =>
            fontSizeСalculator.Calculate(word, minFontSize, maxFontSize, maxCountOfOccurrences, minCountOfOccurrences);

        private Size CalculateRectangleSize(Word word, int wordSize, string fontName)
        {
            var proposedSize = new Size(int.MaxValue, int.MaxValue);
            var flags = TextFormatFlags.NoPadding;
            return TextRenderer.MeasureText(word.Text, new Font(fontName, wordSize), proposedSize, flags);
        }

        public void Clear() => RectanglesCloud.Restart();
    }
}