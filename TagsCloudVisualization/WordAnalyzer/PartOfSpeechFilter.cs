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

        public Result<IEnumerable<Word>> Filter(IEnumerable<Word> words)
        {
            var result = new List<Word>();
            foreach (var word in words)
            {
                if (word is null || word.Text is null) continue;
                var recognizeResult = partOfSpeechRecognizer.Recognize(word.Text);
                if (!recognizeResult.IsSuccess) return Result.Warning(recognizeResult.ErrorMessage, words);
                if (!excludedPartsOfSpeech.Contains(recognizeResult.Value))
                    result.Add(word);
            }
            return Result.Ok((IEnumerable<Word>)result);
        }


    }
}