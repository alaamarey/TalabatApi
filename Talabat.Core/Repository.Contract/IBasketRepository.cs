using System;
using System.Collections.Generic;
using System.Text;
using Talabat.Core.Entities;

namespace Talabat.Core.Repository.Contract
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);
        Task<CustomerBasket?> UpdateOrCreateBasketAsync( CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketId);

    }
}
