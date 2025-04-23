using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Dataseed(StoreDbcontext _Dbcontext) : IDataseeding
    {
        public void DataSeed()
        {
            try
            {
                if (_Dbcontext.Database.GetPendingMigrations().Any())
                {
                    _Dbcontext.Database.Migrate();
                }

                if (!_Dbcontext.productBrands.Any())
                {
                    var ProductBrandsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);
                    if (ProductBrands != null && ProductBrands.Any())
                    {
                        _Dbcontext.productBrands.AddRange(ProductBrands);
                    }
                }

                if (!_Dbcontext.productTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var ProductType = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    if (ProductType != null && ProductType.Any())
                    {
                        _Dbcontext.productTypes.AddRange(ProductType);
                    }
                }
                if (!_Dbcontext.products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    if (Products != null && Products.Any())
                    {
                        _Dbcontext.products.AddRange(Products);
                    }
                }
                _Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}
