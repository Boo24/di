using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IImageSaver
    {
        Result<Bitmap> Save(Bitmap bitmap, string filename);
    }
}
