﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.DTO
{
    public class ProductDTO
    {
          
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime DateCreate { get; set; }
        public virtual ICollection<FilterDTO> Filters { get; set; }
        // public virtual IList<CharacteristicDTO> Characteristics { get; set; }
    }
}
