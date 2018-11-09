
using Newtonsoft.Json;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Store.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult SecretPage()
        {
            return View();
        }

        [HttpPost]
        public string GetFilters()
        {
            List<CheckBoxModel> list = new List<CheckBoxModel>();
            CheckBoxModel model = new CheckBoxModel();
            model.Value = "Test1";
            model.IsChecked = false;
            CheckBoxModel model1 = new CheckBoxModel();
            model1.Value = "Test2";
            model1.IsChecked = false;
            CheckBoxModel model2 = new CheckBoxModel();
            model2.Value = "Test3";
            model2.IsChecked = false;
            list.Add(model);
            list.Add(model1);
            list.Add(model2);
            return JsonConvert.SerializeObject(list);
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