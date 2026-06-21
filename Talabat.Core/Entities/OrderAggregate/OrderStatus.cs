using System;
using System.Collections.Generic;
using System.Text;

namespace Talabat.Core.Entities.OrderAggregate
{
    public enum OrderStatus : byte
    {
        Pending = 0,
        PaymentSucceded = 1,
        PaymentFailed = 2
    }
}
