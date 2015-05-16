using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using SimpleChatApplication.Views;
using Zhingur.Chat.Module.ViewModels;
using Zhingur.Chat.Module.Views.UserControls;

namespace SimpleChatApplication.Unity
{
   public class UnityRegister
    {
       public static void Register()
       {
           IUnityContainer container = new UnityContainer();
           container.RegisterType<ucChatHistoryView>();
           container.RegisterType<ucChatView>();
           container.RegisterType<AppViewModel>();
       }
    }
}
