// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZhingurChatModule.cs" company="">
//  Code belongs to Siddharth Mishra
// </copyright>
// <summary>
//   The Zhingur chat module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Zhingur.Chat.Module.ModuleHelper
{
    using Microsoft.Practices.Prism.MefExtensions.Modularity;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using System.ComponentModel.Composition;
    using Views.UserControls;
    using Zhingur.Chat.Module.Views;

    /// <summary>
    /// The Zhingur chat module.
    /// </summary>
    [Module(ModuleName = "ZhingurChatModule")]
    [ModuleExport(typeof(ZhingurChatModule))]
    public class ZhingurChatModule : IModule
   {
       /// <summary>
       /// The region view registry.
       /// </summary>
       private readonly IRegionViewRegistry regionViewRegistry;

       /// <summary>
       /// Initializes a new instance of the <see cref="ZhingurChatModule"/> class.
       /// </summary>
       /// <param name="regionViewRegistry">
       /// The region view registry.
       /// </param>
       [ImportingConstructor]
       public ZhingurChatModule(IRegionViewRegistry regionViewRegistry)
       {
           if (regionViewRegistry == null)
           {
               throw new ArgumentNullException("regionViewRegistry");
           }

           this.regionViewRegistry = regionViewRegistry;
       }

       /// <summary>
       /// Initializes Chat Module.
       /// </summary>
       public void Initialize()
        {
            this.regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof (ucModuleMainView));
        }
    }
}
