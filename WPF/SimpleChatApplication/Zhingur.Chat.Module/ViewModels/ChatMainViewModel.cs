using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using Zhingur.Chat.Module.Helper;
using Zhingur.Chat.Module.Views.UserControls;

namespace Zhingur.Chat.Module.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ChatMainViewModel : BaseViewModel
    {
        #region Member Variables

        private ucContactsView contactsView;
        private ucFavoriteView favoriteView;
        private ucSettingsView settingsView;
        private ucChatHistoryView chatHistoryView;
        private IEventAggregator eventAggregator;

        private UserControl contentControl;

        #endregion

        #region Properties

        public UserControl ContentControl
        {
            get
            {
                return this.contentControl;
            }
            set
            {
                this.contentControl = value;
                this.RaisePropertyChanged("ContentControl");

            }
        }

        #endregion

        #region Constructors

        [ImportingConstructor]
        public ChatMainViewModel(IEventAggregator eventAggregator, ucContactsView contactsView, ucFavoriteView favoriteView, ucSettingsView settingsView, ucChatHistoryView chatHistoryView)
        {

            if (eventAggregator == null)
            {
                throw new ArgumentNullException("IEventAggregator");
            }

            if (contactsView == null)
            {
                throw new ArgumentNullException("ucContactsView");
            }

            if (favoriteView == null)
            {
                throw new ArgumentNullException("ucFavoriteView");
            }

            if (settingsView == null)
            {
                throw new ArgumentNullException("ucSettingsView");
            }

            if (chatHistoryView == null)
            {
                throw new ArgumentNullException("ucChatHistoryView");
            }

            this.eventAggregator = eventAggregator;

            this.contactsView = contactsView;
            this.favoriteView = favoriteView;
            this.settingsView = settingsView;
            this.chatHistoryView = chatHistoryView;

            this.SettingCommand = new DelegateCommand(this.SettingCommandHandler);
            this.FavoriteCommand = new DelegateCommand(this.FavoriteCommandHandler);
            this.ContactsCommand = new DelegateCommand(this.ContactsCommandHandler);
            this.ChatHistoryCommand = new DelegateCommand(this.ChatHistoryCommandHandler);

            this.ContentControl = this.chatHistoryView;
        }

        #endregion

        #region Commands

        ICommand favoriteCommand;

        public ICommand FavoriteCommand
        {
            get { return this.favoriteCommand; }
            set
            {
                this.favoriteCommand = value;
                this.RaisePropertyChanged("FavoriteCommand");
            }
        }

        private void FavoriteCommandHandler()
        {
            this.ContentControl = this.favoriteView;
        }


        ICommand contactsCommand;

        public ICommand ContactsCommand
        {
            get { return this.contactsCommand; }
            set
            {
                this.contactsCommand = value;
                this.RaisePropertyChanged("ContactsCommand");
            }
        }

        private void ContactsCommandHandler()
        {
            this.ContentControl = this.contactsView;
        }

        ICommand settingCommand;

        public ICommand SettingCommand
        {
            get { return this.settingCommand; }
            set
            {
                this.settingCommand = value;
                this.RaisePropertyChanged("SettingCommand");
            }
        }

        private void SettingCommandHandler()
        {
            this.ContentControl = this.settingsView;
        }

        ICommand chatHistoryCommand;

        public ICommand ChatHistoryCommand
        {
            get { return this.chatHistoryCommand; }
            set
            {
                this.chatHistoryCommand = value;
                this.RaisePropertyChanged("ChatHistoryCommand");
            }
        }

        private void ChatHistoryCommandHandler()
        {
            this.ContentControl = this.chatHistoryView;
        }

        #endregion
    }
}
