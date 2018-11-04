
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult SomeEvent()
        {
            return View();
        }
        public HomeController(/*IRepository BusinessLogicLayer*/)
        {

        }

        [HttpPost]
        public string GetCategory()
        {
            List<string> list = new List<string>();
            list.Add("Шини");
            list.Add("Диски");
            return new JavaScriptSerializer().Serialize(list.ToArray());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}