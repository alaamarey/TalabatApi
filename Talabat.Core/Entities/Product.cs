using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Talabat.Core.Entities
{
    public class Product  :BaseEntity
    {
        public string Name  { get; set; }

        public string Description  { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        //[ForeignKey(nameof(Product.ProductBrand))]
         public int BrandId { get; set; }

        //[InverseProperty(nameof(ProductBrand.Products))]
        public ProductBrand  ProductBrand { get; set; }

        //[ForeignKey(nameof(Product.ProductCategory))]
        public int CategoryId { get; set; }

        //[InverseProperty(nameof(ProductCategory.Products))]
        public  ProductCategory  ProductCategory { get; set; }
    }
}
