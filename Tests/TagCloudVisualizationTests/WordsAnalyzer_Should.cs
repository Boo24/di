using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using TagsCloudVisualization.WordAnalyzer;

namespace Tests.TagCloudVisualizationTests
{
    [TestFixture]
    public class WordsAnalyzer_Should
    {
        private WordsAnalyzer analyzer;
        private Mock<IBlackList> fakeBlackList;
        private Mock<IWordConverter> fakeConverter;
        private Mock<IWordsFilter> fakeFilter;

        [SetUp]
        public void SetUp()
        {
            fakeBlackList = new Mock<IBlackList>();
            fakeConverter = new Mock<IWordConverter>();
            fakeFilter = new Mock<IWordsFilter>();
            fakeFilter.Setup(m => m.CheckWord(It.IsAny<Word>())).Returns(true);
            fakeBlackList.Setup(m => m.Contains(It.IsAny<string>())).Returns(false);
            fakeConverter.Setup(m => m.Convert(It.IsAny<IEnumerable<string>>())).Returns<IEnumerable<string>>(k => k);
            analyzer = new WordsAnalyzer(fakeBlackList.Object,
                new List<IWordsFilter>() {fakeFilter.Object},
                new List<IWordConverter>() {fakeConverter.Object});
        }

        [Test]
        public void SettingsCallOne_WhenWordsCount1()
        {
            var words = new List<string>() {"blabla"};
            analyzer.Analyze(words, 1);
            fakeBlackList.Verify(o => o.Contains("blabla"), Times.Once);
            fakeConverter.Verify(o => o.Convert(words), Times.Once);
            fakeFilter.Verify(o => o.CheckWord(It.Is<Word>(w => w.Text == "blabla" && w.CountOfOccurrences == 1)),
                Times.Once);
        }

        [Test]
        public void ExcludeShortWords_WhenWordsNotInBlackList()
        {
            fakeFilter.Setup(o => o.CheckWord(It.IsAny<Word>())).Returns<Word>(w => w.Text.Length > 3);
            var words = new List<string> {"aa", "aa", "a"};
            analyzer.Analyze(words, 150).sortedWords.Should().BeEmpty();
        }

        [Test]
        public void ExcludeWordsFromBlackLisxt_WhenWordsNotShort()
        {
            fakeBlackList.Setup(m => m.Contains(It.IsAny<string>()))
                .Returns<string>(s => s == "across" || s == "about");
            var words = new List<string> {"across", "across", "about"};
            analyzer.Analyze(words, 150).sortedWords.Should().BeEmpty();
        }

        [Test]
        public void RetutnFirst2Words_WhenWordsCount2()
        {
            var words = new List<string> {"acrosss", "acrosss", "acrosss", "aboutt", "aboutt", "abutt"};
            var actualTopWords = analyzer.Analyze(words, 2).sortedWords;
            var expectedTop = new List<Word> {new Word("acrosss", 3), new Word("aboutt", 2)};
            actualTopWords.ShouldBeEquivalentTo(expectedTop);
        }

        [Test]
        public void ReturnWordsInDescendingOrder()
        {
            var words = new List<string>()
            {
                "acrosss",
                "acrosss",
                "aboutt",
                "aboutt",
                "aboutt",
                "aboutt",
                "aboutt",
                "abutt"
            };
            var actualSortedWords = analyzer.Analyze(words, 4).sortedWords;
            var expectedSortedWords =
                new List<Word>() {new Word("aboutt", 5), new Word("acrosss", 2), new Word("abutt", 1)};
            actualSortedWords.ShouldBeEquivalentTo(expectedSortedWords);
        }
    }
}