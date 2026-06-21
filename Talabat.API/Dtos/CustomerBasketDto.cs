using System.ComponentModel.DataAnnotations;
using Talabat.Core.Entities;

namespace Talabat.API.Dtos
{
    public class CustomerBasketDto
    {
        //[Required]
        //public string Id { get; set; } //userEmail
        public List<BasketItemDto> BasketItems { get; set; }
        // When Update Basket be the same paymentIntentId
        public int? DeliveryMethodId { get; set; } = 1;
        public decimal? ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }




        /*
         *  PaymentIntentId للباسكت وهي بالفعل كان فيها  Update   لاجل بس لما اعمل  CustomerBasketDto  في  PaymentIntentId  وجود ال 
         */
    }
}
