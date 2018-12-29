using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class NavigationModel
    {
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<ViewProductModel> Products { get; set; }
      
    }
}