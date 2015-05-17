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
    using System.Windows.Controls;
    using System.Windows.Input;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.PubSubEvents;
    using Common.Events.EventArgs;
    using Common.Events.Events;
    using System.Collections.ObjectModel;
    using Zhingur.Chat.Module.Models;

    /// <summary>
    /// The chat view model.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChatViewModel : BaseViewModel
    {
        #region Private Member Variables

        private UserControl chatHistoryView;
        private IEventAggregator eventAggregator;
        private Guid senderProfileId;
        private Guid receiverProfileId;
        
        private string chatText;
        private string profileName;
        private string profilePic;
        private string userCurrentStatus;
        ObservableCollection<Chat> chatLines;

        #endregion

        #region Properties

        public ObservableCollection<Chat> ChatLines
        {
            get { return chatLines; }
            set
            {
                chatLines = value;
                this.RaisePropertyChanged("ChatLines");
            }
        }

        public string ProfilePic
        {
            get { return this.profilePic; }
            set
            {
                this.profilePic = value;
                this.RaisePropertyChanged("ProfilePic");
            }
        }

        public string ProfileName
        {
            get { return this.profileName; }
            set
            {
                this.profileName = value;
                this.RaisePropertyChanged("ProfileName");
            }
        }

        public string ChatText
        {
            get { return chatText; }
            set
            {
                this.chatText = value;
                this.RaisePropertyChanged("ChatText");
            }
        }

        public string UserCurrentStatus
        {
            get { return this.userCurrentStatus; }
            set
            {
                this.userCurrentStatus = value;
                this.RaisePropertyChanged("UserCurrentStatus");
            }
        }

        public Guid SenderProfileId
        {
            get { return this.senderProfileId; }
            set
            {
                this.senderProfileId = value;
                this.RaisePropertyChanged("SenderProfileId");
            }
        }

        public Guid ReceiverProfileId
        {
            get { return this.receiverProfileId; }
            set
            {
                this.receiverProfileId = value;
                this.RaisePropertyChanged("ReceiverProfileId");
            }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public ChatViewModel(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("IEventAggregator");
            }

            this.eventAggregator = eventAggregator;
            this.GoBackCommand = new DelegateCommand(this.GoBackCommandHandler);
            this.SendChatCommand = new DelegateCommand(this.SendChatCommandHandler);

            this.ProfileName = "No Name";
            this.UserCurrentStatus = "Offline";

            this.ChatLines = new ObservableCollection<Chat>();
            var chat = new Chat(SenderProfileId, "test")
            {
                IsSender = true
            };
            this.ChatLines.Add(chat);
            chat = new Chat(ReceiverProfileId, "test");
            this.ChatLines.Add(chat);
            chat = new Chat(ReceiverProfileId, "test");
            this.ChatLines.Add(chat);
            chat = new Chat(SenderProfileId, "test")
            {
                IsSender = true
            };
            this.ChatLines.Add(chat);


        }

        #endregion

        #region Commands

        ICommand goBackCommand;

        public ICommand GoBackCommand
        {
            get { return this.goBackCommand; }
            set
            {
                this.goBackCommand = value;
                this.RaisePropertyChanged("GoBackCommand");
            }
        }

        private void GoBackCommandHandler()
        {
            var eventArgs = new OpenMainViewEventArgs();
            this.eventAggregator.GetEvent<OpenMainViewEvent>().Publish(eventArgs); ;
        }

        ICommand sendChatCommand;

        public ICommand SendChatCommand
        {
            get { return this.sendChatCommand; }
            set
            {
                this.sendChatCommand = value;
                this.RaisePropertyChanged("SendChatCommand");
            }
        }

        private void SendChatCommandHandler()
        {
            // Code to send chat to Parse.com server.
            var chat = new Chat(this.SenderProfileId, this.ChatText)
            {
                IsSender = true
            };

            this.ChatLines.Add(chat);
        }

        #endregion

    }
}
