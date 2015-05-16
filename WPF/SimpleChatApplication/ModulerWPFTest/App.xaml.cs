using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ModulerWPFTest
{
    using System.ComponentModel.Composition.Hosting;

    using ModulerWPFTest.MEF;
    using ModulerWPFTest.ViewModels;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            var mainWindow = new MainWindow();

            var catalog = new AssemblyCatalog(this.GetType().Assembly);
            var container = new CompositionContainer(catalog);
            var modules = container.GetExportedValues<IModule>();

            mainWindow.DataContext = new MainWindowViewModel(modules.ToList());
            mainWindow.Show();
        }
    }
}
