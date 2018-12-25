using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class FilterNameGroupDTO
    {
       
        public int FilterNameId { get; set; }
        public virtual FilterNameDTO FilterNameOf { get; set; }

    
        public int FilterValueId { get; set; }
        public virtual FilterValueDTO FilterValueOf { get; set; }
    }
}
