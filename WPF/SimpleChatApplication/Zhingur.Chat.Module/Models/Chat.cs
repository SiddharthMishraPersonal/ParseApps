using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhingur.Chat.Module.Models
{
    public class Chat
    {
        #region Member Variables

        private Guid profileId;
        private string chatText;
        private bool isSender;

        #endregion

        #region Properties

        public Guid ProfileId
        {
            get { return profileId; }
            set { profileId = value; }
        }

        public string ChatText
        {
            get { return chatText; }
            set { chatText = value; }
        }

        public bool IsSender
        {
            get { return isSender; }
            set { isSender = value; }
        }

        #endregion

        #region Constructors

        public Chat(Guid profileId, string chatText)
        {
            this.ProfileId = profileId;
            this.ChatText = chatText;
        }

        #endregion

        #region Methods

        #endregion
    }
}
