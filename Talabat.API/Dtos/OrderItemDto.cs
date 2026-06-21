namespace Talabat.API.Dtos
{
    public class OrderItemDto
    {
        public int  Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductPictureURL { get; set; }
        public string ProductBrand { get; set; }
        public string ProductCategory { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}