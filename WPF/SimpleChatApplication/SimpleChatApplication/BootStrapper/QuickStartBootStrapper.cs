using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.ServiceLocation;
using SimpleChatApplication.Views;
using Zhingur.Chat.Module;
using Zhingur.Chat.Module.ModuleHelper;

namespace SimpleChatApplication.BootStrapper
{
    public class QuickStartBootStrapper : UnityBootstrapper
    {
        /// <summary>
        /// The create shell or main window of application.
        /// </summary>
        /// <returns>
        /// The Shell of the Application.
        /// </returns>
        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<ShellAppView>();
        }

        /// <summary>
        /// The initialize shell.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(ZhingurChatModule));
        }
    }
}
