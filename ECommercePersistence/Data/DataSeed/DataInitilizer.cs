using ECommerceDomain.Entities;
using ECommerceDomain.Entities.OrderModule;
using ECommercePersistence.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommercePersistence.Data.DataSeed
{
    public class DataInitilizer : ECommerceDomain.Contarcts.IDataInitilizer
    {
        private readonly StoreDbContext _storeDbContext;

        public DataInitilizer(StoreDbContext storeDbContext )
        {
           _storeDbContext = storeDbContext;
        }
        public async Task  InitilizeAsync()
        {
            try
            {
                var HasProducts = await _storeDbContext.Products.AnyAsync();
                var HasProductBrands = await _storeDbContext.ProductBrands.AnyAsync();
                var HasProductTypes = await _storeDbContext.ProductTypes.AnyAsync();
                var HasDeliveryMethods = await _storeDbContext.Set<DeliveryMethod>().AnyAsync();
                if (HasProducts && HasProductBrands && HasProductTypes && HasDeliveryMethods)
                {
                    return;
                }
                if (!HasProductBrands)
                {
                   await SeedDataFromJson< ECommerceDomain.Entities.ProductModule.ProductBrand,int>("brands.json", _storeDbContext.ProductBrands);
                   

                }
                if (!HasProductTypes)
                {
                    await SeedDataFromJson< ECommerceDomain.Entities.ProductModule.ProductType,int>("types.json", _storeDbContext.ProductTypes);
                }
                _storeDbContext.SaveChanges();

                if (!HasProducts)
                {
                    await SeedDataFromJson< ECommerceDomain.Entities.ProductModule.Product,int>("products.json", _storeDbContext.Products);

                }
                if (!HasDeliveryMethods)
                {
                    await SeedDataFromJson< DeliveryMethod,int>("delivery.json", _storeDbContext.Set<DeliveryMethod>());
                }
                _storeDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured during data seeding: {ex}");
            }
            
        }
        private async Task SeedDataFromJson<T,TKey>(string fileName,DbSet<T> dbset) where T : BaseEntity<TKey>
        {
            //filepath 
            //D:\Arwa\.Net\C#\ECommerceSolution\ECommercePersistence\Data\DataSeed\JSONFiles\brands.json
            var path = @"..\ECommercePersistence\Data\DataSeed\JSONFiles\"+fileName;
            if(!File.Exists(path)) throw new FileNotFoundException("Data Seed file not found",path);
            try
            {
                using var DataStream = File.OpenRead(path);
                var data=JsonSerializer.Deserialize<List<T>>(DataStream, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                });
                if (data != null )
                {
                   await dbset.AddRangeAsync(data);
                   
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error occured while seeding data from json file: {ex}");
            }

        }
    }
}
