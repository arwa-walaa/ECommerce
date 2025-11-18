
using ECommerce.Extention;
using ECommerceDomain.Contarcts;
using ECommercePersistence.Repositires;
using ECommerceService;
using ECommerceService.MappingProfile;
using ECommerceServiceApstarction;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace ECommerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ECommercePersistence.Data.DbContext.StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<ECommerceDomain.Contarcts.IDataInitilizer, ECommercePersistence.Data.DataSeed.DataInitilizer>();

            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBasketRepo, BasketRepo>();
            builder.Services.AddScoped<IBasketService, BasketService>();

            builder.Services.AddScoped<ICachRepo, CachRepo>();
            builder.Services.AddScoped<ICachService, CachService>();
            builder.Services.AddAutoMapper(X => X.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(X => X.AddProfile<BasketProfile>());




            builder.Services.AddSingleton<IConnectionMultiplexer>(O =>
            {
               
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            } );


            var app = builder.Build();

            #region DataSeed

          await  app.MigrateDB();
          await  app.SeedDb();

            #endregion
            //Exception here 
            app.Use(async(Context,Next)=>
            {
               
                try
                {
                    await Next();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Context.Response.StatusCode = StatusCodes.Status500InternalServerError ;
                    await Context.Response.WriteAsJsonAsync(new 
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Error =$"Something went wrong , {ex.Message}"

                    });


                }


            });



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
