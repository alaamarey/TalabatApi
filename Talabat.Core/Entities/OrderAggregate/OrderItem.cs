using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        public OrderItem()
        { }
        public int OrderId { get; set; }
        public ProductOrderItem Product { get; set; }
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public OrderItem(ProductOrderItem product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }


    }
}
