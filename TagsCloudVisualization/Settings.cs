using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public class Settings
    {
        public Color BackgroundColor { get; set; }
        public int MinFontSize { get; set; } = 12;
        public int MaxFontSize { get; set; } = 24;
        public int WordsCount { get; set; } = 150;
        public string FontName { get; set; }= "Arial";
        public HashSet<string> UseFilters { get; set; }= new HashSet<string>();
        public HashSet<string> UseConverters { get; set; } = new HashSet<string>();
    }
}
