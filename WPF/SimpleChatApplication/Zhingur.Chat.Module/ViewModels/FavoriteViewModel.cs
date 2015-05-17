using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhingur.Chat.Module.Helper;

namespace Zhingur.Chat.Module.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FavoriteViewModel : BaseViewModel
    {
        #region Member Variables

        #endregion

        #region Properties

        #endregion

        #region Contstructors

        [ImportingConstructor]
        public FavoriteViewModel(IEventAggregator eventAggregator)
        {

        }

        #endregion

        #region Commands

        #endregion
    }
}
