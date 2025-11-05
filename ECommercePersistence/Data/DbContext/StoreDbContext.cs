using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommercePersistence.Data.DbContext
{
    public class StoreDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public StoreDbContext(Microsoft.EntityFrameworkCore.DbContextOptions<StoreDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public Microsoft.EntityFrameworkCore.DbSet<ECommerceDomain.Entities.ProductModule.Product> Products { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<ECommerceDomain.Entities.ProductModule.ProductBrand> ProductBrands { get; set; } 
        public Microsoft.EntityFrameworkCore.DbSet<ECommerceDomain.Entities.ProductModule.ProductType> ProductTypes { get; set; } 
    }
}
