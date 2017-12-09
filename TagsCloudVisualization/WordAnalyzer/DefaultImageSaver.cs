using System.Drawing;

namespace TagsCloudVisualization
{
    public class DefaultImageSaver : IImageSaver
    {
        public void Save(Bitmap bitmap, string filename) => bitmap.Save(filename);
    }
}
