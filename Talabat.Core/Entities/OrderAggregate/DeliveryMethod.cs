using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entities.OrderAggregate
{
    public class DeliveryMethod : BaseEntity
    {

        public DeliveryMethod()
        {}
        public string  ShortName { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string DeliveryTime { get; set; }


        public DeliveryMethod(string shortName, string description, decimal cost, string deliveryTime)
        {
            ShortName = shortName;
            Description = description;
            Cost = cost;
            DeliveryTime = deliveryTime;
        }

    }

}
