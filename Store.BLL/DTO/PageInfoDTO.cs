using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class PageInfoDTO
    {
        public PageInfoDTO()
        {
            Products = new List<ProductDTO>();
        }
        public IList<ProductDTO> Products { get; set; }
        public int MaxPages { get; set; }
    }
}
