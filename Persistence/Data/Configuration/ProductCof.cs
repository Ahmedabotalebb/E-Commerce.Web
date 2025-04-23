using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Data.Configuration
{
    class productConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P => P.poductBrand)
                .WithMany()
                .HasForeignKey(P => P.BrandId);

            builder.HasOne(P => P.poductBrand)
                            .WithMany()
                            .HasForeignKey(P => P.BrandId);
            builder.Property(P => P.Price).HasColumnType("decimal(10,2)");
        }
    }
}
