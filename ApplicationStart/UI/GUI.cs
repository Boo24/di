using GUI;
using TagsCloudVisualization;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.TextHandler;
using TagsCloudVisualization.WordAnalyzer;

namespace ApplicationStart.UI
{
    public class Gui : IUI
    {
        private CloudCreator cloudCreator;
        private IReader reader;
        private IImageSaver saver;
        private ITextParser parser;
        private TagCloudVisualizer visualizer;
        private WordsAnalyzer analyzer;
        public Gui(CloudCreator cloudCreator, WordsAnalyzer analyzer, IReader reader, ITextParser parser, TagCloudVisualizer visualizer, IImageSaver saver)
        {
            this.cloudCreator = cloudCreator;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
            this.analyzer = analyzer;
        }
        public void Run()
        {
            var app = new App();
            app.Run(new TagCloudWindow(cloudCreator, analyzer, reader, parser, visualizer, saver));
        }
    }
}
