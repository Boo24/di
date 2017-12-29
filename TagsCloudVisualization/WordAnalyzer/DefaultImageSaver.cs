using System.Drawing;

namespace TagsCloudVisualization.WordAnalyzer
{
    public class DefaultImageSaver : IImageSaver
    {
        public Result<Bitmap> Save(Bitmap bitmap, string filename) => 
            Result.Of(() =>
            {
                bitmap.Save(filename);
                return bitmap;
            });
    }
}
