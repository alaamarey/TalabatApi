namespace Talabat.API.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; } = null!;
        public int DeliveryMethodId { get; set; }
    }

}
