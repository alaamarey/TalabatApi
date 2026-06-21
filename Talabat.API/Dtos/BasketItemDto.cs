using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(1, double.MaxValue  , ErrorMessage ="Price must be greater than zero")]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity must be at least one ")]
        public int Quantity { get; set; }
    }
}