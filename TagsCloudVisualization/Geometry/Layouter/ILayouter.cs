using System.Drawing;

namespace TagsCloudVisualization.Geometry.Layouter
{
    public interface ILayouter
    {
        Rectangle PutNextRectangle(Size size);
    }
}
