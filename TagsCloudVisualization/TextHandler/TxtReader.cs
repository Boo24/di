using System.IO;

namespace TagsCloudVisualization.TextHandler
{
    public class TxtReader : IReader
    {
        public Result<string> Read(string filename) => Result.Of(() =>File.ReadAllText(filename));
    }
}
