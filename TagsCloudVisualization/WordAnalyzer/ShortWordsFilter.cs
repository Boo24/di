namespace TagsCloudVisualization.WordAnalyzer
{
    public class ShortWordsFilter : IWordsFilter
    {
        private const int MinWordLength = 4;
        public bool CheckWord(Word word) => word.Text.Length >= MinWordLength;
    }
}
