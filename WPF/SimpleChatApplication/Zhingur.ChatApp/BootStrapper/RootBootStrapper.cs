using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.MefExtensions;
using Zhingur.ChatApp.View;
using Zhingur.Chat.Module.ModuleHelper;

namespace Zhingur.ChatApp.BootStrapper
{
    public class RootBootStrapper : MefBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<ShellAppView>();
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
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ZhingurChatModule).Assembly));
        }
    }
}
