using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Product;
using Service.Specification;
using Shared;

namespace Service.Specification
{
    class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) :
            base(b => (!queryParams.BrandId.HasValue || b.BrandId == queryParams.BrandId)
         && (!queryParams.TypeId.HasValue || b.TypeId == queryParams.TypeId)
         && (string.IsNullOrWhiteSpace(queryParams.SearchValue) || b.Name.ToLower().Contains(queryParams.SearchValue.ToLower())))
         {
        
        
        }
         


    }
}
