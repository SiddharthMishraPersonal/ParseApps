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

    /// <summary>
    /// The chat view model.
    /// </summary>
    public class ChatViewModel : BaseViewModel
    {
        private ucChatView ucChatViewControl = null;

        public ChatViewModel(ucChatView chatView)
        {
            this.ucChatViewControl = chatView;
        }
    }
}
