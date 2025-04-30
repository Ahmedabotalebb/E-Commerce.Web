using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModels;
using ServiceApstraction;
using Shared.DataTransfereObjects.BasketModuleDto;

namespace Service
{
    class BasketService(IBasketRepository basketRepository , IMapper mapper) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CusrtomerBasket = mapper.Map<BasketDto, CustomerBasket>(basket);
           var IsCreatedOrUpdated = await basketRepository.CreateOrUpdateBasketAsync(CusrtomerBasket);
            if (IsCreatedOrUpdated is not null)
                return await GetBasketAsync(basket.Id);
            else
                throw new Exception("Can't Create Or Update Basket Now , Try Again Later");
        }

        

        public async Task<BasketDto> GetBasketAsync(string key)
        {
            var basket = await basketRepository.GetBasketAsync(key);
            if (basket is not null)
            {
                return mapper.Map<CustomerBasket, BasketDto>(basket);
            }
            else
                throw new BasketNotFoundException(key);
        }
        public async Task<bool> DeleteBasketAsync(string key)=> await basketRepository.DeleteBasketAsync(key);
    }
}
