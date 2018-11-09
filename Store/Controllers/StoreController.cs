using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Store.BLL.Interfaces;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        [HttpGet]
        public ActionResult SecretPage()
        {
            List<CheckBoxModel> list = new List<CheckBoxModel>();

            CheckBoxModel.RefreshId();

            CheckBoxModel model = new CheckBoxModel();
            model.Value = "Test1";
            model.IsChecked = false;
            CheckBoxModel model1 = new CheckBoxModel();
            model1.Value = "Test2";
            model1.IsChecked = false;
            CheckBoxModel model2 = new CheckBoxModel();
            model2.Value = "Test3";
            model2.IsChecked = true;

            list.Add(model);
            list.Add(model1);
            list.Add(model2);

            ViewBag.Filters = list;
            return View();
        }

        [HttpGet]
        public async Task<PartialViewResult> GetFilters(IEnumerable<Store.Models.CheckBoxModel> Message)
        {
            if (Message == null)
                Debug.Write("null!!!\n\n\n\n\n\n");
            else
            {
                Debug.Write("NotNull\n\n\n\n\n\n");
                Debug.Write((Message as List<CheckBoxModel>).Count); //Приходить пустий список
            }
            // ViewBag.ToReloadFilters = false;
            return await Task.Run(() =>
            {
                
                List<CheckBoxModel> list = new List<CheckBoxModel>();

                CheckBoxModel.RefreshId();

                CheckBoxModel model = new CheckBoxModel();
                model.Value = "Test1";
                model.IsChecked = false;
                CheckBoxModel model1 = new CheckBoxModel();
                model1.Value = "Test2";
                model1.IsChecked = false;
                CheckBoxModel model2 = new CheckBoxModel();
                model2.Value = "Test3";
                model2.IsChecked = true;

                list.Add(model);
                list.Add(model1);
                list.Add(model2);

                return PartialView("_FilterView", list);
               // return JsonConvert.SerializeObject(list);
            });

        }


        [HttpPost]
        public async Task SendFilters(IEnumerable<CheckBoxModel> checks)
        {
            await Task.Run(() =>
            {
                List<CheckBoxModel> list = checks as List<CheckBoxModel>;
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