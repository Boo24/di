using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IWordConverter
    {
        IEnumerable<string> Convert(IEnumerable<string> words);
    }
}
