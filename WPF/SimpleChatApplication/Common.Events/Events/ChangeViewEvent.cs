using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Common.Events.EventArgs;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Common.Events.Events
{
    /// <summary>
    /// The change view event.
    /// </summary>
    public class ChangeViewEvent : PubSubEvent<ChangeViewEventArgs>
    {
    }
}
