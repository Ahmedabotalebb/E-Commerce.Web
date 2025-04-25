using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransfereObjects;

namespace ServiceApstraction
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<ProductDto> GetProductById(int id);
        Task<IEnumerable<TypeDto>> GetProductTypesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
