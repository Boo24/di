using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public class CloudCreater
    {
        private int maxFontSize;
        private int minFontSize;
        private int maxCountOfOccurrences;
        private int minCountOfOccurrences;
        public IRectanglesCloud RectanglesCloud;
        private IFontColorSelector colorSelector;
        private IFontSizeСalculator fontSizeСalculator;
        private WordsAnalyzer analizer;
        public CloudCreater(WordsAnalyzer analizer,IRectanglesCloud rectanglesCloud, IFontColorSelector colorSelector, IFontSizeСalculator fontSizeСalculator)
        {
            RectanglesCloud = rectanglesCloud;
            this.colorSelector = colorSelector;
            this.analizer = analizer;
            this.fontSizeСalculator = fontSizeСalculator;
        }

        public void Create(IEnumerable<string> wordsFlow, int maxFontSize, int minFontSize, int wordsCount, string fontName)
        {
            var data = analizer.Analyze(wordsFlow, wordsCount);
            var words = data.sortedWords;
            this.minFontSize = minFontSize;
            this.maxFontSize = maxFontSize;
            this.minCountOfOccurrences = data.minCount;
            this.maxCountOfOccurrences = data.maxCount;
            foreach (var word in words)
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
            return TextRenderer.MeasureText(word.Text, new Font(fontName, wordSize), proposedSize,flags);
        }

        public void Clear() => RectanglesCloud.Restart();
    }
}
