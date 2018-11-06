
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.Models;
using Store.Util;
using System;

using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Store.Util
{
    public class CustomPrincipal : ICustomPrincipal
    {
        private readonly string[] _roles;

        public CustomPrincipal(IIdentity identity, string[] roles)
        {
            Identity = identity;
            _roles = roles;
        }

        protected string[] Roles //Приходить серіалізований обжект юзера
        {
            get { return _roles; }
        }

        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            foreach (string rol in Roles)
            {
                //Debug.Write(Roles[0]+"\n\n\n\n\n");
                if (string.Compare(rol, role, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        //public CustomPrincipal(string email)
        //{
        //    this.Identity = new GenericIdentity(email);
        //    HttpContext.Current.GetOwinContext();
        //}

        public string Name { get; set; }
        public string Email { get; set; }
    }
}