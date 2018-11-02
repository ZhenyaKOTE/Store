using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Store.Helpers
{
    public static class ActionHelper
    {
        public static async Task<MvcHtmlString> ActionAsync(string ActionName, string Controller)
        {
          
            //TagBuilder result = new TagBuilder("h6");
            //result.SetInnerText("Hello,World!");
            return new MvcHtmlString("Hello");
        }
    }
}