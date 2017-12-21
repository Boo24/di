//using System.Collections.Generic;
//using System.Linq;
//using FluentAssertions;
//using Moq;
//using NUnit.Framework;
//using TagsCloudVisualization.WordAnalyzer;

//namespace Tests.TagCloudVisualizationTests
//{
//    [TestFixture]
//    public class Filters_Should
//    {
//        private ShortWordsFilter shortWordsFilter;
//        private PartOfSpeechFilter partsOfSpeechFilter;
//        private DefaultBlackList blackList;
//        [SetUp]
//        public void SetUp()
//        {
//            shortWordsFilter = new ShortWordsFilter();  
//            var fakePosTagger = new Mock<IPartsOfSpeechRecognizer>();
//            fakePosTagger.Setup(m => m.Recognize(It.IsAny<string>())).Returns(PartsOfSpeech.Adjective);
//            fakePosTagger.Setup(m => m.Recognize(It.Is<string>(v => v == "can"))).Returns(PartsOfSpeech.Verb);
//            fakePosTagger.Setup(m => m.Recognize(It.Is<string>(v => v == "aa"))).Returns(PartsOfSpeech.Unknown);
//            partsOfSpeechFilter = new PartOfSpeechFilter(new []{PartsOfSpeech.Verb}, fakePosTagger.Object);
//            blackList = new DefaultBlackList(new []{"boredum"});
//        }

//        [Test]
//        public  void ShortWordsFilterShuld_FulterWordShorter4_WhenWordsNotNull()
//        {
//            var words = new List<Word> {new Word("aa", 1), new Word("qwerty", 1)};
//            var goodWords = shortWordsFilter.Filter(words).ToList();
//            goodWords.Should().HaveCount(1);
//            goodWords.ShouldBeEquivalentTo(new List<Word> { new Word("qwerty", 1) });
//        }

//        [Test]
//        public void ShortWordsFilterShuld_FulterWordShorter4_WhenWordTextIsNull()
//        {
//            var words = new List<Word> { new Word(null, 1), new Word("qwerty", 1) };
//            var goodWords = shortWordsFilter.Filter(words).ToList();
//            goodWords.Should().HaveCount(1);
//            goodWords.ShouldBeEquivalentTo(new List<Word> { new Word("qwerty", 1) });
//        }

//        [Test]
//        public void ShortWordsFilterShuld_FulterWordShorter4_WhenWordIsNull()
//        {
//            var words = new List<Word> { null, new Word("qwerty", 1) };
//            var goodWords = shortWordsFilter.Filter(words).ToList();
//            goodWords.Should().HaveCount(1);
//            goodWords.ShouldBeEquivalentTo(new List<Word> { new Word("qwerty", 1) });
//        }

//        [Test]
//        public void PartsOfSpeechFilterShould_FilterVerbs_WhenWordsIsNotNull()
//        {
//            var words = new List<Word> {new Word("can", 1), new Word("bla",1)};
//            var goodWords = partsOfSpeechFilter.Filter(words).ToList();
//            goodWords.Should().HaveCount(1);
//            goodWords.ShouldBeEquivalentTo(new List<Word>{new Word("bla",1)});
//        }

//        [Test]
//        public void PartsOfSpeechFilterShould_FilterVerbs_WhenWordTextIsNull()
//        {
//            var words = new List<Word> { new Word("can", 1), new Word(null, 1) };
//            partsOfSpeechFilter.Filter(words).Should().HaveCount(0);
//        }

//        [Test]
//        public void PartsOfSpeechFilterShould_FilterVerbs_WhenWordIsNull()
//        {
//            var words = new List<Word> { new Word("can", 1), null };
//            partsOfSpeechFilter.Filter(words).Should().HaveCount(0);
//        }

//        [Test]
//        public void PartsOfSpeechFilterShould_NotFilter_WhenWordIsUnknowPartOfSpeech()
//        {
//            var words = new List<Word> { new Word("aa", 1), new Word("blabla",1) };
//            var goodWords = partsOfSpeechFilter.Filter(words).ToList();
//            goodWords.Should().HaveCount(2);
//            goodWords.ShouldBeEquivalentTo(words);
//        }

//        [Test]
//        public void BoringWordsFilterShould_FilterWordsFromBoringWordsList()
//        {
//            var words = new List<Word> { new Word("boredum", 1), new Word("blabla", 1) };
//            var goodWords = blackList.Filter(words).ToList();
//            goodWords.Should().HaveCount(1);
//            goodWords.ShouldBeEquivalentTo(new List<Word>(){new Word("blabla",1)});
//        }
//    }
//}