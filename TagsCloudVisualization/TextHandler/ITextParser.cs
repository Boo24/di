using System.Collections.Generic;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.TextHandler
{
    public interface ITextParser
    {
        IEnumerable<Word> Parse(string text);
    }
}
