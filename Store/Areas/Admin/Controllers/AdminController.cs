using System.Web.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Util;
using System.Diagnostics;

namespace Store.Areas.Admin.Controllers
{
    
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        //[Authorize(Roles = "User")]
        public ActionResult Index()
        {
            Debug.Write((User as CustomPrincipal).IsInRole("User"));
            return View("Index");
        }
    }
}