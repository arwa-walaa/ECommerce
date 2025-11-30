using ECommerceDomain.Entities.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePersistence.Data.Configration.OrderConfigs
{
    public class DeliveryMethoudConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(dm => dm.Price) 
                    .HasColumnType("decimal(18,2)");

            builder.Property(dm => dm.ShortName)
                .HasColumnType("varchar")
               
                .HasMaxLength(50);
            builder.Property(dm => dm.Description)
                  .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(dm => dm.DeliveryTime)
                .HasColumnType("varchar")
              .HasMaxLength(50);

        }
    }
}
