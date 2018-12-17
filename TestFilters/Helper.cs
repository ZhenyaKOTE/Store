using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestFilters
{
    class FValueViewItem
    {
        public int? Id { get; set; } //Костиль
        public string Name { get; set; }
    }
    class FNameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FValueViewItem> Childrens { get; set; }
    }
}
