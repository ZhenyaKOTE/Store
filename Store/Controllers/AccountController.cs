
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Store.BLL.DTO;
using Store.BLL.Infrastructure;
using Store.BLL.Interfaces;
using Store.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace Store.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            //Debug.Write("fdsfsdfsdfs");
            if (ModelState.IsValid)
            {
                //var user = userRepository.Users.Where(u => u.Email == viewModel.Email).First();
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };

                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                var a = claim.Claims.ToList();
                string temp = "";

                foreach (Claim tempClaim in a)
                {
                    //tempClaim.ValueType
                    temp += tempClaim.Value + "\n\n";
                }

                Debug.Write(temp + "\n\n\n\n\n\n");
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    string _TempUserName = "";

                    foreach (var ClaimStr in claim.Claims)
                    {
                        if (ClaimStr.Type == "UserName")
                        {
                            _TempUserName = ClaimStr.Value;
                            break;
                        }
                    }

                    AuthenticationManager.SignOut();

                    //var a = new AuthenticationProperties {  }

                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);


                    //CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel
                    //{
                    //    Name = _TempUserName,
                    //    Email = userDto.Email,
                    //    Roles = await UserService.GetRoles(claim.GetUserId())
                    //};

                    //JavaScriptSerializer serializer = new JavaScriptSerializer();

                    //string userData = serializer.Serialize(serializeModel);

                    //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    //         1,
                    //         model.Email,
                    //         DateTime.Now,
                    //         DateTime.Now.AddDays(14),
                    //         false,
                    //         userData);

                    //string encTicket = FormsAuthentication.Encrypt(authTicket);
                    //HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    //Response.Cookies.Add(faCookie);

                    return RedirectToAction("Index", "Home");
                }

            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {

            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    Name = model.Name
                };
                userDto.Roles.Add("User");
                userDto.Roles.Add("Admin");


                OperationDetails operationDetails = await UserService.CreateAsync(userDto);
                //Debug.Write(operationDetails.Message);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        
        public ActionResult Logout()
        {
            Debug.Write(AuthenticationManager.User.IsInRole("User"));
            Debug.Write("Pause");

            AuthenticationManager.SignOut();

            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                var cookieToDelete = new HttpCookie(FormsAuthentication.FormsCookieName);
                cookieToDelete.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookieToDelete);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
