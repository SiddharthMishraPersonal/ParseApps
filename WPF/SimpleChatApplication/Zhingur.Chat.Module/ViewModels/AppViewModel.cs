using System;
using System.Windows.Controls;
using System.Windows.Input;
using Common.Events.EventArgs;
using Common.Events.Events;
using Microsoft.Practices.Prism.Regions;
using Zhingur.Chat.Module.Helper;
using Zhingur.Chat.Module.Views.UserControls;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;

namespace Zhingur.Chat.Module.ViewModels
{
    using Microsoft.Practices.Prism.PubSubEvents;

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AppViewModel : BaseViewModel
    {
        #region Private Member Variables

        private UserControl contentControl;
        private IEventAggregator eventAggregator;
        private IRegionManager regionManager;
        private ucChatHistoryView chatHistoryView;


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
        [ImportingConstructor]
        public AppViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ucChatHistoryView chatHistoryView)
        {
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
            this.chatHistoryView = chatHistoryView;
            this.ContentControl = this.chatHistoryView;

            // Subscription of the event.
            var changeViewEvent = this.eventAggregator.GetEvent<ChangeViewUserControlEvent>();
            changeViewEvent.Subscribe(this.SubscribeChangeViewEvent);
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
                this.ContentControl = userControl;
            }
        }

        #endregion

    }
}
