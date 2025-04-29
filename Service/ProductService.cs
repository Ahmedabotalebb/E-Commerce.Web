using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Service.Specification;
using ServiceApstraction;
using Shared;
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
        
        

        public async Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams)
        {
            var specification = new ProductWithBrandandTypeSepcification(queryParams);
            var Repo = _Unitofwork.GetRepository<Product, int>();
            var products = await Repo.GetAllAsync(specification);
            var Data = _Imapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var productsCount= Data.Count();

            var totalCount=await Repo.CountAsync(new ProductCountSpecification(queryParams));
            return new PaginatedResult<ProductDto>(queryParams.PageIndex, productsCount, totalCount, Data);
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var specification = new ProductWithBrandandTypeSepcification(id);
            var product = await _Unitofwork.GetRepository<Product,int>().GetByIdAsync(specification);
            return _Imapper.Map<Product, ProductDto>(product);
        }

        public async Task<IEnumerable<TypeDto>> GetProductTypesAsync()
        {
            var products = await _Unitofwork.GetRepository<ProductType, int>().GetAllAsync();
            var productsTypeDto = _Imapper.Map<IEnumerable<ProductType>, IEnumerable<TypeDto>>(products);
            return productsTypeDto;
        }
    }
}
