using System;
using System.Collections.Generic;
using System.Text;

namespace WK.TAA.CCHAxcess.CodeGames.Models
{
    public class UserInfo
    {
         public string AccountId { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string PushRegistrationId { get; set; }

        public string PushTag { get
            {
                return AccountId + "_" + UserId;
            }
        }
    }
}
