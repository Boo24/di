using System.Collections.Generic;
using System.Linq;
using SharpNL.POSTag;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class PartOfSpeechRecognizer : IPartsOfSpeechRecognizer
    {
        private Result<POSTaggerME> PosTagger;
        private readonly Dictionary<PartsOfSpeech, string[]> partsOfSpeechAndTags =
            new Dictionary<PartsOfSpeech, string[]>()
            {
                {PartsOfSpeech.Unknown, new []{"."}},
                {PartsOfSpeech.Pronoun, new[] {"PRP", "PRP$", "WP", "WP$"}},
                {PartsOfSpeech.Verb, new[] {"MD", "RB", "RBR", "RBS", "TO", "VB", "VBD", "VBN", "VBP", "VBZ", "WRB", "VBG"}},
                {PartsOfSpeech.Particle, new[] {"RP"}},
                {PartsOfSpeech.Noun, new[] {"NN", "NNS", "NNP", "NNPS"}},
                {PartsOfSpeech.Interjection, new[] {"UH"}},
                {PartsOfSpeech.Number, new[] {"CD"}},
                {PartsOfSpeech.Adjective, new[] {"JJ", "JJR", "JJS", "WDT"}},
                {PartsOfSpeech.Preposition, new[] {"IN"}}
            };
        public PartOfSpeechRecognizer() =>
            PosTagger = Result.Of(() => new POSModel("en-pos-maxent.bin")).Then(r => new POSTaggerME(r));

        public Result<PartsOfSpeech> Recognize(string word)
        {
            if (!PosTagger.IsSuccess) return Result.Fail<PartsOfSpeech>(PosTagger.Error).RefineError("No resolver");
            var partOfSpeech = PosTagger.Value.Tag(new[] {word});
            return partsOfSpeechAndTags.FirstOrDefault(k => k.Value.Contains(partOfSpeech[0])).Key.AsResult();
        }
    }
}
