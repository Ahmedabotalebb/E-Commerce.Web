using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Models.Product;
using Shared.DataTransfereObjects.ProductModuleDto;

namespace Service.MappingProfiles
{
    class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.BrandName, Option => Option.MapFrom(src => src.poductBrand.Name))
                .ForMember(dest => dest.TypeName, Option => Option.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<PictureUrlResolver>());


            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
