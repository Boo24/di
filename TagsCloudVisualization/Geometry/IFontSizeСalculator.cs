using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public interface IFontSizeСalculator
    {
        int Calculate(Word word, int minFontSize, int maxFontSize, int maxCountOfOccurrences = 0, int minCountOfOccurrences = 0);
    }
}
