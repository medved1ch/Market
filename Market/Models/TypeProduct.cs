using System;
using System.Collections.Generic;

#nullable disable

namespace Market.Models
{
    public partial class TypeProduct
    {
        public TypeProduct()
        {
            Characteristics = new HashSet<Characteristic>();
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Characteristic> Characteristics { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
