using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using ServiceApstraction;
using Shared.DataTransfereObjects;

namespace Service
{
    public class ProductService(IUnitOfWork _Unitofwork,IMapper _Imapper) : IProductService
    {
        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var brands = await _Unitofwork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsDto = _Imapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandDto>>(brands);
            return brandsDto;
        }
        
        

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var products = await _Unitofwork.GetRepository<Product, int>().GetAllAsync();
            var productsDto = _Imapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _Unitofwork.GetRepository<Product,int>().GetByIdAsync(id);
            return _Imapper.Map<Product, ProductDto>(product);
        }

        public async Task<IEnumerable<TypeDto>> GetProductTypesAsync()
        {
            var products = await _Unitofwork.GetRepository<ProductType, int>().GetAllAsync();
            var productsTypeDto = _Imapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(products);
            return productsTypeDtos;
        }
    }
}
