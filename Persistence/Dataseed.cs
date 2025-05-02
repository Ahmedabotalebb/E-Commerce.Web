using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;

namespace Persistence
{
    public class Dataseed(StoreDbcontext _Dbcontext , UserManager<ApplicationUser> _User
        ,RoleManager<IdentityRole> _Role,
        StoreIdentityDbContext _identityDbcontext) : IDataseeding
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

        public async Task IdentityDataSeedAsync()
        {
            try
            {
                if (!_Role.Roles.Any())
                {
                    await _Role.CreateAsync(new IdentityRole("admin"));
                    await _Role.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_User.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "ahmed@gmail",
                        DisplayName = "ahmed",
                        PhoneNumber = "010",
                        UserName = "ahmedabotaleb"
                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "salma@gmail",
                        DisplayName = "salma",
                        PhoneNumber = "010",
                        UserName = "salma123"
                    };
                    await _User.CreateAsync(user01, "P@ssw0rd");
                    await _User.CreateAsync(user02, "P@ssw0rd");


                    await _User.AddToRoleAsync(user01, "admin");
                    await _User.AddToRoleAsync(user01, "admin");
                }
                await _identityDbcontext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
