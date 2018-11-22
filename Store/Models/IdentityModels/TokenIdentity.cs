using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models.IdentityModels
{
    public class TokenIdentity
    {
        public int UserID { get; set; }
        public string AuthToken { get; set; }
       // public  SocialUser { get; set; }
    }
}