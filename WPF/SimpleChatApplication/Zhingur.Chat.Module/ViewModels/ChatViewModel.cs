// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChatViewModel.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the ChatViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Zhingur.Chat.Module.Views.UserControls;

namespace Zhingur.Chat.Module.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Helper;
    using System.ComponentModel.Composition;

    /// <summary>
    /// The chat view model.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChatViewModel : BaseViewModel
    {
        #region Private Member Variables

        private string chatText;

        public string ChatText
        {
            get { return chatText; }
            set
            {
                chatText = value;
                OnPropertyChanged("ChatText");
            }
        }

        #endregion

        [ImportingConstructor]
        public ChatViewModel()
        {

        }
    }
}
