using ECommerceDomain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePersistence.Data.Configration
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
               
                .HasMaxLength(100);
            builder.Property(p => p.Description)
               
                .HasMaxLength(500);
            builder.Property(p => p.Price)
                .HasPrecision(18,2);
            builder.Property(p => p.PictureUrl)
                 .HasMaxLength(200);
               
            // Configure relationships
            builder.HasOne(p => p.ProductBrands)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.ProductTypes)
                .WithMany()
                .HasForeignKey(p => p.TypeId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
