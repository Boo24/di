using System.Drawing;

namespace TagsCloudVisualization.Geometry
{
    public class TagCloudVisualizer
    {
        private const int FrameSize = 30; 
        public Result<Bitmap> Vizualize(IRectanglesCloud cloud, Color backgroundColor)
        {
            var bitmap = new Bitmap(cloud.Size.Width + FrameSize, cloud.Size.Height + FrameSize);
                using (var gr = Graphics.FromImage(bitmap))
                {
                    gr.TranslateTransform(cloud.Size.Width / 2 - cloud.Center.X, cloud.Size.Height / 2 - cloud.Center.Y);
                    gr.Clear(backgroundColor);
                    foreach (var component in cloud.LayouterComponents)
                        gr.DrawString(component.Word.Text, new Font(component.FontName, component.FontSize),
                            component.WordColor, component.Location);
                }
            return Result.Ok(bitmap);
        }
    }
}