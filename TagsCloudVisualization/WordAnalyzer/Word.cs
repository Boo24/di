namespace TagsCloudVisualization.WordAnalyzer
{
    public class Word
    {
        public string Text { get; }
        public int CountOfOccurrences { get; }
        public Word(string value, int countOfOccurrences)
        {
            CountOfOccurrences = countOfOccurrences;
            Text = value;
        }


    }
}
