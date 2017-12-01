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
   
                var container = new ContainerConfig().GetContainer();
                var ui = container.Resolve<Gui>();
                //для консольной версии надо пробросить аргументы:((    
                //ui.args = args; 
                ui.Run();


        }
    }
}
