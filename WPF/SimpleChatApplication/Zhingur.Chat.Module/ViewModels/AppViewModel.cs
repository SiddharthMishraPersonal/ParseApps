using System;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Events.EventArgs;
using Common.Events.Events;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Zhingur.Chat.Module.Helper;
using Zhingur.Chat.Module.Views.UserControls;

namespace Zhingur.Chat.Module.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        #region Private Member Variables

        private UserControl contentControl;
        private IEventAggregator eventAggregator;
        private IRegionManager regionManager;
        private ucChatHistoryView view;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the User Control to show on UI.
        /// </summary>
        public UserControl ContentControl
        {
            get
            {
                return this.contentControl;
            }

            set
            {
                this.contentControl = value;
                this.OnPropertyChanged("ContentControl");
            }
        }

        private ICommand _newChatCommand;

        public ICommand NewChatCommand
        {
            get { return _newChatCommand; }
            set { _newChatCommand = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AppViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">
        /// The event Aggregator.
        /// </param>
        /// <param name="regionManager">
        /// The region Manager.
        /// </param>
        /// <param name="view">
        /// The view.
        /// </param>
        public AppViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ucChatHistoryView view)
        {
            this.view = view;

            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }

            if (regionManager == null)
            {
                throw new ArgumentNullException("regionManager");
            }

            this.eventAggregator = eventAggregator;
            this.regionManager = regionManager;
            this.ContentControl = new ucChatHistoryView();
            this.NewChatCommand = new DelegateCommand(this.CreateNewChat);

            var changeViewEvent = this.eventAggregator.GetEvent<ChangeViewUserControlEvent>();
            changeViewEvent.Subscribe(this.SubscribeChangeViewEvent);
        }

        #endregion

        #region Commands

        private void CreateNewChat()
        {
            var changeViewEvent = this.eventAggregator.GetEvent<ChangeViewUserControlEvent>();

            var changeViewEventArgs = new ChangeViewUserControlEventArgs(new ucChatView());
            changeViewEvent.Publish(changeViewEventArgs);
        }

        #endregion

        #region Public Methods


        #endregion

        #region Private Methods

        private void SubscribeChangeViewEvent(ChangeViewUserControlEventArgs eventArgs)
        {
            var userControl = eventArgs.ViewUserControl;

            if (userControl != null)
            {
                if (userControl.GetType() == typeof (ucChatView))
                {
                    regionManager.RegisterViewWithRegion("MainRegion", typeof (ucChatView));
                }
            }
        }

        #endregion


    }
}
