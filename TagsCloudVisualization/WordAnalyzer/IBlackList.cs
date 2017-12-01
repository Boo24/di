using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IBlackList
    {
        IEnumerable<string> GetBadWords();
        bool Contains(string word);
    }
}
