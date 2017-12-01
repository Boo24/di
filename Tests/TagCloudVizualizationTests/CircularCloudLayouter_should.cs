//using System;
//using System.Drawing;
//using FluentAssertions;
//using NUnit.Framework;
//using NUnit.Framework.Interfaces;

//namespace TagsCloudVisualization
//{
//    [TestFixture]
//    internal class CircularCloudLayouter_Should
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


//        [Test]
//        public void СheckThatStylingIsInTheFormOfCircle()
//        {
//            layouter.PutNextRectangle(new Size(30, 30));
//            layouter.PutNextRectangle(new Size(20, 40));
//            var expectedRadius = 30;
//            foreach (var rect in layouter.RectCloud.LayouterComponents)
//            {
//                var actualRadius =
//                    Math.Sqrt(Math.Pow(center.X - rect.X, 2) + Math.Pow(center.Y - rect.Y, 2));
//                actualRadius.Should().BeLessOrEqualTo(expectedRadius);
//            }
//        }

//        [Test]
//        public void FilledSpaceIsMoreThan70Percent()
//        {
//            var rnd = new Random();
//            for (var i = 0; i <= 2000; i++)
//                layouter.PutNextRectangle(new Size(20, 10));
//            var imageComp = layouter.RectCloud;
//            var totalArea = Math.Pow(imageComp.Size.Height, 2) * Math.PI / 4;
//            var filledArea = imageComp.LayouterComponents.Select(x => x.Size.Height * x.Size.Width).Sum();
//            var percentageOfFill = filledArea / totalArea * 100;


//            (percentageOfFill).Should().BeGreaterOrEqualTo(70);

//        }
//        [TearDown]
//        public void TearDown()
//        {
//            if (Equals(TestContext.CurrentContext.Result.Outcome, ResultState.Failure))
//            {
//                var imageComp = new RectanglesCloud(center);
//                var d = new TagCloudVizualizer(Color.Aquamarine, Brushes.CornflowerBlue, Color.Black);
//                d.Vizualize(imageComp);
//            }
//        }

//    }
//}
