using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Models
{
    public class GeneralProductModel
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public string TextUrl { get; set; }
        public FotoName PhotoPath { get; set; }
        public string UrlToBuy { get; set; }
    }
}