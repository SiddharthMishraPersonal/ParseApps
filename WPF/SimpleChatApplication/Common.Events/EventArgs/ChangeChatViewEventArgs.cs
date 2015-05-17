using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Common.Events.EventArgs
{
  public  class ChangeChatViewEventArgs
    {
      /// <summary>
        /// The user control.
        /// </summary>
        private readonly UserControl viewUserControl;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeViewEventArgs"/> class.
        /// </summary>
        /// <param name="viewUserControl">
        /// The view user control.
        /// </param>
        public ChangeChatViewEventArgs(UserControl viewUserControl)
        {
            this.viewUserControl = viewUserControl;
        }

        /// <summary>
        /// Gets the user control.
        /// </summary>
        public UserControl ViewUserControl
        {
            get { return this.viewUserControl; }
        }
    }
}
