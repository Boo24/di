using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IImageSaver
    {
        Bitmap Save(Bitmap bitmap, string filename);
    }
}
