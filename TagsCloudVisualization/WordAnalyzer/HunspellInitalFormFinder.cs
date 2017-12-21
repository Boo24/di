using System.Collections.Generic;
using NHunspell;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class HunspellInitalFormFinder : IInitalFormFinder
    {
        public Result<IEnumerable<string>> Find(IEnumerable<string> words) => Result.Of(() => FindInitalForm(words));

        private IEnumerable<string> FindInitalForm(IEnumerable<string> words)
        {
            var result = new List<string>();
            using (var hunspell = new Hunspell("en_US.aff", "en_US.dic"))
                foreach (var word in words)
                {
                    var stem = hunspell.Stem(word);
                    result.Add(stem.Count == 0 ? word : stem[0]);
                }
            return result;
        }
    }
}
