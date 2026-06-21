using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class Order : BaseEntity
    {
        public Order()
        {  }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }
        public DeliveryMethod? DeliveryMethod { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        //[NotMapped]
        //public decimal Total => SubTotal + DeliveryMethod.Cost;

        public decimal GetTotal()
                => SubTotal + DeliveryMethod.Cost;

        public string PaymentId { get; set; } 

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod? deliveryMethod, ICollection<OrderItem> orderItems, decimal subTotal , string paymentIntentId)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            OrderItems = orderItems;
            SubTotal = subTotal;
            PaymentId = paymentIntentId;
        }

    }
}
