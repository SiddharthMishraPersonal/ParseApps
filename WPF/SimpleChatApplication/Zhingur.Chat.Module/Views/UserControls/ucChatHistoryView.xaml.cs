// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucChatHistoryView.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for ucChatHistoryView.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Zhingur.Chat.Module.Views.UserControls
{
    using System.ComponentModel.Composition;
    using System.Windows.Controls;
    using Zhingur.Chat.Module.ViewModels;

    /// <summary>
    /// Interaction logic for ucChatHistoryView.xaml
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ucChatHistoryView : UserControl
    {
        public ucChatHistoryView()
        {
            InitializeComponent();
        }

        [Import]
        public ChatHistoryViewModel AppViewModel
        {
            get
            {
                return this.DataContext as ChatHistoryViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
