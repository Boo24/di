using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IInitalFormFinder
    {
        IEnumerable<string> Find(IEnumerable<string> words);
    }
}
