using System.Collections.Generic;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class InitalFormConverter : IWordConverter
    {
        public WordsConverterType Type { get; } = WordsConverterType.InitalFormConverter;
        private IInitalFormFinder initalFormFinder;
        public InitalFormConverter(IInitalFormFinder initalFormFinder) => this.initalFormFinder = initalFormFinder;

        public IEnumerable<string> Convert(IEnumerable<string> words) => initalFormFinder.Find(words);
    }
}
