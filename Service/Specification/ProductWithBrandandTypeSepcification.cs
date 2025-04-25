using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace Service.Specification
{
    class ProductWithBrandandTypeSepcification : BaseSpecification <Product , int>
    {
        public ProductWithBrandandTypeSepcification():base(null)
        {
            AddInclude(P => P.poductBrand);
            AddInclude(P => P.ProductType);
        }
        public ProductWithBrandandTypeSepcification(int Id):base(B=>B.Id == Id)
        {
            AddInclude(P => P.poductBrand);
            AddInclude(P => P.ProductType);
        }
    }
}
