using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class ProductOrderItem
    {

        public ProductOrderItem()
        {}
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPictureURL { get; set; }
        public string ProductBrand { get; set; }
        public string ProductCategory { get; set; }

        public ProductOrderItem(int productId, string productName, string productDescription, string productPictureURL, string productBrand, string productCategory)
        {
            ProductId = productId;
            ProductName = productName;
            ProductDescription = productDescription;
            ProductPictureURL = productPictureURL;
            ProductBrand = productBrand;
            ProductCategory = productCategory;
        }

    }
}
