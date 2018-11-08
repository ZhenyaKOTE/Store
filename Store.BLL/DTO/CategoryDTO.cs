using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class CategoryDTO
    {
        public CategoryDTO() => ProductsDTO = new List<ProductDTO>();
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual IList<ProductDTO> ProductsDTO { get; set; }
    }
}
