using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Geometry.Layouter;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Geometry
{
    public class RectanglesCloud : IRectanglesCloud   
    {
        public Size Size { get; private set; }
        public List<LayouterComponent> LayouterComponents { get; }
        private const int StepToCenterCount = 30;
        public Point Center { get; }
        private ILayouter layouter;
        private int leftBorder;
        private int rightBorder;
        private int downBorder;
        private int upBorder;
        public RectanglesCloud(ILayouter layouter)
        {
            LayouterComponents = new List<LayouterComponent>();
            Center = Point.Empty;
            this.layouter = layouter;
            Size = new Size(100,100);
        }
        private void UpdateBordersAndSize(Rectangle newRectangle)
        {
            if (newRectangle.X < leftBorder)
                leftBorder = newRectangle.X;
            if (newRectangle.Y < downBorder)
                downBorder = newRectangle.Y;
            if (newRectangle.X + newRectangle.Width > rightBorder)
                rightBorder = newRectangle.X + newRectangle.Width;
            if (newRectangle.Y + newRectangle.Height > upBorder)
                upBorder = newRectangle.Bottom;
            Size = new Size(rightBorder - leftBorder, upBorder - downBorder);
        }

        public void PutNextWord(Word word, Size size, int fontSize, Brush wordColor, string fontName)
        {
            var foundRect = Rectangle.Empty;
            while (foundRect == Rectangle.Empty || CheckIntersectionWithExistigRectangles(foundRect))
                foundRect = layouter.PutNextRectangle(size);
            foundRect = MoveRectangleToCenter(foundRect);
            LayouterComponents.Add(new LayouterComponent(word, foundRect, fontSize, wordColor, fontName));
            UpdateBordersAndSize(foundRect);
        }

        internal bool CheckIntersectionWithExistigRectangles(Rectangle rect)
        {
            for (var i=LayouterComponents.Count-1; i>=0; i--)
                if (LayouterComponents[i].Location.IntersectsWith(rect))
                    return true;
            return false;
        }
        internal Rectangle MoveRectangleToCenter(Rectangle rect)
        {
            int lastGoodX;
            int lastGoodY;
            var curX = lastGoodX = rect.X;
            var curY = lastGoodY = rect.Y;
            var stepCount = 0;
            while (curX != Center.X && curY != Center.Y && stepCount != StepToCenterCount)
            {
                curX = MoveCoordinateToCenter(curX, Center.X);
                curY = MoveCoordinateToCenter(curY, Center.Y);
                var tempRect = new Rectangle(new Point(curX, curY), rect.Size);
                if (!CheckIntersectionWithExistigRectangles(tempRect))
                {
                    lastGoodX = curX;
                    lastGoodY = curY;
                }
                else
                    stepCount += 1;
            }
            return new Rectangle(new Point(lastGoodX, lastGoodY), rect.Size);
        }
        internal int MoveCoordinateToCenter(int cur, int center) =>
            cur < center ? cur + 1 : cur - 1;

        public void Restart()
        {
            LayouterComponents.Clear();
            layouter.Restart();
        }
    }
}
