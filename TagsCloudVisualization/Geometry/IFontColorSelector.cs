using System.Drawing;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public interface IFontColorSelector
    {
        Brush GetColorFor(Word word);
    }
}
