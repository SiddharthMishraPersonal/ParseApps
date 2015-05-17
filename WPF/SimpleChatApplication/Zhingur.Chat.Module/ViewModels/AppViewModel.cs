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
        private ucChatMainView chatMainView;


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
                this.RaisePropertyChanged("ContentControl");
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
        public AppViewModel(IEventAggregator eventAggregator, ucChatMainView chatMainView)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }

            this.eventAggregator = eventAggregator;
            this.chatMainView = chatMainView;
            this.ContentControl = this.chatMainView;

            // Subscription of the event.
            var changeViewEvent = this.eventAggregator.GetEvent<ChangeViewEvent>();
            changeViewEvent.Subscribe(this.SubscribeChangeViewEvent);
        }

        #endregion

        #region Public Methods


        #endregion

        #region Private Methods

        private void SubscribeChangeViewEvent(ChangeViewEventArgs eventArgs)
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
