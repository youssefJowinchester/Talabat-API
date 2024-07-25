﻿using Talabat.Core.Entities;

namespace Talabat.Repository.Repositories
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string basketId);

        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);

        Task<bool> DeleteBasketAsync(string basketId);
    }
}
