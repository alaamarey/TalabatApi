using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities.OrderAggregate;

namespace Talabat.Core.Services.Contract
{
    public interface IOrderService
    {

        Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodId);

        Task<IReadOnlyList<Order>?> GetOrdersForUserAsync(string buyerEmail);

        Task<Order?> GetOrderByIdForUserAsync(int orderId, string buyerEmail);

        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync();

    }
}
