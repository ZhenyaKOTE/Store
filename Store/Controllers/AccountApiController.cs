using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json;
using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.Models;
using Store.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace Store.Controllers
{
    public class AccountApiController : ApiController
    {
        private IUserService UserService
        {
            get
            {
                return Request.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        // POST api/Account/Register
        [AllowAnonymous]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                // return Ok(ModelState);
            }
            UserDTO user = new UserDTO { Email = userModel.Email, Password = userModel.Password, Name = userModel.Name };

            OperationDetails result = await UserService.CreateAsync(user);

            if (result == null)
                return InternalServerError();

            if (!result.Succedeed)
            {
                ModelState.AddModelError(result.Property, result.Message);
                return BadRequest(ModelState);
            }
            
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                UserService.Dispose();
            }

            base.Dispose(disposing);
        }


    }

    public static class Logger
    {
        public static void Write(string log)
        {
            using (StreamWriter str = new StreamWriter(@"D:\Desktop\SiteLogs.txt", false, System.Text.Encoding.Default))
            {
                str.Write(log+"\n\n");
            }
        }
    }
}
