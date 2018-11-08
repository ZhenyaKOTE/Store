using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities.StoreEntities
{
    public class Product
    {
        public Product() => Characteristics = new List<Characteristic>();      
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public virtual IList<Characteristic> Characteristics { get; set; }
    }
}
