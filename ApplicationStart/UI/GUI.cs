using GUI;
using TagsCloudVisualization;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.TextHandler;

namespace ApplicationStart.UI
{
    public class Gui : IUI
    {
        private CloudCreator cloudCreator;
        private IReader reader;
        private IImageSaver saver;
        private ITextParser parser;
        private TagCloudVisualizer visualizer;
        public Gui(CloudCreator cloudCreator, IReader reader, ITextParser parser, TagCloudVisualizer visualizer, IImageSaver saver)
        {
            this.cloudCreator = cloudCreator;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
        }
        public void Run()
        {
            var app = new App();
            app.Run(new TagCloudWindow(cloudCreator, reader, parser, visualizer, saver));
        }
    }
}
