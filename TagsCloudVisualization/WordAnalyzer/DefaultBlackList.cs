using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class DefaultBlackList : IBlackList
    {
        private readonly string[] badWords = new[] { "me", "you", "him", "her", "it", "us", "them", "I", "you", "he", "she", "it", "they", "we","aboard","about","above","across","after","against","along","amid","among","anti","around"," as","at","before","behind","below","beneath","beside","besides","between","beyond","but","by","concerning","considering","despite","down","during","except","excepting","excluding","following","for","from","in","inside","into","like","minus","near","of","off","on","onto","opposite","outside","over","past","per","plus","regarding","round","save","since","than","through","to","toward","towards","under","underneath","unlike","until","up","upon","versus","via","with","within","without" };
        public IEnumerable<string> GetBadWords() => badWords;
        public bool Contains(string word) => badWords.Contains(word);
    }
}
