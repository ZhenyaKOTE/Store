using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class PaginationViewModel
    {
        public IQueryable<ProductDTO> Products { get; set; }
        public int MaxPages { get; set; }
    }

    public class PaginationParamsModel
    {
        public int Page { get; set; }
        public int CountViewProducts { get; set; }
        public int CategoryId { get; set; }
    }
}
