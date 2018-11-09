using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Store.BLL.Interfaces;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Store.Controllers
{
    public class StoreController : Controller
    {
        private IStoreService StoreService
        { 
            get
            {   
                return HttpContext.GetOwinContext().Get<IStoreService>();
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult SecretPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetFilters()
        {
            return await Task.Run(() =>
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
            });

        }

        [HttpPost]
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.ServerAndClient)]
        public async Task<string> GetCategory()
        {
            var list = await StoreService.GetCategoryNames();
            return JsonConvert.SerializeObject(list);
        }
    }
}