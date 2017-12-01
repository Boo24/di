//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using FluentAssertions;
//using NUnit.Framework;
//using TagsCloudVisualization;
//using TagsCloudVisualization.TextReader;

//namespace TagsCloudVisualization.Tests
//{
//    [TestFixture]
//    class RectanglecCloud_Should
//    {
//        private CircularLayouter layouter;
//        private Point center;
//        [SetUp]
//        public void SetUp()
//        {
//            center = new Point(300, 300);
//            layouter = new CircularLayouter(center);
//            layouter.PutNextRectangle(new Size(20, 10));
//            layouter.PutNextRectangle(new Size(20, 10));
//        }

//        [TestCase(0, 0, 10, 20, ExpectedResult = false, TestName = "Add in left-up")]
//        [TestCase(330, 14, 10, 20, ExpectedResult = false, TestName = "Add in down")]
//        [TestCase(370, 300, 10, 20, ExpectedResult = false, TestName = "Add in rigt")]
//        public bool NotIntersection_WhenAddRectangeInFreeSpace(int x, int y, int width, int height)
//        {
//            return layouter.RectCloud.CheckIntersectionWithExistigRectangles(new Rectangle(x, y, width, height));
//        }

//        [TestCase(300, 300, 10, 20, ExpectedResult = true, TestName = "Add in center")]
//        [TestCase(310, 300, 10, 20, ExpectedResult = true, TestName = "Add in another rectangle Space")]
//        public bool ExistIntersection_WhenAddRectangeInOccupiedSpace(int x, int y, int width, int height)
//        {
//            return layouter.RectCloud.CheckIntersectionWithExistigRectangles(new Rectangle(x, y, width, height));
//        }

//        [Test]
//        public void EmptyRectanglesAndZeroSize_WhenNoRectangles()
//        {
//            var expectedSize = new Size(0, 0);
//            var rectanglesCloud = new RectanglesCloud(new Point(0, 0));

//            rectanglesCloud.Size.ShouldBeEquivalentTo(expectedSize);
//        }

//        [Test]
//        public void CalculateSize_WhenRectanglesWithNegativeCoordinates()
//        {
//            var expectedSize = new Size(15, 35);
//            var rectanglesCloud = new RectanglesCloud(new Point(-10, -10));
//            rectanglesCloud.PutNextWord(new Rectangle(-10, -10, 5, 5));
//            rectanglesCloud.PutNextWord(new Rectangle(-20, -15, 5, 5));
//            rectanglesCloud.PutNextWord(new Rectangle(-20, -40, 10, 10));
//            rectanglesCloud.Size.ShouldBeEquivalentTo(expectedSize);
//        }
//        [Test]
//        public void CalculateSize_WhenRectanglesWithPositiveCoordinates()
//        {
//            var expectedSize = new Size(20, 40);
//            var rectanglesCloud = new RectanglesCloud(new Point(10, 10));
//            rectanglesCloud.PutNextWord(new Rectangle(10, 10, 5, 5));
//            rectanglesCloud.PutNextWord(new Rectangle(20, 15, 5, 5));
//            rectanglesCloud.PutNextWord(new Rectangle(20, 40, 10, 10));
//            rectanglesCloud.Size.ShouldBeEquivalentTo(expectedSize);
//        }

//        [Test]
//        public void CalculateSize_WhenRectanglesWithPositiveAndNeganiveCoordinates()
//        {
//            var expectedSize = new Size(50, 55);
//            var rectanglesCloud = new RectanglesCloud(new Point(10, 10));
//            rectanglesCloud.PutNextWord(new Rectangle(10, 10, 5, 5));
//            rectanglesCloud.PutNextWord(new Rectangle(-20, -15, 5, 5));
//            rectanglesCloud.PutNextWord(new Rectangle(20, -40, 10, 10));
//            rectanglesCloud.Size.ShouldBeEquivalentTo(expectedSize);
//        }

//        [Test]
//        public void METHOD()
//        {
//            var a = new TxtParser(new[] { ' ' });
//            var b = a.GetAllWords(new List<string>() { "a", "a", "a", "b", "c", "c", "c", "c" });
//            var analyzer = new WordsAnalyzer(null);

//        }
//    }
//}
