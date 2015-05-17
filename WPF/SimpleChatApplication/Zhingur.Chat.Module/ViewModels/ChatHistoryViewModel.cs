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

        private IEventAggregator eventAggregator;

        #endregion


        [ImportingConstructor]
        public ChatHistoryViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.NewChatCommand = new DelegateCommand(this.CreateNewChat);
        }


        #region Commands

        private ICommand newChatCommand;

        public ICommand NewChatCommand
        {
            get { return newChatCommand; }
            set { newChatCommand = value; }
        }

        private void CreateNewChat()
        {
            try
            {
                var openChatViewEventArgs = new OpenChatViewEventArgs();
                this.eventAggregator.GetEvent<OpenChatViewEvent>().Publish(openChatViewEventArgs);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
