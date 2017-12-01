using GUI;
using TagsCloudVisualization;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.TextHandler;

namespace ApplicationStart.UI
{
    public class Gui : IUI
    {
        private CloudCreater cloudCreater;
        private IReader reader;
        private IImageSaver saver;
        private ITextParser parser;
        private TagCloudVizualizer visualizer;
        public Gui(CloudCreater cloudCreater, IReader reader, ITextParser parser, TagCloudVizualizer visualizer, IImageSaver saver)
        {
            this.cloudCreater = cloudCreater;
            this.reader = reader;
            this.saver = saver;
            this.visualizer = visualizer;
            this.parser = parser;
        }
        public void Run()
        {
            var app = new App();
            app.Run(new TagCloudWindow(cloudCreater, reader, parser, visualizer, saver));
        }
    }
}
