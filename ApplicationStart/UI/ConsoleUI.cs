using System.Drawing;
using System.Drawing.Imaging;
using TagsCloudVisualization;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.TextHandler;

namespace ApplicationStart.UI
{
    public class ConsoleUI :IUI
    {
        private CloudCreater cloudCreater;
        private IReader reader;
        private ITextParser parser;
        private DefaultImageSaver saver;
        public string[] args;
        private TagCloudVizualizer visualizer;
        public ConsoleUI(CloudCreater cloudCreater,IReader reader, ITextParser parser,TagCloudVizualizer visualizer,DefaultImageSaver saver)
        {
            this.cloudCreater = cloudCreater;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
        }
        public void Run()
        {
            var options = new Options();
            CommandLine.Parser.Default.ParseArguments(args, options);
            var text = reader.Read(options.InputFile);
            var words = parser.Parse(text);
            cloudCreater.Create(words, options.MaxFontSize, options.MinFontSize, options.WordsCount, options.Font);
            var bitmap = visualizer.Vizualize(cloudCreater.RectanglesCloud, Color.AliceBlue);
            saver.Save(bitmap, options.OutputFile);
        }
    }
}
