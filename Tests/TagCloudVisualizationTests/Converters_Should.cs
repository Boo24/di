using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization.WordAnalyzer;

namespace Tests.TagCloudVisualizationTests
{
    [TestFixture]
    public class Converters_Should
    {
        private InitalFormConverter initalFormConverter;

        [SetUp]
        public void SetUp()
        {
            var fakeIInitalFormFinder = new Mock<IInitalFormFinder>();
            fakeIInitalFormFinder
                .Setup(m => m.Find(It.IsAny<IEnumerable<string>>()))
                .Returns<IEnumerable<string>>(s => s);
            fakeIInitalFormFinder
                .Setup(m => m.Find(It.Is<IEnumerable<string>>(e => e.Contains("book") && e.Contains("books"))))
                .Returns<IEnumerable<string>>(k => k.Where(s => s != "books").Append("book"));
            initalFormConverter = new InitalFormConverter(fakeIInitalFormFinder.Object);

        }

        [Test]
        public void ConvertInInitalForm_WhenWordsIsNotNull()
        {
            var words = new[] {"book", "books", "blabla"};
            var convertWords = initalFormConverter.Convert(words).ToList();
            convertWords.Count.Should().Be(3);
            convertWords.ShouldAllBeEquivalentTo(new List<string> {"blabla", "book", "book"});
        }

        [Test]
        public void ConvertInInitalForm_WhenWordIsNull()
        {
            var words = new[] { "book", "books", null };
            var convertWords = initalFormConverter.Convert(words).ToList();
            convertWords.Count.Should().Be(3);
            convertWords.ShouldAllBeEquivalentTo(new List<string> { "book", "book", null });
        }

    }
}
