using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public interface IWordConverter
    {
        WordsConverterType Type { get; }
        IEnumerable<string> Convert(IEnumerable<string> words);
    }

    public enum WordsConverterType
    {
        InitalFormConverter
    }
}
