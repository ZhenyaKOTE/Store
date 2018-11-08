using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class ProductDTO
    {
        public ProductDTO() => Characteristics = new List<CharacteristicDTO>();      
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public virtual IList<CharacteristicDTO> Characteristics { get; set; }
    }
}
