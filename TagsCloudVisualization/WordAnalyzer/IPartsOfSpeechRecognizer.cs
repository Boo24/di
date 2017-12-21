namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IPartsOfSpeechRecognizer
    {
        Result<PartsOfSpeech> Recognize(string word);
    }
}
