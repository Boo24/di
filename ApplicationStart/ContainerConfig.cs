using ApplicationStart.UI;
using Autofac;
using TagsCloudVisualization;
using TagsCloudVisualization.Geometry;
using TagsCloudVisualization.Geometry.Layouter;
using TagsCloudVisualization.TextHandler;
using TagsCloudVisualization.WordAnalyzer;

namespace ApplicationStart
{
    public class ContainerConfig
    {
        public IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TxtReader>().As<IReader>();
            builder.RegisterType<TxtParser>().As<ITextParser>();
            builder.RegisterType<CloudCreater>().AsSelf();
            builder.RegisterType<WordsAnalyzer>().AsSelf();
            builder.RegisterType<DefaultBlackList>().As<IBlackList>();
            builder.RegisterType<RectanglesCloud>().As<IRectanglesCloud>();
            builder.RegisterType<RandomColorSelector>().As<IFontColorSelector>();
            builder.RegisterType<CircularLayouter>().As<ILayouter>();
            builder.RegisterType<ArchimedeanSpiral>().As<ISpiral>();
            builder.RegisterType<DefaultImageSaver>().As<IImageSaver>();
            builder.RegisterType<Gui>().AsSelf();
            builder.RegisterType<TagCloudVizualizer>().AsSelf();
            return builder.Build();
        }
    }
}
