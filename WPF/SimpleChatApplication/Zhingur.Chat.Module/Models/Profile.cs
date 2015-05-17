using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhingur.Chat.Module.Models
{
    public class Profile
    {
        #region Member Variables

        private Guid profileId;

        #endregion

        #region Properties

        public Guid ProfileId
        {
            get { return profileId; }
            set { profileId = value; }
        }

        #endregion

        #region Constructors

        public Profile()
        {
            this.ProfileId = Guid.NewGuid();
        }

        #endregion

        #region Methods

        #endregion
    }
}
