using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class PartOfSpeechFilter : IWordsFilter
    {
        public FilterType Type { get; } = FilterType.PartsOfSpeechFilter;
        private IEnumerable<PartsOfSpeech> excludedPartsOfSpeech;
        private IPartsOfSpeechRecognizer partOfSpeechRecognizer;

        public PartOfSpeechFilter(IEnumerable<PartsOfSpeech> excludedPartsOfSpeech, IPartsOfSpeechRecognizer recognizer)
        {
            this.excludedPartsOfSpeech = excludedPartsOfSpeech;
            partOfSpeechRecognizer = recognizer;
        }

        public IEnumerable<Word> Filter(IEnumerable<Word> words) =>
            words.Where(w => w?.Text != null && !excludedPartsOfSpeech.Contains(partOfSpeechRecognizer.Recognize(w.Text)));

    }
}