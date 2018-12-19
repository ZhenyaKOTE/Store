using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities.StoreEntitiesWithFilters
{
    [Table("tblProducts")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreate { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Filter> Filters { get; set; }
        public string Description { get; set; }
    }
}
