using System.Collections.Generic;
using System.Drawing;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.Geometry.Layouter;
using TagsCloudVisualization.WordAnalyzer;

namespace TagsCloudVisualization.Tests
{
    [TestFixture]
    class RectanglecCloud_Should
    {
        private RectanglesCloud rectCloud;
        [SetUp]
        public void SetUp()
        {
            rectCloud = new RectanglesCloud(new CircularLayouter(new ArchimedeanSpiral()));
        }

        [Test]
        public void AddWordsInLayouterComponents()
        {
            var words = new List<Word>() { new Word("acrosss", 2), new Word("aboutt", 2), new Word("abutt", 1) };
            foreach (var word in words)
                rectCloud.PutNextWord(word, new Size(2, 3), 5, Brushes.AliceBlue, "Arial");
            rectCloud.LayouterComponents.Count.ShouldBeEquivalentTo(3);
        }

        [Test]
        public void СalculateSizeCorrectly()
        {
            var words = new List<Word>() { new Word("acrosss", 2), new Word("aboutt", 2), new Word("abutt", 1) };
            foreach (var word in words)
                rectCloud.PutNextWord(word, new Size(2, 3), 5, Brushes.AliceBlue, "Arial");
            rectCloud.Size.ShouldBeEquivalentTo(new Size(6, 5));
        }
    }
}
