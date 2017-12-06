using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IWordsFilter
    {
        IEnumerable<Word> Filter(IEnumerable<Word> words);
        FilterType Type { get; }
    }

    public enum FilterType
    {
        BoringWordsFilter,
        PartsOfSpeechFilter,
        FilterShortWords
    }
}
