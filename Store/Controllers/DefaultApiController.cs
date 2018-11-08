
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Store.BLL.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Store.Controllers
{

    public class DefaultApiController : System.Web.Http.ApiController
    {
        private IStoreService StoreService { //StoreService +
            get
            { 
                return HttpContext.Current.GetOwinContext().Get<IStoreService>();
            }
        }


        [System.Web.Http.HttpPost]
        [OutputCache(Duration = 3600, Location = OutputCacheLocation.ServerAndClient)]
        public async Task<string> GetCategory()
        {
            //if(StoreService == null)
            // Debug.Write("Alaxakbar blat null \n\n\n\n\n\n\n");
            //else
            //    Debug.Write("Norm \n\n\n\n\n\n\n");

            var list = await StoreService.GetCategoryNames();
            //List<string> list = new List<string>();
            //list.Add("Шини");
            //list.Add("Диски");
            return JsonConvert.SerializeObject(list);
        }

    }
}
