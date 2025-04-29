using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Shared;

namespace Service.Specification
{
    class ProductWithBrandandTypeSepcification : BaseSpecification <Product , int>
    {
        public ProductWithBrandandTypeSepcification(ProductQueryParams queryParams) : base(b => (!queryParams.BrandId.HasValue || b.BrandId == queryParams.BrandId)
        && (!queryParams.TypeId.HasValue || b.TypeId == queryParams.TypeId)
        &&(string.IsNullOrWhiteSpace(queryParams.SearchValue)||b.Name.ToLower().Contains(queryParams.SearchValue.ToLower()))
        )
        {
            AddInclude(P => P.poductBrand);
            AddInclude(P => P.ProductType);

            switch (queryParams.sortingOptions)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(b=>b.Name);
                    break;
                case ProductSortingOptions.NameDesc:
                    AddOrderByDecsinding(b=>b.Name);
                    break;
                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(b=>b.Price);
                    break;
                case ProductSortingOptions.PriceDesc:
                    AddOrderByDecsinding(b => b.Price);
                    break;
                    default:
                    break;
                        

            }
            ApplyPagenation(queryParams.PageSize, queryParams.PageIndex);
        }
        public ProductWithBrandandTypeSepcification(int Id):base(B=>B.Id == Id)
        {
            AddInclude(P => P.poductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
