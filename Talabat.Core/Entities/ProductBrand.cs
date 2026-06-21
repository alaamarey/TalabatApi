using System;

namespace Talabat.Core.Entities
{
    public class ProductBrand :BaseEntity
    {
        public string Name  { get; set; }


        // انا مش عايز اللقطه دي في البروجكت  ان اعرض المنتجات بتاع براند معين 
       //public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
