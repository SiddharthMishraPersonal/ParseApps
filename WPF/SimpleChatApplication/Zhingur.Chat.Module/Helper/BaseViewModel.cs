// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseViewModel.cs" company="Samyak Inc.">
// This application is copyright to Siddharth Mishra and Samyak Mishra.  
// </copyright>
// <summary>
//   The base view model.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Zhingur.Chat.Module.Helper
{
    using System.ComponentModel;

    /// <summary>
    /// The base view model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The property changed event handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// The on property changed method which will invoke Property Changed event.
        /// </summary>
        /// <param name="property">
        /// The property name.
        /// </param>
        public void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
