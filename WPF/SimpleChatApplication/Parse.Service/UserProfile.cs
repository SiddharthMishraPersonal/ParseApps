using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parse.Service
{
    public class UserProfile : IChatParseObject
    {
        public string UserName { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string ObjectId { get; set; }
        public string CellPhoneNumber { get; set; }

        public UserProfile(string userName, string loginId, string password, string cellPhoneNumber)
        {
            this.UserName = userName;
            this.LoginId = loginId;
            this.Password = password;
            this.ObjectId = Guid.NewGuid().ToString();
        }

        public async Task<string> SaveObject()
        {
            try
            {
                ParseObject newUser = new ParseObject("UserProfile");
                newUser["ObjectId"] = this.ObjectId;
                newUser["UserName"] = this.UserName;
                newUser["LoginId"] = this.LoginId;
                newUser["Password"] = this.Password;
                newUser["CellPhoneNumber"] = this.CellPhoneNumber;
                await newUser.SaveAsync();
            }
            catch (Exception exception)
            {
                
                throw;
            }
            return ObjectId;
        }
    }
}
