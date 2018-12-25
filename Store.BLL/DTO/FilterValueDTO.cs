
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class FilterValueDTO
    {  
        public int Id { get; set; }
        [Required, StringLength(maximumLength: 250)]
        public string Name { get; set; }
        public virtual ICollection<FilterDTO> Filters { get; set; }
        public virtual ICollection<FilterNameGroupDTO> FilterNameGroups { get; set; }
    }
}
