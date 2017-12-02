using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TagsCloudVisualization.WordAnalyzer;

namespace Tests.TagCloudVizualizationTests
{
    [TestFixture]
    public class WordsAnalyzer_Should
    {
        private WordsAnalyzer analyzer;
        [SetUp]
        public void SetUp()
        {
            analyzer = new WordsAnalyzer(new DefaultBlackList(), new List<IWordsFilter>() { new ShortWordsFilter() }, new List<IWordConverter>(){new InitalFormConverter()});
        }

        [Test]
        public void ExcludeShortWords_WhenWordsNotInBlackList()
        {
            var words = new List<string>() {"aa", "aa", "a"};
            analyzer.Analyze(words, 150).sortedWords.Should().BeEmpty();
        }

        [Test]
        public void ExcludeWordsFromBlackLisxt_WhenWordsNotShort()
        {
            var words = new List<string>() { "across", "across", "about" };
            analyzer.Analyze(words, 150).sortedWords.Should().BeEmpty();
        }

        [Test]
        public void RetutnFirst2Words_WhenWordsCount2()
        {
            var words = new List<string>() { "acrosss", "acrosss", "acrosss", "aboutt", "aboutt", "abutt"};
            var actualTopWords = analyzer.Analyze(words, 2).sortedWords;
            var expectedTop = new List<Word>() { new Word("acrosss", 3), new Word("aboutt", 2) };
            actualTopWords.ShouldBeEquivalentTo(expectedTop);
        }

        [Test]
        public void ReturnWordsInDescendingOrder()
        {
            var words = new List<string>() { "acrosss", "acrosss", "aboutt", "aboutt", "aboutt", "aboutt", "aboutt", "abutt"};
            var actualSortedWords = analyzer.Analyze(words, 4).sortedWords;
            var expectedSortedWords = new List<Word>() { new Word("aboutt", 5), new Word("acrosss", 2), new Word("abutt", 1) };
            actualSortedWords.ShouldBeEquivalentTo(expectedSortedWords);
        }

    }
}
