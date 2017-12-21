using System.Collections.Generic;

namespace TagsCloudVisualization.TextHandler
{
    public interface ITextParser
    {
        Result<IEnumerable<string>> Parse(string text);
    }
}
