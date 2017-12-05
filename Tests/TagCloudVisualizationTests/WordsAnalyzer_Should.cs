using System.Collections.Generic;
using System.Linq;
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
        private Mock<IWordConverter> fakeConverter;
        private Mock<IWordsFilter> fakeFilter;
        private HashSet<string> fakeConvertesNames;
        private HashSet<string> fakeFiltersNames;
        [SetUp]
        public void SetUp()
        {

            fakeFiltersNames = new HashSet<string>() {"Filter short words"};
            fakeConvertesNames = new HashSet<string>() {"Inital form converter"};
            fakeConverter = new Mock<IWordConverter>();
            fakeFilter = new Mock<IWordsFilter>();
            fakeFilter
                .SetupGet(m => m.Name)
                .Returns("Filter short words");
            fakeFilter
                .Setup(m => m.Filter(It.IsAny<IEnumerable<Word>>()))
                .Returns<IEnumerable<Word>>(k => k);
            fakeConverter
                .SetupGet(m => m.Name)
                .Returns("Inital form converter");
            fakeConverter
                .Setup(m => m.Convert(It.IsAny<IEnumerable<string>>()))
                .Returns<IEnumerable<string>>(k => k);
            analyzer = new WordsAnalyzer(
                new List<IWordsFilter>() {fakeFilter.Object},
                new List<IWordConverter>() {fakeConverter.Object});
        }

        [Test]
        public void SettingsCallOne_WhenWordsCount1()
        {
            var words = new List<string>() { "blabla" };
            analyzer.Analyze(words, 1, fakeFiltersNames, fakeConvertesNames);
            fakeConverter.Verify(o => o.Convert(words), Times.Once);
            fakeFilter
                .Verify(o => o.Filter(It.Is<IEnumerable<Word>>(w => w.Count()==1 && w.First().Text=="blabla" && w.First().CountOfOccurrences==1)),
                Times.Once);
        }

        [Test]
        public void ExcludeShortWords_WhenWordsNotInBlackList()
        {
            fakeFilter
                .Setup(o => o.Filter(It.IsAny<IEnumerable<Word>>()))
                .Returns<IEnumerable<Word>>(w => w.Where(wd=> wd.Text.Length > 3));
            var words = new List<string> { "aa", "aa", "a" };
            analyzer.Analyze(words, 150, fakeFiltersNames, fakeConvertesNames).SortedWordsTop.Should().BeEmpty();
        }

        [Test]
        public void RetutnFirst2Words_WhenWordsCount2()
        {
            var words = new List<string> { "acrosss", "acrosss", "acrosss", "aboutt", "aboutt", "abutt" };
            var actualTopWords = analyzer.Analyze(words, 2, fakeFiltersNames,fakeConvertesNames).SortedWordsTop;
            var expectedTop = new List<Word> { new Word("acrosss", 3), new Word("aboutt", 2) };
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
            var actualSortedWords = analyzer.Analyze(words, 4, fakeFiltersNames, fakeConvertesNames).SortedWordsTop;
            var expectedSortedWords =
                new List<Word>() { new Word("aboutt", 5), new Word("acrosss", 2), new Word("abutt", 1) };
            actualSortedWords.ShouldBeEquivalentTo(expectedSortedWords);
        }
    }
}