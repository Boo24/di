using System.Collections.Generic;

namespace TagsCloudVisualization.TextHandler
{
    public interface ITextParser
    {
        IEnumerable<string> Parse(string text);
    }
}
