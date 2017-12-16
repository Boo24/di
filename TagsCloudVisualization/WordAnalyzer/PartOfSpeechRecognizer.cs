using System.Collections.Generic;
using System.IO;
using System.Linq;
using SharpNL.POSTag;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class PartOfSpeechRecognizer : IPartsOfSpeechRecognizer
    {
        private POSTaggerME PosTagger;
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
        public PartOfSpeechRecognizer()
        {
            POSModel posModel;
            using (var modelFile = new FileStream("en-pos-maxent.bin", FileMode.Open))
                posModel = new POSModel(modelFile);
            PosTagger = new POSTaggerME(posModel);
        }
        public PartsOfSpeech Recognize(string word)
        {
           var partOfSpeech = PosTagger.Tag(new[] { word });
           return partsOfSpeechAndTags.FirstOrDefault(k => k.Value.Contains(partOfSpeech[0])).Key;
        }
    }
}
