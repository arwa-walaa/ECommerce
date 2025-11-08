using ECommerceDomain.Entities;
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
        public void Initilize()
        {
            try
            {
                var HasProducts = _storeDbContext.Products.Any();
                var HasProductBrands = _storeDbContext.ProductBrands.Any();
                var HasProductTypes = _storeDbContext.ProductTypes.Any();
                if (HasProducts && HasProductBrands && HasProductTypes)
                {
                    return;
                }
                if (!HasProductBrands)
                {
                    SeedDataFromJson< ECommerceDomain.Entities.ProductModule.ProductBrand,int>("brands.json", _storeDbContext.ProductBrands);
                   

                }
                if (!HasProductTypes)
                {
                    SeedDataFromJson< ECommerceDomain.Entities.ProductModule.ProductType,int>("types.json", _storeDbContext.ProductTypes);
                }
                _storeDbContext.SaveChanges();

                if (!HasProducts)
                {
                    SeedDataFromJson< ECommerceDomain.Entities.ProductModule.Product,int>("products.json", _storeDbContext.Products);

                }
                _storeDbContext.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured during data seeding: {ex}");
            }
            
        }
        private void SeedDataFromJson<T,TKey>(string fileName,DbSet<T> dbset) where T : BaseEntity<TKey>
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
                    dbset.AddRange(data);
                   
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error occured while seeding data from json file: {ex}");
            }

        }
    }
}
