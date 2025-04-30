using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models.Product;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class StoreDbcontext : DbContext
    {
        public StoreDbcontext(DbContextOptions<StoreDbcontext> options) : base(options)
        {

        }
        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> productBrands { get; set; }
        public DbSet<ProductType> productTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemplyRefernce).Assembly);
        }
    }
}
