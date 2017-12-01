using System.Drawing;

namespace TagsCloudVisualization.Geometry.Layouter
{
    public class CircularLayouter : ILayouter
    {
        private readonly ISpiral spiral;
        public CircularLayouter(ISpiral spiral) => this.spiral = spiral;
        public Rectangle PutNextRectangle(Size rectangleSize) => FindFreeRectangle(rectangleSize);
        private Rectangle FindFreeRectangle(Size size)
        {
            while (true)             
            {
                var point = spiral.GetNextPoint();
                var foundRectangle = new Rectangle((int)point.X, (int)point.Y, size.Width, size.Height);
                return foundRectangle;
            }
        }
    }
}
