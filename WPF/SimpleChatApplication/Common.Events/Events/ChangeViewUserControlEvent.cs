using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Events.EventArgs;
using Microsoft.Practices.Prism.Events;

namespace Common.Events.Events
{
    /// <summary>
    /// The change view event.
    /// </summary>
    public class ChangeViewUserControlEvent : CompositePresentationEvent<ChangeViewUserControlEventArgs>
    {
    }
}
