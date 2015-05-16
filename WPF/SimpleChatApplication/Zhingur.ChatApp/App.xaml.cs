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

    using Zhingur.Contract.InheritedContract;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        [Import]
        public ViewFactory ViewFactory { get; set; }

        public App()
        {
            ParseClient.Initialize("exhummmyHwKOUmv8DoG96hG1QKa3wdGnCRv6L7aU", "ZYilkxNirRFFwgB3WExZjHVMUbmdDcsKiVHmAPFv");
            this.Startup += App_Startup;
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var d =  Directory.GetCurrentDirectory();
                var catalog = new DirectoryCatalog(@".\");
                var container = new CompositionContainer(catalog);
                container.ComposeParts(this);
            }
            catch (Exception exception)
            {
                throw;
            }
        }





















        ///// <summary>
        ///// To import classes.
        ///// </summary>
        //[ImportMany(typeof(IMain))]
        //private IEnumerable<Lazy<IMain>> components;

        //private CompositionContainer mefContainer;

        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    try
        //    {
        //        var catalog = new DirectoryCatalog(@"C:\Users\WKTF64\Documents\GitHub\ParseApps\WPF\Artifacts");
        //        var container = new CompositionContainer(catalog);
        //        container.ComposeParts(this);
        //    }
        //    catch (Exception exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
