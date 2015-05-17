using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Zhingur.Chat.Module.Views.UserControls;

namespace Zhingur.Chat.Module.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChatHistoryViewModel
    {
        #region Private Member Variables

        private ucChatView chatView;

        #endregion


        [ImportingConstructor]
        public ChatHistoryViewModel(ucChatView chatView)
        {
            this.NewChatCommand = new DelegateCommand(this.CreateNewChat);
        }


        #region Commands

        private ICommand _newChatCommand;

        public ICommand NewChatCommand
        {
            get { return _newChatCommand; }
            set { _newChatCommand = value; }
        }

        private void CreateNewChat()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
