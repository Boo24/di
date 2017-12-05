using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IWordConverter
    {
        string Name { get; }
        IEnumerable<string> Convert(IEnumerable<string> words);
    }
}
