using System.Drawing;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public interface IFontColorSelector
    {
        Result<Brush> GetColorFor(Word word);
    }
}
