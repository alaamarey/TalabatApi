namespace Talabat.API.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; } // For Update & Delete
        public string Name { get; set; }
        public string Description { get; set; }
        public string? PictureUrl { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; } // For Update & Delete
        public string BrandName { get; set; }
        public int CategoryId { get; set; } //For Update & Delete
        public string CategoryName { get; set; }

    }
}
