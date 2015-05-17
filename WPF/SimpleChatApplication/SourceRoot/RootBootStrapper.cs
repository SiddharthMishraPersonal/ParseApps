using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;

namespace SourceRoot
{
    public class RootBootStrapper : MefBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<MainWindow>();

        }

        protected override void InitializeShell()
        {

            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;

            Application.Current.MainWindow.Show();

        }

        protected override void ConfigureAggregateCatalog()
        {

            base.ConfigureAggregateCatalog();

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(RootBootStrapper).Assembly));

        }

    }

}
