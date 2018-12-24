using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.DAL.Entities.StoreEntitiesWithFilters
{
    [Table("tblCategories")]
    public class Category
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        //[ForeignKey("Childrens"), Column(Order = 1)]
        //public int? ParentId { get; set; }
        [Required, StringLength(maximumLength: 255)]
        public string Name { get; set; }
        public IEnumerable<Category> Childrens { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
    }
}
