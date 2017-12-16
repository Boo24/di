using System.Drawing;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry.Layouter
{
    public class LayouterComponent
    {
        public Word Word { get; }
        public Rectangle Location { get; }
        public int FontSize { get; }
        public Brush WordColor { get; }
        public string FontName { get; }
        public LayouterComponent(Word word, Rectangle location, int fontSize, Brush wordColorBrush, string fontName)
        {
            Word = word;
            Location = location;
            FontSize = fontSize;
            WordColor = wordColorBrush;
            FontName = fontName;
        }
    }
}
