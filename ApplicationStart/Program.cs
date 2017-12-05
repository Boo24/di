using System;
using ApplicationStart.UI;
using Autofac;

namespace ApplicationStart
{

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            var container = new ContainerConfig().GetContainer(args);
            if (args.Length == 0)
            {
                var ui = container.Resolve<Gui>();
                ui.Run();
            }
            else
            {
                var ui = container.Resolve<ConsoleUi>();
                ui.Run();
            }



        }
    }
}
