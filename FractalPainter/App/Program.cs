using System;
using System.Windows.Forms;
using FractalPainting.Infrastructure;
using Ninject;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
        //    try
        //    {
        //        var conteiner = new StandartKernel();
        //        conteiner.Bind(x => x.FromThisAssembly()
        //            .SelectAllClasses()
        //            .InheritedFrom(typeof(IUiAction))
        //            .BindAllInterfaces()
        //            .Configure(y => y.InSingletonScope()
        //            ));
               
        //        Application.EnableVisualStyles();
        //        Application.SetCompatibleTextRenderingDefault(false);
        //        Application.Run(new MainForm());
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        }
    }
}