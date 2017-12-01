using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Geometry.Layouter;
using TagsCloudVisualization.WordAnalyzer;


namespace TagsCloudVisualization.Geometry
{
    public interface IRectanglesCloud
    {
        void PutNextWord(Word word, Size size, int fontSize, Brush wordColor, string fontName);
        Size Size { get; }
        List<LayouterComponent> LayouterComponents { get; }
        Point Center { get; }

    }
}
