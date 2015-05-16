// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ucChatHistoryView.xaml.cs" company="">
//   
// </copyright>
// <summary>
//   Interaction logic for ucChatHistoryView.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Zhingur.UserControls.UserControls
{
    using System.ComponentModel.Composition;
    using System.Windows.Controls;
    using Zhingur.Contract.InheritedContract;

    /// <summary>
    /// Interaction logic for ucChatHistoryView.xaml
    /// </summary>
    [Export]
    public partial class ucChatHistoryView : UserControl, IMain
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ucChatHistoryView"/> class.
        /// </summary>
        public ucChatHistoryView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets the name of user control.
        /// </summary>
        public string NameOfUserControl
        {
            get
            {
                return "ucChatHistoryView";
            }
        }
    }
}
