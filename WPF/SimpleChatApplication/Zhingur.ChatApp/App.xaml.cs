// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Zhingur.ChatApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Configuration;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    using Parse;
    using Zhingur.ChatApp.BootStrapper;
   

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ParseClient.Initialize("exhummmyHwKOUmv8DoG96hG1QKa3wdGnCRv6L7aU", "ZYilkxNirRFFwgB3WExZjHVMUbmdDcsKiVHmAPFv");
            this.Startup += App_Startup;
        }

        /// <summary>
        /// The startup event handler to authenticate the Application client.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// The event args.
        /// </param>
        private async void App_Startup(object sender, StartupEventArgs e)
        {
            await ParseAnalytics.TrackAppOpenedAsync();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                RootBootStrapper bootstrapper = new RootBootStrapper();
                bootstrapper.Run();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
