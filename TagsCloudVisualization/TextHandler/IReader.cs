namespace TagsCloudVisualization.TextHandler
{
    public interface IReader
    {
        Result<string> Read(string filename);
    }
}
