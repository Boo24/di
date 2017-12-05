using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IWordsFilter
    {
        IEnumerable<Word> Filter(IEnumerable<Word> words);
        string Name { get; }
    }
}
