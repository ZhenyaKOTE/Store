using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Store.Controllers
{
   
    public class DefaultApiController : ApiController
    {
        [System.Web.Http.HttpPost]
        //[OutputCache(Duration = 3600, Location = OutputCacheLocation.Server)]
        public string GetCategory()
        {
            List<string> list = new List<string>();
            list.Add("Шини");
            list.Add("Диски");
            //Thread.Sleep(5000);
            return JsonConvert.SerializeObject(list.ToArray());
            //return new JavaScriptSerializer().Serialize(list.ToArray());
        }

        public string Get()
        {
            return "hELLO";
        }
    }
}
