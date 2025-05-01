using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransfereObjects.ProductModuleDto;

namespace ServiceApstraction
{
    public interface IProductService
    {
        Task<PaginatedResult<ProductDto>> GetAllProductsAsync(ProductQueryParams queryParams);
        Task<ProductDto> GetProductById(int id);
        Task<IEnumerable<TypeDto>> GetProductTypesAsync();
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    }
}
