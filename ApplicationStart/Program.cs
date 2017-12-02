using System;
using ApplicationStart.UI;
using Autofac;
using TagsCloudVisualization.WordAnalyzer;

namespace ApplicationStart
{

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
   
                var container = new ContainerConfig().GetContainer(args);
                var ui = container.Resolve<Gui>();
                ui.Run();


        }
    }
}
