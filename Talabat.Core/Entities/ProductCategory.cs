using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entities
{
    public class ProductCategory :BaseEntity
    {
        public string Name  { get; set; }

        // انا مش عايز اللقطه دي في البروجكت  ان اعرض المنتجات بتاع براند معين 
        //public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
