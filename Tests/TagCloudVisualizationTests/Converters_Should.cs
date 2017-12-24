//using System.Collections.Generic;
//using System.Linq;
//using FluentAssertions;
//using Moq;
//using NUnit.Framework;
//using TagsCloudVisualization;
//using TagsCloudVisualization.WordAnalyzer;

//namespace Tests.TagCloudVisualizationTests
//{
//    [TestFixture]
//    public class Converters_Should
//    {
//        private InitalFormConverter initalFormConverter;

//        [SetUp]
//        public void SetUp()
//        {
//            var fakeIInitalFormFinder = new Mock<IInitalFormFinder>();
//            fakeIInitalFormFinder
//                .Setup(m => m.Find(It.IsAny<IEnumerable<string>>()))
//                .Returns<Result<IEnumerable<string>>>(s => s);
//            fakeIInitalFormFinder
//                .Setup(m => m.Find(It.Is<IEnumerable<string>>(e => e.Contains("book") && e.Contains("books"))))
//                .Returns<Result<IEnumerable<string>>>(k => Result.Ok(k.Value.Where(s => s != "books").Append("book")));
//            initalFormConverter = new InitalFormConverter(fakeIInitalFormFinder.Object);

//        }

//        [Test]
//        public void ConvertInInitalForm_WhenWordsIsNotNull()
//        {
//            var words = new List<string> { "book", "books", "blabla" };
//            var convertWords = initalFormConverter.Convert(words).Value.ToList();
//            convertWords.Count.Should().Be(3);
//            convertWords.ShouldAllBeEquivalentTo(new List<string> { "blabla", "book", "book" });
//        }

//        [Test]
//        public void ConvertInInitalForm_WhenWordIsNull()
//        {
//            var words = new List<string> { "book", "books", null };
//            var convertWords = initalFormConverter.Convert(words).Value.ToList();
//            convertWords.Count.Should().Be(3);
//            convertWords.ShouldAllBeEquivalentTo(new List<string> { "book", "book", null });
//        }

//    }
//}
