using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class InfoForPageProductModel
    {
        public IList<GeneralProductModel> ProductModels { get; set; }
        public NavigationModel NavigationModel_ { get; set; }

        public string ThisCategory { get; set; }
    }
}