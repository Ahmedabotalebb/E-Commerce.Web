﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.BasketModels;

namespace DomainLayer.Contracts
{
    public interface IBasketRepository
    {
        Task<CustomerBasket?> GetBasketAsync(string key);
        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket basket , TimeSpan? timeToLive = null);
        Task<bool> DeleteBasketAsync(string id);
    }
}
