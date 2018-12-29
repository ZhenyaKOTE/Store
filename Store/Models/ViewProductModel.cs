using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Models
{
    public class ViewProductModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Text{ get; set; }
        public string PhotoPath { get; set; }
        public string UrlToBuy { get; set; }
    }
}