using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IWordsFilter
    {
        bool CheckWord(Word word);
    }
}
