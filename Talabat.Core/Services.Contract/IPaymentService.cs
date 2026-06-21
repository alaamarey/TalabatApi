using Talabat.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities;

namespace Talabat.Core.Services.Contract
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntent(string basketId);

        Task<Order> UpdatePaymentIntentToSucceededOrFailedAsync(string paymentIntentId, bool isSucceeded);
    }
}
