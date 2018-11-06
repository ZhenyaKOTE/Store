using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class CustomPrincipalSerializeModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string[] Roles { get; set; }
    }
}