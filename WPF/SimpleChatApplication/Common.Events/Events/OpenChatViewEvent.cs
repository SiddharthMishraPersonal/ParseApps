using Common.Events.EventArgs;
using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Events.Events
{
    public class OpenChatViewEvent : PubSubEvent<OpenChatViewEventArgs>
    {
    }
}
