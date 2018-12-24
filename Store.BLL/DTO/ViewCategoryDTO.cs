using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    class ViewCategoryDTO
    {
        public PreViewCategoryDTO pViewCategoryDTO { get; set; }
        public virtual IEnumerable<ProductDTO> ProductsDTO { get; set; }
    }
}
