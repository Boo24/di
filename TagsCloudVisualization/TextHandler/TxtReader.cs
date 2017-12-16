using System.IO;

namespace TagsCloudVisualization.TextHandler
{
    public class TxtReader : IReader
    {
        public string Read(string filename) => File.ReadAllText(filename);
    }
}
