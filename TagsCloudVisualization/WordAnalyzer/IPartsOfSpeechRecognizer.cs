namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IPartsOfSpeechRecognizer
    {
        PartsOfSpeech Recognize(string word);
    }
}
