using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceApstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper mapper ,IBasketRepository basketRepository , UserManager<ApplicationUser> userManager,IConfiguration configuration) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>((new ProductService(_unitOfWork, mapper)));
        private readonly Lazy<IAuthenticationService> _authenticationservice = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager, configuration));
        public IProductService productService => _LazyProductService.Value;
        private readonly Lazy<IBasketService>  _basketService = new Lazy<IBasketService>(() =>new BasketService(basketRepository,mapper));
        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService authenticationService => _authenticationservice.Value;
    }
}
