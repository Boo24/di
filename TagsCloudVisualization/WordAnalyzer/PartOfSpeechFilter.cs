using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpNL.Extensions;
using SharpNL.POSTag;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class PartOfSpeechFilter : IWordsFilter
    {
        public string Name { get; } = "Filter by parts of speech";
        private readonly Dictionary<PartsOfSpeech, string[]> partsOfSpeechAndTags =
            new Dictionary<PartsOfSpeech, string[]>()
            {
                {PartsOfSpeech.Pronoun, new[] {"PRP", "PRP$", "WP", "WP$"}},
                {PartsOfSpeech.Verb, new[] {"MD", "RB", "RBR", "RBS", "TO", "VB", "VBD", "VBN", "VBP", "VBZ", "WRB"}},
                {PartsOfSpeech.Particle, new[] {"RP"}},
                {PartsOfSpeech.Noun, new[] {"NN", "NNS", "NNP", "NNPS"}},
                {PartsOfSpeech.Interjection, new[] {"UH"}},
                {PartsOfSpeech.Number, new[] {"CD"}},
                {PartsOfSpeech.Adjective, new[] {"JJ", "JJR", "JJS", "WDT"}},
                {PartsOfSpeech.Preposition, new[] {"IN"}}
            };

        private IEnumerable<PartsOfSpeech> excludedPartsOfSpeech;
        private POSTaggerME PosTagger;

        public PartOfSpeechFilter() { }
        public PartOfSpeechFilter(IEnumerable<PartsOfSpeech> excludedPartsOfSpeech)
        {
            this.excludedPartsOfSpeech = excludedPartsOfSpeech;
            POSModel posModel;
            using (var modelFile = new FileStream("en-pos-maxent.bin", FileMode.Open))
                posModel = new POSModel(modelFile);
            PosTagger = new POSTaggerME(posModel);
        }

        public IEnumerable<Word> Filter(IEnumerable<Word> words)
        {
            foreach (var word in words)
            {
                var POSTags = PosTagger.Tag(new[] {word.Text});
                if (excludedPartsOfSpeech.All(excludePart => !partsOfSpeechAndTags[excludePart].Contains(POSTags[0])))
                    yield return word;
            }
        }
    }
}