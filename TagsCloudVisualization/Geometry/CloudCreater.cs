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
        public IRectanglesCloud RectanglesCloud;
        private IFontColorSelector colorSelector;
        private WordsAnalyzer analizer;
        public CloudCreater(WordsAnalyzer analizer,IRectanglesCloud rectanglesCloud, IFontColorSelector colorSelector)
        {
            RectanglesCloud = rectanglesCloud;
            this.colorSelector = colorSelector;
            this.analizer = analizer;
        }

        public void Create(IEnumerable<Word> words, int maxFontSize, int minFontSize, int wordsCount, string fontName)
        {
            var data = analizer.Analyze(words, wordsCount);
            words = data.sortedWords;
            this.minFontSize = minFontSize;
            this.maxFontSize = maxFontSize;
            foreach (var word in words)
            {
                var fontSize = CalculateWordSize(word);
                var rectangleSize = CalculateRectangleSize(word, fontSize, fontName);
                var wordColor = colorSelector.GetColorFor(word);
                RectanglesCloud.PutNextWord(word, rectangleSize, fontSize, wordColor, fontName);
            }
        }

        private int CalculateWordSize(Word word)
        {
            var size= Math.Max(3*(int)((Math.Log(word.CountOfOccurrences) - Math.Log(minFontSize)) /
                   (Math.Log(maxFontSize) - Math.Log(minFontSize))), minFontSize-5);
            return size;
        }

        private Size CalculateRectangleSize(Word word, int wordSize, string fontName)
        {
            Size proposedSize = new Size(int.MaxValue, int.MaxValue);
            TextFormatFlags flags = TextFormatFlags.NoPadding;
            return TextRenderer.MeasureText(word.Text, new Font(fontName, wordSize), proposedSize,flags);
        }
    }
}
