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
    public partial class ucChatHistoryView : UserControl
    {
        [ImportingConstructor]
        public ucChatHistoryView()
        {
            InitializeComponent();
        }

        [Import]
        public AppViewModel AppViewModel
        {
            get
            {
                return this.DataContext as AppViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
