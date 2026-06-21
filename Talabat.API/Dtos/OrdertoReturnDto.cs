using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.API.Dtos
{
    public class OrdertoReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string DeliveryMethodName { get; set; }
        public decimal DeliveryMethodPrice { get; set; }
        public decimal SubTotal { get; set; }
        public Address ShippingAddress { get; set; }
        public decimal Total { get; set; }
        public string PaymentId { get; set; } = string.Empty;
        public ICollection<OrderItemDto> OrderItems { get; set; }






    }
}
