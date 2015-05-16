// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ZhingurChatModule.cs" company="">
//  Code belongs to Siddharth Mishra
// </copyright>
// <summary>
//   The Zhingur chat module.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace Zhingur.Chat.Module
{
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Views.UserControls;

    /// <summary>
    /// The Zhingur chat module.
    /// </summary>
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
            this.regionViewRegistry.RegisterViewWithRegion("MainRegion", typeof (ucChatHistoryView));
        }
    }
}
