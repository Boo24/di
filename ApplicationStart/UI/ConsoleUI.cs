using System.Drawing;
using TagsCloudVisualization;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.TextHandler;

namespace ApplicationStart.UI
{
    public class ConsoleUI :IUI
    {
        private CloudCreator cloudCreator;
        private IReader reader;
        private ITextParser parser;
        private IImageSaver saver;
        public string[] Args { get; private set; }
        private TagCloudVisualizer visualizer;
        public ConsoleUI(CloudCreator cloudCreator,IReader reader, ITextParser parser,TagCloudVisualizer visualizer, IImageSaver saver)
        {
            this.cloudCreator = cloudCreator;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
        }
        public void Run()
        {
            var options = new Options();
            CommandLine.Parser.Default.ParseArguments(Args, options);
            var text = reader.Read(options.InputFile);
            var words = parser.Parse(text);
            cloudCreator.Create(words, options.MaxFontSize, options.MinFontSize, options.WordsCount, options.Font);
            var bitmap = visualizer.Vizualize(cloudCreator.RectanglesCloud, Color.AliceBlue);
            saver.Save(bitmap, options.OutputFile);
        }
    }
}
