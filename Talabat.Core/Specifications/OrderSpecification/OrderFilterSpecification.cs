using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Specifications.OrderSpecification
{
    public class OrderFilterSpecification : BaseSpecifications<Order>
    {
        public OrderFilterSpecification(string buyerEmail, int? orderId = null) :
            base(O =>
                        (O.BuyerEmail == buyerEmail) && (orderId == null || O.Id == orderId)
            )
        {
            OrderByDesc = O => O.OrderDate;

            Includes.Add(O => O.DeliveryMethod); // One لانها علاقه Eager Loading انا هنا عملتها 
            Includes.Add(O => O.OrderItems); // OrderItems محتاجه اجيب  Order لان في كل مره هجيب  Eager Loading  لكن هتكون  Many هنا العلاقه 

        }


    }
}
