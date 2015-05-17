using Common.Events.EventArgs;
using Common.Events.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
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
        private IEventAggregator eventAggregator;

        #endregion


        [ImportingConstructor]
        public ChatHistoryViewModel(IEventAggregator eventAggregator, ucChatView chatView)
        {
            this.eventAggregator = eventAggregator;
            this.chatView = chatView;
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
                var changeViewUserControlEventArgs = new ChangeViewUserControlEventArgs(this.chatView);
                this.eventAggregator.GetEvent<ChangeViewUserControlEvent>().Publish(changeViewUserControlEventArgs);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
