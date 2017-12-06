using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization
{
    public class Settings
    {
        public Color BackgroundColor { get; set; }
        public int MinFontSize { get; set; } = 12;
        public int MaxFontSize { get; set; } = 24;
        public int WordsCount { get; set; } = 150;
        public string FontName { get; set; }= "Arial";
        public HashSet<FilterType> UseFilters { get; set; } = new HashSet<FilterType>();
        public HashSet<WordsConverterType> UseConverters { get; set; } = new HashSet<WordsConverterType>();
    }
}
