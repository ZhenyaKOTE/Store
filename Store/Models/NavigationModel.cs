using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class NavigationModel
    {
        public int SelectedPage { get; set; }
        public int MaxPages { get; set; }
        public IEnumerable<string> ActionUrl { get; set; }
    }
}