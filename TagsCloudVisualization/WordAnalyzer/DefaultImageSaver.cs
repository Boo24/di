using System.Drawing;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class DefaultImageSaver : IImageSaver
    {
        public Bitmap Save(Bitmap bitmap, string filename)
        {
            bitmap.Save(filename);
            return bitmap;
        }
    }
}
