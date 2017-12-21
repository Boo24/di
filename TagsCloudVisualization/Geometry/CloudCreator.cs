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
        private IRectanglesCloud RectanglesCloud;
        private IFontColorSelector colorSelector;
        private IFontSizeСalculator fontSizeСalculator;

        public CloudCreator(IRectanglesCloud rectanglesCloud, IFontColorSelector colorSelector, IFontSizeСalculator fontSizeСalculator)
        {
            RectanglesCloud = rectanglesCloud;
            this.colorSelector = colorSelector;
            this.fontSizeСalculator = fontSizeСalculator;
        }

        public Result<IRectanglesCloud> Create(AnalyzeResult analyzeResult, int maxFontSize, int minFontSize, string fontName)
        {
                this.minFontSize = minFontSize;
                this.maxFontSize = maxFontSize;
                minCountOfOccurrences = analyzeResult.MinCountOfOccurrences;
                maxCountOfOccurrences = analyzeResult.MaxCountOfOccurrences;
                foreach (var word in analyzeResult.SortedWordsTop)
                {
                    var fontSize = CalculateWordSize(word);
                    var rectangleSize = fontSize.Then(fz => CalculateRectangleSize(word, fz, fontName));
                    if (!rectangleSize.IsSuccess) return Result.Fail<IRectanglesCloud>(rectangleSize.Error);
                    var wordColor = colorSelector.GetColorFor(word);
                    if (!wordColor.IsSuccess) return Result.Fail<IRectanglesCloud>(wordColor.Error);
                    RectanglesCloud.PutNextWord(word, rectangleSize.Value, fontSize.Value, wordColor.Value, fontName);
                }
                return RectanglesCloud.AsResult();
        }

        private Result<int> CalculateWordSize(Word word) =>
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