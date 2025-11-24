
using ECommerce.CustomMiddleWares;
using ECommerce.Extention;
using ECommerce.Factories;
using ECommerceDomain.Contarcts;
using ECommerceDomain.Entities.IdentityModule;
using ECommercePersistence.IdentityData.DBContext;
using ECommercePersistence.Repositires;
using ECommerceService;
using ECommerceService.MappingProfile;
using ECommerceServiceApstarction;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Linq;
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
            builder.Services.AddKeyedScoped<ECommerceDomain.Contarcts.IDataInitilizer, ECommercePersistence.Data.DataSeed.DataInitilizer>("Default");
            builder.Services.AddKeyedScoped<ECommerceDomain.Contarcts.IDataInitilizer, ECommercePersistence.Data.DataSeed.DataInitilizerIdentity>("Identity");
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IBasketRepo, BasketRepo>();
            builder.Services.AddScoped<IBasketService, BasketService>();

            builder.Services.AddScoped<ICachRepo, CachRepo>();
            builder.Services.AddScoped<ICachService, CachService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddAutoMapper(X => X.AddProfile<ProductProfile>());

            builder.Services.AddAutoMapper(X => X.AddProfile<BasketProfile>());


            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory 
               = ApiResponseFactory.CreateApiResponse;
               

            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(O =>
            {
               
                return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
            } );

            builder.Services.AddDbContext<StoreIdentityDBContext>(options => {   
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            //builder.Services.AddIdentity<ECommerceDomain.Entities.IdentityModule.ApplicationUser, Microsoft.AspNetCore.Identity.IdentityRole>()
            //    .AddEntityFrameworkStores<StoreIdentityDBContext>();
               builder.Services.AddIdentityCore< ApplicationUser >().
                AddRoles<Microsoft.AspNetCore.Identity.IdentityRole>().
                AddEntityFrameworkStores<StoreIdentityDBContext>();
            //Add-Migration  "IdentityTablecreate" -OutDir "Identit" -Context StoreIdentityDBContext
            //Add-Migration "IdentityTableCreate" -OutputDir "Identity/Migrations" -Context "StoreIdentityDBContext"
            var app = builder.Build();

            #region DataSeed

              await  app.MigrateDB();
              await  app.MigrateIDentityDB();
              await  app.SeedDb();
              await  app.SeedIdentityDb();

            #endregion
            //Exception here 
            app.UseMiddleware<ExceptionHandlerMiddleWare>();



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
