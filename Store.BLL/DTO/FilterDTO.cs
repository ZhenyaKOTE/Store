using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class FilterDTO
    {
        //[ForeignKey("FilterNameOf"), Key, Column(Order = 0)]
        public int FilterNameId { get; set; }
        public virtual FilterNameDTO FilterNameOf { get; set; }

        //[ForeignKey("FilterValueOf"), Key, Column(Order = 1)]
        public int FilterValueId { get; set; }
        public virtual FilterValueDTO FilterValueOf { get; set; }

        //[ForeignKey("ProductOf"), Key, Column(Order = 2)]
        public int ProductId { get; set; }
        public virtual ProductDTO ProductOf { get; set; }
    }
}
