using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceApstraction;
using Shared;
using Shared.DataTransfereObjects;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IServiceManager _ServiceManager) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams queryParams)
        {
            var products = await _ServiceManager.productService.GetAllProductsAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int Id)
        {
            var products =await _ServiceManager.productService.GetProductById(Id);
            return Ok(products);
        } 
        
        [HttpGet("Types")]
        public async Task<ActionResult<TypeDto>> GetAllTypes()
        {
            var products =await _ServiceManager.productService.GetProductTypesAsync();
            return Ok(products);
        }
        
        [HttpGet("Brands")]
        public async Task<ActionResult<BrandDto>> GetAllBrands()
        {
            var products =await _ServiceManager.productService.GetAllBrandsAsync();
            return Ok(products);
        }
    }
}
