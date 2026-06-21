using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new();
        public int? DeliveryMethodId { get; set; } = 1;
        public decimal? ShippingPrice { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? ClientSecret { get; set; }
        public CustomerBasket(string id)
        {
            Id = id;
        }

        public CustomerBasket()
        {
        }
        /*
          here i make parameterless ctor to help autoMapper know how to build object of customerBasket 
          as there is no id in customerBasketDto
         */




    }
}
