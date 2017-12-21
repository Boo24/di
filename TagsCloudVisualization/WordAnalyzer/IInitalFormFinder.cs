using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IInitalFormFinder
    {
        Result<IEnumerable<string>> Find(IEnumerable<string> words);
    }
}
