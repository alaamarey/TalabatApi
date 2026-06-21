using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Specifications.OrderSpecification
{
    public class OrderWithPaymentIntentId :BaseSpecifications<Order>
    {

        public OrderWithPaymentIntentId(string paymentIntentId) 
            :base( O => O.PaymentId == paymentIntentId)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.OrderItems);
        }
    }
}
