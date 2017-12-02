using System.Collections.Generic;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.TextHandler
{
    public interface ITextParser
    {
        IEnumerable<string> Parse(string text);
    }
}
