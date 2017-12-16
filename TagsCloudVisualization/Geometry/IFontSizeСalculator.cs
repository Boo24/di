using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public interface IFontSizeСalculator
    {
        int Calculate(Word word, int minFontSize, int maxFontSize, int maxCountOfOccurrences = 0, int minCountOfOccurrences = 0);
    }
}
