using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class Dataseed(StoreDbcontext _Dbcontext) : IDataseeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                if ( (await _Dbcontext.Database.GetPendingMigrationsAsync()).Any())
                {
                     await _Dbcontext.Database.MigrateAsync();
                }

                if (!_Dbcontext.productBrands.Any())
                {
                    var ProductBrandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands =await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandsData);
                    if ( ProductBrands != null && ProductBrands.Any())
                    {
                        await _Dbcontext.productBrands.AddRangeAsync(ProductBrands);
                    }
                }

                if (!_Dbcontext.productTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\types.json");
                    var ProductType = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    if (ProductType != null && ProductType.Any())
                    {
                        await _Dbcontext.productTypes.AddRangeAsync(ProductType);
                    }
                }
                if (!_Dbcontext.products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeed\products.json");
                    var Products =await  JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    if (Products != null && Products.Any())
                    {
                       await _Dbcontext.products.AddRangeAsync(Products);
                    }
                }
                await _Dbcontext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                //TODO
            }
        }
    }
}
