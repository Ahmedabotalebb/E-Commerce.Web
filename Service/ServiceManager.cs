using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using ServiceApstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork , IMapper mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>((new ProductService(_unitOfWork, mapper));
        public IProductService productService => _LazyProductService.Value;
    }
}
