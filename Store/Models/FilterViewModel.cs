using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.Models
{
    public class FilterViewModel
    {
        public bool IsChecked { get; set; }
        public string Value { get; set; }
        public int Id { get; set; }
    }
}