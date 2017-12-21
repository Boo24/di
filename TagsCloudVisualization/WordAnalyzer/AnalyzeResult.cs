using System;
using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class AnalyzeResult
    {
        public int MaxCountOfOccurrences { get; }
        public int MinCountOfOccurrences { get; }
        public IEnumerable<Word> SortedWordsTop { get; }
        public AnalyzeResult(int maxCountOfOccurrences, int minCountOfOccurrences, IEnumerable<Word> sortedWordsTop)
        {
            MaxCountOfOccurrences = maxCountOfOccurrences;
            MinCountOfOccurrences = minCountOfOccurrences;
            SortedWordsTop = sortedWordsTop;
        }


    }
}
