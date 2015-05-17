// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for App.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace SimpleChatApplication
{
    using System.Windows;
    using Parse;
    using BootStrapper;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // Initialize Parse client
            ParseClient.Initialize("exhummmyHwKOUmv8DoG96hG1QKa3wdGnCRv6L7aU", "ZYilkxNirRFFwgB3WExZjHVMUbmdDcsKiVHmAPFv");

            // Lets authenticate application with Parse server.
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

        /// <summary>
        /// The on startup overridden method.
        /// </summary>
        /// <param name="e">
        /// The eventargs.
        /// </param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var bootstrapper = new QuickStartBootStrapper();
            bootstrapper.Run();
        }
    }
}
